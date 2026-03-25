using UnityEngine;
using UnityEngine.SceneManagement;

// SceneChangeButton: Changes the scene to a specific level when pressed (set in the editor)

public class SceneChangeButton : MonoBehaviour{
    public string Level = "MainMenuLevel"; // The level to change when clicking the button 

    public void Start(){
        if (SceneUtility.GetBuildIndexByScenePath(Level) == -1) { // Check if the Scene doesnt exist
            Debug.LogWarning("Scene '" + Level + "' Doesnt exist!"); 
            Level = ""; // Reset Level to "" to prevent errors loading invalid scene
        }
    }

    public void ChangeSceneButton(){
        Debug.Log("Loading Scene: '"+Level+"'");
        SceneManager.LoadSceneAsync(Level);
    }
}
