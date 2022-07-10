using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] Image leftSpeakerImage;
    [SerializeField] Image rightSpeakerImage;
    [SerializeField] Image speakerNameBox;
    [SerializeField] Text speakerNameText;

    [SerializeField] Dialog testDialog;
    [SerializeField] float lettersPerSecond;
    bool isTyping;
    bool forceComplete = false;

    Action onDialogFinished;


    private void Update()
    {
        if(ShowNextLine())
        {
            ForceCompleteText();
        }
    }

    public void ForceCompleteText()
    {
        if(isTyping && !forceComplete)
        {
            forceComplete = true;
        }

    }

    public bool ShowNextLine()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public void StartTest()
    {
        StartDialog(testDialog);
    }
    public void StartDialog(Dialog dialog, Action onFinished = null)
    {
        onDialogFinished = onFinished;
        StartCoroutine(DialogCO(dialog));
    } 

    IEnumerator DialogCO(Dialog dialog)
    {
        ShowDialogBox();
        foreach(Line line in dialog.lines)
        {
            
            SetSpeaker(line.speaker, line.emotion);
            //dialogText.text = line.text;
            yield return TypewriteDialog(line.text);
            yield return new WaitUntil(() => ShowNextLine());
            yield return new WaitForEndOfFrame();
        }
        dialog.OnDialogOver?.Call();
        onDialogFinished?.Invoke();
        HideDialogBox();
    }

    IEnumerator TypewriteDialog(string message)
    {
        isTyping = true;
        forceComplete = false;
        dialogText.text = "";
        foreach(char letter in message)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
            if (forceComplete)
            {
                dialogText.text = message;
                yield return new WaitUntil(() => ShowNextLine());
                break;
            }
        }
        isTyping = false;
    }

    private void SetSpeaker(Speaker speaker, SpeakerEmotion emote = SpeakerEmotion.Default, bool isLeftSpeaker = true)
    {
        leftSpeakerImage.sprite = speaker.GetSprite(emote);
        leftSpeakerImage.SetNativeSize();
        speakerNameText.text = speaker.speakerName;
        speakerNameBox.color = speaker.nameBoxColor;

    }

    public void SetDialogBoxActive(bool isActive)
    {
        dialogBox.SetActive(isActive);
    }
    public void ShowDialogBox()
    {
        SetDialogBoxActive(true);
    }

    public void HideDialogBox()
    {
        SetDialogBoxActive(false);
    }
}
