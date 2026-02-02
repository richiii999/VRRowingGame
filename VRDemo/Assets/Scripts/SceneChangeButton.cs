using UnityEngine;
using UnityEngine.SceneManagement;

// SceneChangeButton.cs: Changes the scene to a specific level when pressed (set in the editor)

public class SceneChangeButton : MonoBehaviour{
    public string Level = "MainMenu"; // The level to change when clicking the button 

    public void Start(){
        if (SceneUtility.GetBuildIndexByScenePath(Level) == -1) { // Check if the Scene doesnt exist
            Debug.Log("Scene '" + Level + "' Doesnt exist!"); 
            Level = ""; // Reset Level to "" to prevent errors loading invalid scene
        }
    }

    public void ChangeSceneButton(){
        Debug.Log("Loading Scene: '"+Level+"'");
        SceneManager.LoadSceneAsync(Level);
    }
}
