using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Text;

/* test area */
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/*
public delegate void UpdateProgress(float percent);
public delegate void DoNextDownload();
*/

public class AssetBundleMgr : MonoBehaviour
{

	/* test area */
	private Dictionary<string, int> progressDic = new Dictionary<string, int> ();
	int count = 0;
	int beforeCount = 0;
	int max = 0;
	bool updated = false;

	public void SetDownloadList (string[] list)
	{
		for (int i = 0; i < list.Length; i++) {
			progressDic.Add (list [i], 0);
		}
		max = progressDic.Count * 100;
	}

	[MethodImpl (MethodImplOptions.Synchronized)]
	public void OnProgressChanged (int percent, string name)
	{
		//lock (lockObject)
		//{
		//progressDic[name] = percent;
		//}
	}

	public void OnDownloaded (string name)
	{
		progressDic [name] = 100;
		updated = true;
	}

	IEnumerator UpdateUI ()
	{
		while (true) {
			try {
				count = 0;
				if (max != 0) {
					//lock (lockObject)
					//{
					foreach (int percent in progressDic.Values) {
						count += percent;
					}
					//}

					if (beforeCount != count) {
						GuiMgr.GetInst ().OnPatchProgressChanged ((float)count / (float)max);

						if (count >= max) {
							break;
						} else if (updated) {
							DownloadNextAssetBundle ();
							updated = false;
						}
						beforeCount = count;
					}
				}
			} catch (Exception e) {
				Debug.LogError (e.Message);
			} finally {
			}
			yield return null;
		}

		CompleteDownloadAssetBundles ();
		GuiMgr.GetInst ().OnPatchCompleted ();
	}
	/* end */

	private static AssetBundleMgr inst;
	private static DateTime devicesPatchDateTime = Convert.ToDateTime ("5/17/2016 4:43:00 PM");
	//TODO: device에 저장되어 있어야함.
	private static DateTime candidatePatchDateTime;

	private int downloadedAssetCount;
	private string[] downloadAssetList;

	void Awake ()
	{
		if (!inst)
			inst = this;
	}

	private AssetBundleMgr ()
	{
	}

	public static AssetBundleMgr GetInst ()
	{
		return inst;
	}

	public void CheckIsPatched ()
	{
		HttpReqMgr.GetInst ().ReqHttpGet ("info/patchdate", CheckPatchDateTime);
	}

	private void CheckPatchDateTime (string responseJson)
	{
		PatchDateTimeResponse response = JsonParser.GetResponseJsonClassObject<PatchDateTimeResponse> (responseJson);
		candidatePatchDateTime = Convert.ToDateTime (response.recent_patch_date);

		if (devicesPatchDateTime.CompareTo (candidatePatchDateTime) < 0) {
			GameStateMgr.GetInst ().OnCompletePatchCheck (false);
			TryPatch ();
			return;
		} else {
			Debug.Log ("Already patched");
			GameStateMgr.GetInst ().OnCompletePatchCheck (true);
		}
	}

	public void TryPatch ()
	{
#if UNITY_EDITOR
		if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android) {
			HttpReqMgr.GetInst ().ReqHttpGet ("info/android/patchlist", DownloadAssetBundles);
		} else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS) {
			HttpReqMgr.GetInst ().ReqHttpGet ("info/ios/patchlist", DownloadAssetBundles);
		} else {
			Debug.LogError ("please change build target (Android or iOS)");
			return;
		}
#elif UNITY_ANDROID
		HttpReqMgr.GetInst().ReqHttpGet("info/android/patchlist", DownloadAssetBundles);
#elif UNITY_IPHONE
		HttpReqMgr.GetInst().ReqHttpGet("info/ios/patchlist", DownloadAssetBundles);
