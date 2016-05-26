using UnityEngine;
using System.Collections;

using System.Collections.Generic;

/* 진명 : LoadCubeFromAssetBundle */
public class Test : MonoBehaviour {
	const string assetBundleFolder = "http://192.168.0.148:9192/iOS/";
	// "file://C:/Users/Vincent/Desktop/Unite_Seattle/NewAssetBundleDemo/AssetBundles/";
	// /Users/yeonjongjeong/prj/ngui_ex/ngui_ex/Assets/@AssetBundle
	const string cubeAssetBundle = "cube.unity3d";

	IEnumerator Start() {
		yield return StartCoroutine (Load());
		yield return StartCoroutine (Load());
	}
		
	// Use this for initialization
	IEnumerator Load() {
		string manifestABPath = assetBundleFolder + "iOS";//"AssetBundles";
	
		// Load AssetBundleManifest assetBundle
		var wwwManifest = new WWW(manifestABPath);
		yield return wwwManifest;
		AssetBundle manifestBundle = wwwManifest.assetBundle;

		// Load AssetBundleManifest object
		AssetBundleManifest manifest = manifestBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
		manifestBundle.Unload (false);

		// Get dependent assetBundles
		string[] dependentAssetBundles = manifest.GetAllDependencies(cubeAssetBundle);
		for (int i = 0; i < dependentAssetBundles.Length; i++) {
			Debug.Log (dependentAssetBundles[i]);
		}

		// Load dependent assetBundles
		AssetBundle[] assetBundles = new AssetBundle[dependentAssetBundles.Length];
		for (int i = 0; i < dependentAssetBundles.Length; i++) {
			string assetBundlePath = assetBundleFolder + dependentAssetBundles [i];

			// Get the hash
			Hash128 hash = manifest.GetAssetBundleHash(dependentAssetBundles[i]);
			var www = WWW.LoadFromCacheOrDownload (assetBundlePath, hash);
			yield return www;
			assetBundles [i] = www.assetBundle;
		}

		// Load cube assetBundle
		var wwwCube = WWW.LoadFromCacheOrDownload(assetBundleFolder + cubeAssetBundle, manifest.GetAssetBundleHash(cubeAssetBundle));
		yield return wwwCube;
		AssetBundle cubeBundle = wwwCube.assetBundle;

		// Load cube
		GameObject cubePrefab = cubeBundle.LoadAsset("Cube") as GameObject;
		GameObject.Instantiate (cubePrefab);


		// Unload dependent assetBundles
		for (int i = 0; i < dependentAssetBundles.Length; i++) {
			assetBundles [i].Unload (false);
		}

		// Unload cube assetBundle
		cubeBundle.Unload(false);
	}
}
