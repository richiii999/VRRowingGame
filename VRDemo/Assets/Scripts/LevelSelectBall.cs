using UnityEngine;
using UnityEngine.SceneManagement;

// LevelSelectBall: Changes scene when this obj is placed into the LevelSelectBasket
// Should be attached to each level diorama on the level select scene.

public class LevelSelectBall : MonoBehaviour {
    public string Level = ""; // Which scene to go to?

    void Start() { // Verify given scene name
        if (SceneUtility.GetBuildIndexByScenePath(Level) == -1) { 
            Debug.Log($"Scene '{Level}' Doesnt exist!"); 
            Level = ""; // Reset Level to "" to prevent errors loading invalid scene
        }
    }

    void OnTriggerEnter(Collider other) {
        if (Level != "" && other.CompareTag("LevelSelectBasket")) { // Detect sphere in the box
            Debug.Log("LevelSelectBall Triggered");
            Destroy(other); // Prevent multiple collisions by deleting the ball
            
            Debug.Log($"Loading Scene: {Level}");
            SceneManager.LoadSceneAsync(Level);
        }
    }

    // When the player picks up the ball, remove it's spring joint (so it doesnt fly back when they let go early)
    public void DetachJoint(){ Destroy(gameObject.GetComponent<SpringJoint>()); }
}
