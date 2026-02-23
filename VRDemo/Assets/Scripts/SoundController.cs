using UnityEngine;

using static Tools;

// SoundController.cs: Controls level bkg sounds and refs to sound effects that are called by other objects
// Note: 3D Sound requires 'spatial blend' of an AudioSource to be set to 1 (default 0)
// Make sure splashes are 3D, other sounds can be 2D

public class SoundController : MonoBehaviour{
    // Stores references to each of the AudioSource objects (by category)
    AudioSource[] splashes; 
    AudioSource[] bgSounds; 
    AudioSource[] cheers; 

    // Only 1 sound effect in its category can play at a time (bg exempt since only one is played via loop)
    public int spamTimer = 120; // Time in frames to delay repeated sounds
    private int splashTimer = 0;
    private int cheerTimer = 0;
    private AudioSource currSound = null; // Ref to the current sound

    public bool BGSoundOnStart = true; // Play BG sound on start?
    public AudioSource BGSound = null; // Specific BG to play, if not set, pick random
    
    void Start(){
        // Populate the AudioSource arrays with their respective components
        splashes = GetComponentsInChildren<AudioSource>(RefToObj("Splashes")); // Sensitive names, dont change in editor
        bgSounds = GetComponentsInChildren<AudioSource>(RefToObj("BgSounds"));
        cheers = GetComponentsInChildren<AudioSource>(RefToObj("Cheers"));

        if (BGSoundOnStart) { // Play BGSound on loop, if none set: pick one randomly
            if (BGSound == null && bgSounds.Length > 0) BGSound = bgSounds[Random.Range(0, bgSounds.Length)];
            if (BGSound){ BGSound.loop = true; BGSound.Play(); }
            else Debug.LogWarning("Unable to find a BGSound!");
        }
    }

    void Update(){
        if (Input.GetKeyUp(KeyCode.LeftBracket)) PlayRandomSound("splash"); // DEBUG: Splash with '['

        // Decrement spamTimers
        if (cheerTimer > 0) cheerTimer -= 1;
        if (splashTimer > 0) splashTimer -= 1;
    }

    // Plays one of the splash sound effects randomly
    // BUG (bad design): why cant I have a Vector3(0,0,0) as default param? So dumb to separate it to xyz
    public void PlayRandomSound(string category = "none", float x = 0f, float y = 0f, float z = 0f){ 
        currSound = null; // Reset currSound to prevent double play of sound effects
        Debug.Log(category);
        switch (category){
            case "splash": 
                if (splashTimer == 0) {
                    currSound = splashes[Random.Range(0, splashes.Length)];
                    splashTimer += spamTimer;
                }
                break;
            case "cheer": 
                if (cheerTimer == 0) {
                    currSound = cheers[Random.Range(0, cheers.Length)];
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
