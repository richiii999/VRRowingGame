using UnityEngine;

using static Tools;

// SceneChangeButton: Attach this to a button to change scene to a specific level when pressed (set in the editor)

public class SceneChangeButton : MonoBehaviour{
    public string Level = "MainMenuLevel"; // The level to change when clicking the button 

    public void Start(){ VerifySceneInBuild(Level); }

    public void ChangeSceneButton(){ LoadScene(Level); }
}
