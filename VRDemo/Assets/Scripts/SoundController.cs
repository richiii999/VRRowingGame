using UnityEngine;
using System.Collections.Generic;

// SoundController.cs: Controls level bkg sounds and refs to sound effects that are called by other objects

public class SoundController : MonoBehaviour{
    public List<AudioSource> soundEffects; // Stores references to each of the AudioSource objects
    public List<AudioSource> bgSounds; 

    public bool BGSoundOnStart = true; // Play BG sound on start?
    public AudioSource BGSound = null; // Specific BG to play, if not set, pick random
    
    void Start(){
        // Populate the SE and BG lists with their sounds
        Transform SEContainer = transform.Find("SoundEffects"); // Sensitive names, dont change in editor
        Transform BGContainer = transform.Find("BgSounds"); 
        Debug.Log((SEContainer.childCount));
        for (int i = 0; i < SEContainer.childCount; i++) soundEffects.Add(SEContainer.GetChild(i).gameObject.GetComponent<AudioSource>());
        for (int i = 0; i < BGContainer.childCount; i++) soundEffects.Add(BGContainer.GetChild(i).gameObject.GetComponent<AudioSource>());


        // Play BGSound on loop, if none set: pick one randomly
        if (BGSound == null && bgSounds.Count > 0) { BGSound = bgSounds[Random.Range(0, bgSounds.Count)]; }
        if (BGSound){ BGSound.loop = true; BGSound.Play(); }
        else Debug.LogWarning("Unable to find a BGSound!");
        
    }

    void Update(){
        
    }
}
