using UnityEngine;
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

public class AssetBundleMgr : MonoBehaviour {

    /* test area */
    private Dictionary<string, int> progressDic = new Dictionary<string, int>();
    int count = 0;
    int beforeCount = 0;
    int max = 0;
    bool updated = false;

    public void SetDownloadList(string[] list) {
        for (int i = 0; i < list.Length; i++) {
            progressDic.Add(list[i], 0);
        }
        max = progressDic.Count * 100;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
	public void OnProgressChanged(int percent, string name) {
        //lock (lockObject)
        //{
        //progressDic[name] = percent;
        //}
    }

	public void OnDownloaded(string name) {
		progressDic[name] = 100;
        updated = true;
    }

    IEnumerator UpdateUI() {
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
                        GuiMgr.GetInst().OnPatchProgressChanged((float)count / (float)max);

                        if (count >= max) {
                            break;
                        } else if (updated) {
							DownloadNextAssetBundle();
                            updated = false;
                        }
                        beforeCount = count;
                    }
                }
            } catch (Exception e) {
                Debug.LogError (e.Message);
            } finally { }
            yield return null;
        }

		CompleteDownloadAssetBundles();
        GuiMgr.GetInst().OnPatchCompleted();
    }
    /* end */

    private static AssetBundleMgr inst;
    private static DateTime devicesPatchDateTime = Convert.ToDateTime("5/17/2016 4:43:00 PM"); //TODO: device에 저장되어 있어야함.
    private static DateTime candidatePatchDateTime;

    private int downloadedAssetCount;
    private string[] downloadAssetList;

    void Awake() {
        if (!inst) inst = this;
    }

    private AssetBundleMgr() { }

    public static AssetBundleMgr GetInst() {
        return inst;
    }

    public void CheckIsPatched() {
        HttpReqMgr.GetInst().ReqHttpGet("info/patchdate", CheckPatchDateTime);
    }

    private void CheckPatchDateTime(string responseJson) {
        PatchDateTimeResponse response = JsonParser.GetResponseJsonClassObject<PatchDateTimeResponse>(responseJson);
		candidatePatchDateTime = Convert.ToDateTime(response.recent_patch_date);

		if (devicesPatchDateTime.CompareTo(candidatePatchDateTime) < 0) {
            GuiMgr.GetInst().ShowPatchUI();
            TryPatch();
            return;
        } else {
            Debug.Log("Already patched");
            GameStateMgr.GetInst().ForwardState(GAME_STATE.LobbyState);
        }
    }

    public void TryPatch() {
		HttpReqMgr.GetInst().ReqHttpGet("info/patchlist", DownloadAssetBundles);
    }

    private void DownloadAssetBundles(string responseJson) {
        PatchListResponse patchListResponse = JsonParser.GetResponseJsonClassObject<PatchListResponse>(responseJson);

		downloadAssetList = patchListResponse.patch_list;
		downloadedAssetCount = 0;

        SetDownloadList(patchListResponse.patch_list);
        StartCoroutine(UpdateUI()); //TODO: 완료, 에러, back버튼 cancle coroutine.

		if (downloadAssetList.Length != 0)
			DownloadNextAssetBundle ();
		else {
			Debug.LogError ("info/patchlist is empty");
		}
    }

    public void DownloadNextAssetBundle() {
		HttpReqMgr.GetInst().ReqDownloadAssetBundle(downloadAssetList[downloadedAssetCount]);//, function1, TryNextDownload);
		downloadedAssetCount++;
    }

	public void CompleteDownloadAssetBundles() {
		downloadAssetList = null;
        devicesPatchDateTime = candidatePatchDateTime;
        GuiMgr.GetInst().OnPatchCompleted();
        
        Debug.Log("download complete");
    }
    
    //reference: http://docs.unity3d.com/ScriptReference/AssetBundle.LoadFromFile.html
    public GameObject LoadAsset(string name) {
		StringBuilder sb = new StringBuilder(Application.persistentDataPath);
		sb.Append('/');
        sb.Append(name);
        sb.Append(".unity3d");

		GameObject prefab = null;
		try {
			//Debug.Log("load Path: " + sb.ToString());
			AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(sb.ToString());
        	prefab = myLoadedAssetBundle.LoadAsset<GameObject>(name + ".prefab");
			myLoadedAssetBundle.Unload(false);
		} catch (Exception e) {
			Debug.Log("failed to load AssetBundle!");
			Debug.LogError (e.Message);
			return null;
		}
        return prefab;
    }

}
