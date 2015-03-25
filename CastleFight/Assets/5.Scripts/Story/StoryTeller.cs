using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Xml;

public class StoryTeller : MonoBehaviour
{
    public string dialogueName;
    public Image portrait;
    public Text characterName;
    public Text dialogueText;

    //list character index for editing in inspector
    public List<CharacterIndex> characterIndex;
    //real character list
    Dictionary<string, Character> listCharacter;

    string currentActor;
    Dialogue.Speech currentSpeech;
    Queue<Dialogue.Speech> dialogue;
    
    void Awake()
    {
    }

    void Start()
    {
        Time.timeScale = 0;

        if (characterIndex != null)
        {
            listCharacter = new Dictionary<string, Character>();
            foreach (CharacterIndex entry in characterIndex)
            {
                listCharacter.Add(entry.name, entry.character);
            }
        }
        else
            EndDialogue();

        if (dialogueName != null)
            LoadDialogue(dialogueName);

        if (dialogue != null && dialogue.Count > 0)
        {
            Dialogue.Speech speech = dialogue.Dequeue();

            currentActor = speech.character;
            currentSpeech = speech;

            Character nextActor;
            listCharacter.TryGetValue(currentActor, out nextActor);

            if (nextActor != null)
            {
                ChangeCharacter(nextActor);
            }

            ChangeText(speech.dialogueText);
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogue != null && dialogue.Count > 0)
            {
                Dialogue.Speech speech = dialogue.Dequeue();
                if (!currentActor.Equals(speech.character))
                {
                    currentActor = speech.character;
                    Character nextActor;
                    listCharacter.TryGetValue(currentActor, out nextActor);

                    if (nextActor != null)
                    {
                        ChangeCharacter(nextActor);
                    }
                }

                ChangeText(speech.dialogueText);
            }
            else
                EndDialogue();
        }
    }

    void EndDialogue()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }

    void ChangeCharacter(Character nextCharacter)
    {
        portrait.sprite = nextCharacter.portrait;
        characterName.text = nextCharacter.name;
        LeanTween.alpha(portrait.gameObject.GetComponent<RectTransform>(), 1, 0.5f).setFrom(0).setIgnoreTimeScale(true);
        LeanTween.textAlpha(characterName.gameObject.GetComponent<RectTransform>(), 1, 0.5f).setFrom(0).setIgnoreTimeScale(true);
    }

    void ChangeText(string nextText)
    {
        dialogueText.text = nextText;
        LeanTween.textAlpha(dialogueText.gameObject.GetComponent<RectTransform>(), 1, 0.5f).setFrom(0).setIgnoreTimeScale(true);
    }

    public void LoadDialogue(string dialogueName)
    {
        Dialogue story = new Dialogue(dialogueName);
        this.dialogue = new Queue<Dialogue.Speech>(story.ListSpeech);
    }

    [Serializable]
    public class CharacterIndex
    {
        public string name;
        public Character character;
    }
}
