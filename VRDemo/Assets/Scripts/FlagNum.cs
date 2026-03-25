using TMPro;
using UnityEngine;

// FlagText: Sets the flag text for boats
// The only reason this is a script is so I can search for all objects with this component and assign
// the same number to flags of each boat (ex. NPC1 has flags '1' '1' NPC2 has flags '2' '2' ...)

public class FlagNum : MonoBehaviour{
    public string initFlagStr = "X"; // Can also customize the text

    void Start(){ SetFlagText(initFlagStr); }

    public void SetFlagText(string newFlagStr) { 
        foreach (TextMeshProUGUI flag in GetComponentsInChildren<TextMeshProUGUI>()) {
            flag.text = newFlagStr;
        }
    }
}
