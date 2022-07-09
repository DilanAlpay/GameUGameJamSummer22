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

    public void StartTest()
    {
        StartDialog(testDialog);
    }
    public void StartDialog(Dialog dialog)
    {
        StartCoroutine(DialogCO(dialog));
    } 

    IEnumerator DialogCO(Dialog dialog)
    {
        ShowDialogBox();
        foreach(Line line in dialog.lines)
        {
            SetSpeaker(line.speaker, line.emotion);
            dialogText.text = line.text;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return new WaitForEndOfFrame();
        }
        dialog.OnDialogOver?.Call();
        HideDialogBox();
    }

    private void SetSpeaker(Speaker speaker, SpeakerEmotion emote = SpeakerEmotion.Default, bool isLeftSpeaker = true)
    {
        leftSpeakerImage.sprite = speaker.GetSprite(emote);
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
