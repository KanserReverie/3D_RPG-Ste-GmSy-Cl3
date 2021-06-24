using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // Faction of this dialogue.
    public string faction;

    public string greeting;
    public LineOfDialogue goodbye;
    public bool firstDialogue;

    public LineOfDialogue[] DialogueOptions;
    
   // Implement on player movement.
    private void Update()
    {
        if (!firstDialogue) return;
        if(Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.instance.LoadDialogue(this);
        }
    }

}
