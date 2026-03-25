using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

// Tools.cs: Collection of useful funcs that are used throughout various scripts

// NOTE: All funcs here should be 'public static fn()' to allow access and use from anywhere. 
// Import can also be 'using static Tools'. If imported, calling a func require Tools.fn(), you can just call fn() directly

public static class Tools{

    // Standardized initialization of references to GameObjects/Components in scripts, since we do it so often.
    // Use these instead of manually connecting things in the Editor. 
    // 'mustExist' False: return null if not found (ex. reference may or may not exist, intended). True: LogError and halt
    public static GameObject RefToObj(string path, bool mustExist = true){
        GameObject searchObj = GameObject.Find(path);

        if (searchObj == null && mustExist) QuitGame($"Could not find required Obj: {path}");

        return searchObj;
    }

    public static Component RefToComp<Component>(string path, bool mustExist = true){
        GameObject searchObj = RefToObj(path, mustExist);
        Component searchComp = default; // Components cannot be NULL (even tho it can be idk)

        if (searchObj != null) { // Check to prevent double-warning of null obj (RefToObj() already warns this)
            searchComp = searchObj.GetComponent<Component>();
            if (searchComp == null && mustExist) QuitGame($"Could not find required Component in Obj: {path}");
        }

        return searchComp;
    }

    // Recursively search for child objects (root is the object to search from, usually the calling object)
    public static GameObject FindChild(GameObject root, string path, bool mustExist = true){
        Transform searchTF = root.transform.Find(path);

        if (searchTF == null && mustExist) QuitGame($"Could not find required child of {root} Obj: {path}");

        return (searchTF == null) ? null : searchTF.gameObject;
    }

    // Gets the relative angle (deg in XZ-plane) of A's fwd to B.
    public static float XZAngleBetween(GameObject A, GameObject B){ 
        Vector2 a = new Vector2(B.transform.forward.x,  B.transform.forward.z).normalized;
        Vector2 c = new Vector2(B.transform.position.x - A.transform.position.x, B.transform.position.z - A.transform.position.z).normalized;

        return Vector2.SignedAngle(a * -1, c);
    }

    public static void QuitGame(string errorStr="Unspecified Error"){ // Quits the game with an error (even in the editor)
        Debug.LogError(errorStr);

        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public static void LoadScene(string sceneName=""){ // Loads scene, even in editor

        Debug.Log($"Loading Scene: {sceneName}");
    
        #if UNITY_STANDALONE
            SceneManager.LoadSceneAsync(sceneName);
        #endif

        #if UNITY_EDITOR
            bool success = false;
            try { EditorSceneManager.OpenScene($"Assets/Scenes/{sceneName}.unity"); success = true; } catch {} // Ignore first fail, try different path
            try { if (!success) EditorSceneManager.OpenScene($"Assets/Scenes/Levels{sceneName}.unity"); } 
            catch (Exception e) { Debug.LogError($"Failed to load Scene: {sceneName}"); Debug.LogError(e.Message); } // only if both fail
        #endif
    }

    public static void VerifySceneInBuild(string sceneName){ // Checks if sceneName is in the build path (required to change scenes at runtime)
        if (SceneUtility.GetBuildIndexByScenePath(sceneName) == -1) {
            Debug.LogWarning($"Scene: {sceneName} not in build path"); 
        }
    }
}
