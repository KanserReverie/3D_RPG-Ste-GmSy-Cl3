using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineOfDialogue
{   
    [TextArea(4,6)]
    public string question, response;

    public float minApproval = -1f;
    public float changeApproval = 0f;

    public Dialogue nextDialogue;

    [System.NonSerialized]
    public int buttonID;
}
