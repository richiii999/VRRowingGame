#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class Startup
{
    static Startup()    
    {
        EditorPrefs.SetInt("showCounts_WhiteCity_2", EditorPrefs.GetInt("showCounts_WhiteCity_2") + 1);

        if (EditorPrefs.GetInt("showCounts_WhiteCity_2") == 1)       
        {
            Application.OpenURL("https://assetstore.unity.com/packages/slug/322344");
            // System.IO.File.Delete("Assets/SportCar/Racing_Game.cs");
        }
    }     
}
#endif