#endif
	}

	private void DownloadAssetBundles (string responseJson)
	{
		PatchListResponse patchListResponse = JsonParser.GetResponseJsonClassObject<PatchListResponse> (responseJson);

		downloadAssetList = patchListResponse.patch_list;
		downloadedAssetCount = 0;

		SetDownloadList (patchListResponse.patch_list);
		StartCoroutine (UpdateUI ()); //TODO: 완료, 에러, back버튼 cancle coroutine.

		if (downloadAssetList.Length != 0)
			DownloadNextAssetBundle ();
		else {
			Debug.LogError ("patchlist is empty");
		}
	}

	public void DownloadNextAssetBundle ()
	{
		HttpReqMgr.GetInst ().ReqDownloadAssetBundle (downloadAssetList [downloadedAssetCount]);//, function1, TryNextDownload);
		downloadedAssetCount++;
	}

	public void CompleteDownloadAssetBundles ()
	{
		downloadAssetList = null;
		devicesPatchDateTime = candidatePatchDateTime;
		GuiMgr.GetInst ().OnPatchCompleted ();
        
		Debug.Log ("download complete");
	}

	
	static Dictionary<string, object> cachedAssetDic = new Dictionary<string, object> ();
	static AssetBundleManifest manifest = null;
	//reference: http://docs.unity3d.com/ScriptReference/AssetBundle.LoadFromFile.html
	public GameObject LoadAsset (string name)
	{
		StringBuilder sb = new StringBuilder (Application.persistentDataPath);
		sb.Append ('/');
		string assetBundleFolder = sb.ToString ();					//
		Debug.Log ("bundle folder: " + assetBundleFolder);

		sb = new StringBuilder (name);
		sb.Append (".unity3d");
		string bundleName = sb.ToString ();							//
		Debug.Log ("target name: " + bundleName);

		GameObject prefab = null;
		try {

			/* check manifest file */
			if (manifest == null) {
				sb = new StringBuilder (assetBundleFolder);
#if UNITY_EDITOR
				if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android) {
					sb.Append ("Android");
				} else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS) {
					sb.Append ("iOS");
				} else {
					Debug.LogError ("please change build target (Android or iOS)");
					return null;
				}
#elif UNITY_ANDROID
		sb.Append("Android");
#elif UNITY_IPHONE
		sb.Append("iOS");
#endif
				string manifestABPath = sb.ToString ();						//
				Debug.Log ("manifestABPath: " + manifestABPath);
				AssetBundle manifestBundle = AssetBundle.LoadFromFile (manifestABPath);
				manifest = manifestBundle.LoadAsset ("AssetBundleManifest") as AssetBundleManifest;
				manifestBundle.Unload (false);
			}

			string[] dependentAssetBundles = manifest.GetAllDependencies (bundleName);
			for (int i = 0; i < dependentAssetBundles.Length; i++) {
				Debug.Log (" " + dependentAssetBundles [i]);
			}

			AssetBundle[] assetBundles = new AssetBundle[dependentAssetBundles.Length];
			for (int i = 0; i < dependentAssetBundles.Length; i++) {

				if (!cachedAssetDic.ContainsKey (dependentAssetBundles [i])) {
					sb = new StringBuilder (assetBundleFolder);
					sb.Append (dependentAssetBundles [i]);
					Debug.Log ("  " + sb.ToString ());  //

					assetBundles [i] = AssetBundle.LoadFromFile (sb.ToString());
					//object temp = assetBundles[i].LoadAsset(assetBundles[i].GetAllAssetNames()[0]);
					cachedAssetDic.Add(dependentAssetBundles[i], assetBundles [i]);
				}

			}


			sb = new StringBuilder (assetBundleFolder);
			sb.Append (bundleName);
			Debug.Log ("target: " + sb.ToString ());
			//Debug.Log("load Path: " + sb.ToString());
			AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(sb.ToString());
			prefab = myLoadedAssetBundle.LoadAsset(name + ".prefab") as GameObject;
			prefab = myLoadedAssetBundle.LoadAsset(myLoadedAssetBundle.GetAllAssetNames()[0]) as GameObject;

			//for (int i = 0; i < dependentAssetBundles.Length; i++) {
				//if (assetBundles[i] != null)
				//	assetBundles[i].Unload(false);
			//}
			myLoadedAssetBundle.Unload (false);

		} catch (Exception e) {
			Debug.Log ("failed to load AssetBundle!");
			Debug.LogError (e.Message);
			return null;
		}
		return prefab;
	}

}
