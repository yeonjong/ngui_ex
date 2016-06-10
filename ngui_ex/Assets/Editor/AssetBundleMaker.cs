using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

public class AssetBundleMaker {

    private const string OUTPUT_PATH_ANDROID = "Assets/@AssetBundle/Android";
	private const string OUTPUT_PATH_IOS = "Assets/@AssetBundle/iOS";

/* (unity4) 옛날 버전의 방법. */
/*
    [MenuItem("Tools/AssetBundle/MakeAssetBundle - Track dependencies (v4)")]
    static void ExportResource()
    {
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "", "unity3d");
        Debug.Log(path);
        if (path.Length != 0)
        {
            UnityEngine.Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, 0);
            Selection.objects = selection;
        }
    }
    [MenuItem("Tools/AssetBundle/MakeAssetBundle - No dependency tracking (v4)")]
    static void ExportResourceNoTrack()
    {
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "", "unity3d");
        if (path.Length != 0)
        {
            BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path);
        }
    }
*/


/* (unity5) AssetBundle 만들기. */

	[MenuItem("Tools/Asset Bundle/Register selected asset to assetDatabase")]
	static void RegistAsset() {
		ClearLog ();

		Dictionary<string, string> assetImporterDic = new Dictionary<string, string> ();
		string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
		for (int i = 0; i < assetBundleNames.Length; i++) {
			assetImporterDic.Add (assetBundleNames[i], "");
		}

		var selectedPrefab = Selection.activeObject;
		string selectedPrefabPath = AssetDatabase.GetAssetPath (selectedPrefab);
		RegistToAssetImporter (ref assetImporterDic, selectedPrefabPath);

		foreach (var dependency in EditorUtility.CollectDependencies(new[]{ selectedPrefab })) {
			var dependencyPath = AssetDatabase.GetAssetPath(dependency);
			if (string.IsNullOrEmpty(dependencyPath)) continue;

			string extension = Path.GetExtension (dependencyPath);
			//Debug.Log (dependencyPath);
			//Debug.Log (extension);

			switch (extension) {
			case ".prefab":
				if (dependencyPath.Equals (selectedPrefabPath))
					continue;
				RegistToAssetImporter (ref assetImporterDic, dependencyPath);
				break;
			case ".shader":
			case ".mat":
			case ".FBX":
			case ".psd":	// texture extention add.
			case ".png":
			case ".wav":	// autioClip extention add.
				//Debug.Log("?? " + dependencyPath);
				RegistToAssetImporter (ref assetImporterDic, dependencyPath);
				break;
			case ".cs":		// skip script
				break;
			default:
				StringBuilder sb = new StringBuilder ("There are non registered extention. Please check and add this extention.\n");
				sb.AppendFormat ("New Extention: {0}\n", extension);
				sb.AppendFormat ("Asset Path: {0}\n", dependencyPath);
				Debug.LogError(sb.ToString());
				break;
			}
		}
	}

	static void RegistToAssetImporter(ref Dictionary<string, string> dic, string targetPath) {
		//string extension = Path.GetExtension (targetPath);

		int lastSlashIndex = targetPath.LastIndexOf('/');
		string assetBundleName = targetPath.Substring(lastSlashIndex + 1, targetPath.Length - lastSlashIndex - 1);
		assetBundleName = assetBundleName.ToLower();
		assetBundleName += ".unity3d";

		if (dic.ContainsKey (assetBundleName)) {
			/*
			//Debug.Log (dic [name] + "/" + extension);
			if (dic [name].Equals (extension)) {
				//Debug.Log ("Same extension. skip");
				return;
			} else {
				Debug.LogError ("two objects have same name but have different extention");
				Debug.LogError ("it was not made to asset bundle. please check these objects");
				Debug.LogError ("[crash info] name: " + name + ", extention 1 :" + dic [name] + ", extention 2 " + extension);
				Debug.LogError (targetPath);
				return;
			}
			*/
		} else {
			AssetImporter importer = AssetImporter.GetAtPath (targetPath);
			importer.assetBundleName = assetBundleName;
			dic.Add (assetBundleName, "");
			Debug.Log(targetPath +  " => " + assetBundleName);
		}
	}


	[MenuItem("Tools/Asset Bundle/Make AssetBundles for iOS")]
	static void MakeAssetBundle_iOS() {
		ClearLog ();

		if (EditorUserBuildSettings.selectedBuildTargetGroup != BuildTargetGroup.iOS) {
			Debug.LogError ("<b>Build for iOS</b> requires <b>iOS Platform</b> to be selected.");
			return;
		}

		if (!Directory.Exists(OUTPUT_PATH_IOS)) {
			Debug.Log("Directory not exist: " + OUTPUT_PATH_IOS);
		}

		CreateNewAssetBundleFromAssetDatabase (BuildTarget.iOS, OUTPUT_PATH_IOS);
	}
	[MenuItem("Tools/Asset Bundle/Make AssetBundles for Android")]
	static void MakeAssetBundle_Android() {
		ClearLog ();

		if (EditorUserBuildSettings.selectedBuildTargetGroup != BuildTargetGroup.iOS) {
			Debug.LogError ("<b>Build for iOS</b> requires <b>iOS Platform</b> to be selected.");
			return;
		}

		if (!Directory.Exists(OUTPUT_PATH_IOS)) {
			Debug.LogError("Directory not exist: " + OUTPUT_PATH_IOS);
		}

		CreateNewAssetBundleFromAssetDatabase (BuildTarget.Android, OUTPUT_PATH_ANDROID);
	}
	static void CreateNewAssetBundleFromAssetDatabase(BuildTarget buildTarget, string outputPath) {
		try {
			BuildPipeline.BuildAssetBundles (outputPath, BuildAssetBundleOptions.None, buildTarget);
			Debug.Log("Success making assetbundles");
		} catch (Exception e) {
			Debug.Log ("Fail making assetbundles");
			Debug.LogError (e.Message);
			return;
		} finally {
			AssetDatabase.Refresh();
		}
	}

