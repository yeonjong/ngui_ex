using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class AssetBundleMaker {

    private const string OUTPUT_PATH = "Assets/@AssetBundle";

    /*
    [MenuItem("Tools/AssetBundle/MakeAssetBundle - Track dependencies (v4)")]
    static void ExportResource()
    {
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "", "unity3d");
        Debug.Log(path);
        if (path.Length != 0)
        {
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
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

    [MenuItem("Tools/AssetBundle/CheckAssetDatabase - with editor (v5)")]
    static void CheckAssetDatabase()
    {
        StringBuilder sb = new StringBuilder("[Check Asset Database]");

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

    [MenuItem("Tools/AssetBundle/RemoveUnusedAssetBundleNames on AssetDatabase - with editor (v5) (be careful !!)")]
    static void RemoveAllUnusedAssetBundleNames()
    {
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

    [MenuItem("Tools/AssetBundle/MakeAssetBundle - with editor (v5)")]
    static void CreateNewAssetBundleFromAssetDatabase()
    {
        CheckAssetDatabase();

        string outputPath = OUTPUT_PATH; //"AssetBundles";
        if (!Directory.Exists(outputPath))
        {
            Debug.Log("Directory not exist: " + outputPath);
        }

        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.Android);
        Debug.Log("Made some AssetBundles");

        AssetDatabase.Refresh();
    }

    /*
    [MenuItem("Tools/AssetBundle/MakeAssetBundle - with script, just one target prefab (v5)")]
    static void CreateNewAssetBundleFromScript()
    {
        string outputPath = OUTPUT_PATH; //"AssetBundlesFromScript";
        if (!Directory.Exists(outputPath))
        {
            Debug.Log("Directory not exist: " + outputPath);
        }

        string targetPrefabPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        AssetImporter testImporter = AssetImporter.GetAtPath(targetPrefabPath);

        // ex) Assets/Test.prefab => test.unity3d
        int lastSlashIndex = targetPrefabPath.LastIndexOf('/');
        string assetBundleName = targetPrefabPath.Substring(lastSlashIndex + 1, targetPrefabPath.Length - lastSlashIndex -1);
        assetBundleName = assetBundleName.Replace(".prefab", ".unity3d");
        assetBundleName = assetBundleName.ToLower();
        
        testImporter.assetBundleName = assetBundleName;
        
        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.Android);
        Debug.Log("Make one AssetBundle: " + assetBundleName);

        AssetDatabase.Refresh();
    }
    */

    [MenuItem("Tools/AssetBundle/MakeAssetBundle - with script, many target prefabs (v5)")]
    static void CreateNewAssetBundleFromAssetBundleBuild()
    {
        string outputPath = OUTPUT_PATH; //"AssetBundlesFromBuildInfo";
        if (!Directory.Exists(outputPath)) {
            Debug.Log("Directory not exist: " + outputPath);
        }
        
        List<string> targetPrefabList = new List<string>();
        foreach (Object obj in Selection.objects) {
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
        
        BuildPipeline.BuildAssetBundles(outputPath, buildMap, BuildAssetBundleOptions.None, BuildTarget.Android);
        Debug.Log("Make one AssetBundle: " + assetBundleName);

        AssetDatabase.Refresh();
    }

        /* AssetBundleBuild 참고
         * 
            AssetBundleBuild[] buildMap = new[] {
                new AssetBundleBuild { assetBundleName = "test.unity3d", assetNames = new[] { "Assets/Test.prefab"} },
            };
         * 
         * 
            // Create the array of bundle build details.
            AssetBundleBuild[] buildMap = new AssetBundleBuild[2];

            buildMap[0].assetBundleName = "EnemyBundle";
            string[] enemyAssets = new[] { "EnemyAlienShip", "EnemyAlienShipDamaged" };
            buildMap[0].assetNames = enemyAssets;

            buildMap[1].assetBundleName = "HeroBundle";
            string[] heroAssets = new[] { "HeroShip", "HeroShipDamaged" };
            buildMap[1].assetNames = heroAssets;

            // Put the bundles in a folder called "AssetBundles" within the Assets folder.
            BuildPipeline.BuildAssetBundles("Assets/AssetBundles", buildMap, BuildAssetBundleOptions.None);
         * 
         */



    }
