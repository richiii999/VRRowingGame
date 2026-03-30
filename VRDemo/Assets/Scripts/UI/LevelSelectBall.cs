using UnityEngine;

// LevelSelectBall: Changes scene when this obj is placed into the LevelSelectBasket
// Should be attached to each level diorama on the level select scene.

public class LevelSelectBall : MonoBehaviour {
    public string Level = ""; // Which scene to go to?

    void Start() { Tools.VerifySceneInBuild(Level); }

    void OnTriggerEnter(Collider other) {
        if (Level != "" && other.CompareTag("LevelSelectBasket")) { // Detect sphere in the box
            Debug.Log("LevelSelectBall Triggered");
            Destroy(other); // Prevent multiple collisions by deleting the ball
            
            Tools.LoadScene(Level);
        }
    }

    // When the player picks up the ball, remove it's spring joint (so it doesnt fly back when they let go early)
    // Note: This should be attached to the XRGrabInteractable's event list, for the Select event
    public void DetachJoint(){ Destroy(gameObject.GetComponent<SpringJoint>()); }
}