	[MenuItem("Tools/Asset Bundle/Check AssetBundleNames")]
    static void CheckAssetDatabase()
    {
		ClearLog ();

        StringBuilder sb = new StringBuilder("[Check AssetBundleNames]");

        string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        string[] unusedAssetBundleNames = AssetDatabase.GetUnusedAssetBundleNames();

        sb.Append("\n# of AssetBundles: ");
        sb.Append(assetBundleNames.Length);
        for (int i = 0; i < assetBundleNames.Length; i++)
        {
            sb.Append("\n ");
            sb.Append(assetBundleNames[i]);

            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleNames[i]);
            for (int j = 0; j < assetPaths.Length; j++)
            {
                sb.Append("\n  ");
                sb.Append(assetPaths[j]);
            }
        }

        sb.Append("\n\n# of unused AssetBundles: ");
        sb.Append(unusedAssetBundleNames.Length);
        for (int i = 0; i < unusedAssetBundleNames.Length; i++)
        {
            sb.Append("\n ");
            sb.Append(unusedAssetBundleNames[i]);
        }

        sb.Append("\n");
        Debug.Log(sb.ToString());
        sb.Length = 0; sb.Capacity = 0;
    }

	[MenuItem("Tools/Asset Bundle/Remove UnusedAssetBundleNames on AssetDatabase")]
    static void RemoveAllUnusedAssetBundleNames()
    {
		ClearLog ();

        StringBuilder sb = new StringBuilder("[Show unused AssetBundles]");
        string[] unusedAssetBundleNames = AssetDatabase.GetUnusedAssetBundleNames();
        sb.Append("\n# of unused AssetBundles: ");
        sb.Append(unusedAssetBundleNames.Length);

        if (unusedAssetBundleNames.Length == 0)
        {
            sb.Append("\nThere are not exist unused AssetBundles");
            sb.Append("\n");
            Debug.Log(sb.ToString());
            sb.Length = 0; sb.Capacity = 0;
            return;
        }

        for (int i = 0; i < unusedAssetBundleNames.Length; i++)
        {
            sb.Append("\n ");
            sb.Append(unusedAssetBundleNames[i]);
        }


        AssetDatabase.RemoveUnusedAssetBundleNames();
        sb.Append("\nRemove complete\n");
        Debug.Log(sb.ToString());
        sb.Length = 0; sb.Capacity = 0;

        AssetDatabase.Refresh();
    }

	public static void ClearLog() {
		var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
		var type = assembly.GetType("UnityEditorInternal.LogEntries");
		var method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
	}

	/*
	//[MenuItem("Tools/Make AssetBundle for iOS")]// - with script, many target prefabs (v5)")]
    static void CreateNewAssetBundleFromAssetBundleBuild()
    {
		if (EditorUserBuildSettings.selectedBuildTargetGroup != BuildTargetGroup.iOS) {
			Debug.LogError ("<b>Build for iOS</b> requires <b>iOS Platform</b> to be selected.");
			return;
		}

		if (!Directory.Exists(OUTPUT_PATH_IOS)) {
			Debug.Log("Directory not exist: " + OUTPUT_PATH_IOS);
			return;
		}
        
        List<string> targetPrefabList = new List<string>();
        foreach (UnityEngine.Object obj in Selection.objects) {
            targetPrefabList.Add(AssetDatabase.GetAssetPath(obj));
        }

        // ex) Assets/Test.prefab => test.unity3d
        string activePrefabPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        int lastSlashIndex = activePrefabPath.LastIndexOf('/');
        string assetBundleName = activePrefabPath.Substring(lastSlashIndex + 1, activePrefabPath.Length - lastSlashIndex - 1);
        assetBundleName = assetBundleName.Replace(".prefab", ".unity3d");
        assetBundleName = assetBundleName.ToLower();
        
        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        buildMap[0].assetBundleName = assetBundleName;
        buildMap[0].assetNames = targetPrefabList.ToArray();
        
		try {
			BuildPipeline.BuildAssetBundles (OUTPUT_PATH_IOS, buildMap, BuildAssetBundleOptions.None, BuildTarget.iOS);
			Debug.Log("Made one AssetBundle for iOS: " + assetBundleName);
		} catch (Exception e) {
			Debug.Log ("Failure make some assetBundles");
			Debug.Log (e.Message);
		} finally {
			AssetDatabase.Refresh();
		}
    }
	*/

/* AssetBundleBuild 참고.
* 
AssetBundleBuild[] buildMap = new[] {
	new AssetBundleBuild { assetBundleName = "test.unity3d", assetNames = new[] { "Assets/Test.prefab"} },
* 
*/

}