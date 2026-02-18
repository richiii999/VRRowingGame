using UnityEngine;

// Tools.cs: Collection of useful funcs that are used throughout various scripts

// NOTE: All funcs here should be 'public static fn()' to allow access and use from anywhere. 
// Import can also be 'using static Tools'. Calling a func from tools does not require Tools.fn(), you can just call fn() directly

// TODO: combine more funcs that are duplicated to here.

public static class Tools{

    // Standardized initialization of references to GameObjects/Components in scripts, since we do it so often.
    // Use these instead of manually connecting things in the Editor. 
    // 'mustExist' False: return null if not found (ex. reference may or may not exist, intended). True: LogError and halt
    public static GameObject RefToObj(string path, bool mustExist = true){
        GameObject searchObj = GameObject.Find(path);
        if (searchObj == null) {
            Debug.LogWarning("Could not find Obj: " + path);
            if (mustExist) QuitGame();
        }
        return searchObj;
    }
    public static Component RefToComp<Component>(string path, bool mustExist = true){
        GameObject searchObj = RefToObj(path, mustExist);
        Component searchComp = default; // Components cannot be NULL (even tho it can be idk)
        if (searchObj != null) { // Check to prevent double-warning of null obj (RefToObj() already warns this)
            searchComp = searchObj.GetComponent<Component>();
            if (searchComp == null) {
                Debug.LogWarning("Could not find Component in Obj: " + path); 
                if (mustExist) QuitGame();
            }
        }
        return searchComp;
    }

    // TODO: CP.GetRelativeAngle()); replace with .GRA(A, B, C) with gameobject and vec3 (from position) overloads

    public static void QuitGame(){ // Quits the game, even in the editor
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
