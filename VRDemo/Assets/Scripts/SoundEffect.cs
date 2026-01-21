using UnityEngine;

// Make sure cant spam sound effect

public class SoundEffect : MonoBehaviour{

    public AudioSource soundPlayer = null; // Set in editor

    void Update(){ 
        if (Input.GetKeyUp(KeyCode.LeftBracket)) {
            PlaySound(); 
            Debug.Log("input '['");
        }
    }

    public void PlaySound(){ 
        if (soundPlayer) {
            soundPlayer.Play(); 
            Debug.Log("PlaySound()");
        } 
    }
}
