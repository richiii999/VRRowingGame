using UnityEngine;
using UnityEngine.SceneManagement;

// LevelSelectBall.cs: Changes scene when this obj is placed into the LevelSelectBasket
// Should be attached to each level diorama on the level select scene, with the value set to the right scene.

public class LevelSelectBall : MonoBehaviour {
    public string Level = ""; // Which scene to go to?

    void Start() { 
        if (SceneUtility.GetBuildIndexByScenePath(Level) == -1) { // Check if the Scene doesnt exist
            Debug.Log("Scene '" + Level + "' Doesnt exist!"); 
            Level = ""; // Reset Level to "" to prevent errors loading invalid scene
        }
    }

    void OnTriggerEnter(Collider other) {
        if (Level != "" && other.tag == "LevelSelectBasket") { // Detect sphere in the box
            Debug.Log("LevelSelectBall Triggered");
            Destroy(other); // Prevent multiple collisions by deleting the ball
            Debug.Log("Loading Scene: '"+Level+"'");
            SceneManager.LoadSceneAsync(Level);
        }
    }
}
