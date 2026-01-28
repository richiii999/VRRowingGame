using UnityEngine;
using System.Collections.Generic;

// SoundController.cs: Controls level bkg sounds and refs to sound effects that are called by other objects
// Note: 3D Sound requires 'spatial blend' of an AudioSource to be set to 1 (default 0)
// Make sure splashes are 3D, other sounds can be 2D

public class SoundController : MonoBehaviour{
    // Stores references to each of the AudioSource objects by category
    public List<AudioSource> splashes; 
    public List<AudioSource> bgSounds; 
    public List<AudioSource> cheers; 

    // Only 1 sound effect in its category can play at a time (bg exempt since only one is played via loop)
    public int spamTimer = 120; // Time in frames to delay repeated sounds
    private int splashTimer = 0;
    private int cheerTimer = 0;
    private AudioSource currSound = null; // Ref to the current sound

    public bool BGSoundOnStart = true; // Play BG sound on start?
    public AudioSource BGSound = null; // Specific BG to play, if not set, pick random

    public int c = 0; // tmp
    
    void Start(){
        // Populate the SE and BG lists with their sounds
        Transform SpContainer = transform.Find("Splashes"); // Sensitive names, dont change in editor
        Transform BGContainer = transform.Find("BgSounds"); 
        Transform ChContainer = transform.Find("Cheers"); 
        for (int i = 0; i < SpContainer.childCount; i++) splashes.Add(SpContainer.GetChild(i).gameObject.GetComponent<AudioSource>());
        for (int i = 0; i < BGContainer.childCount; i++) bgSounds.Add(BGContainer.GetChild(i).gameObject.GetComponent<AudioSource>());
        for (int i = 0; i < ChContainer.childCount; i++)   cheers.Add(ChContainer.GetChild(i).gameObject.GetComponent<AudioSource>());

        if (BGSoundOnStart) { // Play BGSound on loop, if none set: pick one randomly
            if (BGSound == null && bgSounds.Count > 0) BGSound = bgSounds[Random.Range(0, bgSounds.Count)];
            if (BGSound){ BGSound.loop = true; BGSound.Play(); }
            else Debug.LogWarning("Unable to find a BGSound!");
        }
    }

    void Update(){
        // DEBUG: Play random splash effect with '['
        if (Input.GetKeyUp(KeyCode.LeftBracket)){
            PlayRandomSound("splash");
        }

        // Decrement timers
        if (cheerTimer > 0) cheerTimer -= 1;
        if (splashTimer > 0) splashTimer -= 1;
    }

    // Plays one of the splash sound effects randomly
    // BUG (bad design): why cant I have a Vector3(0,0,0) as default param? So dumb to separate it to xyz
    public void PlayRandomSound(string category = "none", float x = 0f, float y = 0f, float z = 0f){ 
        currSound = null; // Reset currSound to prevent double play of sound effects
        switch (category){
            case "splash": 
                if (splashTimer == 0) {
                    currSound = splashes[Random.Range(0, splashes.Count)];
                    splashTimer += spamTimer;
                }
                break;
            case "cheer": 
                if (cheerTimer == 0) {
                    currSound = cheers[Random.Range(0, cheers.Count)];
                    cheerTimer += spamTimer;
                }
                break;
            default: Debug.LogWarning("Invalid category of sound!"); break;
        }

        if (currSound != null){
            currSound.gameObject.transform.position = new Vector3(x,y,z); // Positional sound
            currSound.Play();
        }
    }
}
