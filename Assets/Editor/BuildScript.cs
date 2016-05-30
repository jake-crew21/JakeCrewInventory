using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;

using UnityEditor;

public class BuildScript {

    static string[] scenes = FindEnabledEditorScenes();
    static string appName = "Inventory";
    static string targetDir = "Build";

    [MenuItem("Build/Mac OSX")]
    static void MacOSXBuild() {
        string appDir = appName + ".app";
        GenericBuild(scenes, targetDir + "/" + appDir, BuildTarget.StandaloneOSXIntel, BuildOptions.None);
    }

    [MenuItem("Build/Windows (x86)")]
    static void WindowsBuildx86() {
        string appDir = appName + ".exe";
        GenericBuild(scenes, targetDir + "/" + appDir, BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    [MenuItem("Build/Windows (x64)")]
    static void WindowsBuildx64() {
        string appDir = appName + ".exe";
        GenericBuild(scenes, targetDir + "/" + appDir, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes() {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string targetDir,
                             BuildTarget buildTarget, BuildOptions buildOptions) {
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);
        string result = BuildPipeline.BuildPlayer(scenes, targetDir, buildTarget, buildOptions);
        if (result.Length > 0) {
            throw new Exception("BuildPlayer failure: " + result);
        }
    }
}
