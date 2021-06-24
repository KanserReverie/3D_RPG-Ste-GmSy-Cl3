using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    Dialogue loadedDialogue;

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform dialogueButtonPanel;
    [SerializeField] Text responseText;


    // Makes a version of this if there is none and deletes it if there is another one
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void LoadDialogue(Dialogue _dialogue)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        loadedDialogue = _dialogue;

        ClearButtons();
        int i = 0;
        Button spawnedButton;


        foreach (LineOfDialogue item in _dialogue.DialogueOptions)
        {
            float? currentApproval = FactionsManager.instance.FactionsApproval(_dialogue.faction);
            if (currentApproval != null && currentApproval > item.minApproval)
            {
                spawnedButton = Instantiate(buttonPrefab, dialogueButtonPanel).GetComponent<Button>();
                spawnedButton.GetComponentInChildren<Text>().text = item.question;

                // i2 will be a differnt instance next loop.
                // it will be "this" instance of i2 but if just i "ButtonPressed(i)" they will all reference the same thing.
                int i2 = i;

                // delegate is a variable that acts like a fuction, button the function can change depending on different factors.
                spawnedButton.onClick.AddListener(delegate
                {
                    ButtonPressed(i2);
                });
            }

            i++;

        }

        Button spanwnedButton = Instantiate(buttonPrefab, dialogueButtonPanel).GetComponent<Button>();
        spanwnedButton.GetComponentInChildren<Text>().text = _dialogue.goodbye.question;


        // delegate is a variable that acts like a fuction, button the function can change depending on different factors.
        spanwnedButton.onClick.AddListener(EndConversation);


        DisplayResponse(_dialogue.greeting);
    }

    void EndConversation()
    {
        ClearButtons();
        DisplayResponse(loadedDialogue.goodbye.response);
        transform.GetChild(0).gameObject.SetActive(false);

        if (loadedDialogue.goodbye.nextDialogue != null)
        {
            LoadDialogue(loadedDialogue.goodbye.nextDialogue);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void ButtonPressed(int _index)
    {
        FactionsManager.instance.FactionsApproval(loadedDialogue.faction, loadedDialogue.DialogueOptions[_index].changeApproval);

        if (loadedDialogue.DialogueOptions[_index] != null)
        {
            // Greetings is response.
            LoadDialogue(loadedDialogue.DialogueOptions[_index].nextDialogue);
        }
        else
        {
            DisplayResponse(loadedDialogue.DialogueOptions[_index].response);
        }
    }

    private void DisplayResponse(string _response)
    {
        responseText.text = _response;
        
    }

    void ClearButtons()
    {
        foreach (Transform child in dialogueButtonPanel)
        {
            Destroy(child.gameObject);
        }
    }
}