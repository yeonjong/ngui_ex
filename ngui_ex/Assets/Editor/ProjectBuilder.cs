using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

class ProjectBuilder {

    static string[] SCENES = FindEnabledEditorScenes();
    static string APP_NAME = "NGUI Example";

    [MenuItem("Tools/BuildAndroid")]
    static void PerformAndroidBuild() {
        string path = EditorUtility.SaveFolderPanel("Select Build Path", "", "");
        if (path.Length != 0) {
            StringBuilder targetFullFilePath = new StringBuilder(path);
            targetFullFilePath.Append('/');
            targetFullFilePath.Append(APP_NAME);
            targetFullFilePath.Append(".apk");
            GenericBuild(SCENES, targetFullFilePath.ToString(), BuildTarget.Android, BuildOptions.None);
        } else {
            Debug.Log("Build Canceled");
        }
    }

    static void GenericBuild(string[] scenes, string targetFilePath, BuildTarget buildTarget, BuildOptions buildOptions) {
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);
        string res = BuildPipeline.BuildPlayer(scenes, targetFilePath, buildTarget, buildOptions);
        if (res.Length > 0) {
            throw new Exception("BuildPlayer failure: " + res);
        } else {
            Debug.Log("Build success. Directory: " + targetFilePath.ToString());
        }
    }

    private static string[] FindEnabledEditorScenes() {
        List<string> editorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
            if (!scene.enabled) continue;
            editorScenes.Add(scene.path);
        }
        return editorScenes.ToArray();
    }

}