using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class DialogManager : MonoBehaviour
{
    public InputObj inputProceed;
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] List<Image> speakerImages;
    [SerializeField] Image leftSpeakerImage;
    [SerializeField] Image rightSpeakerImage;
    [SerializeField] Image speakerNameBox;
    [SerializeField] Text speakerNameText;

    [SerializeField] Dialog testDialog;
    [SerializeField] float lettersPerSecond;
    [SerializeField] GameEvent onFinish;
    bool isTyping;
    bool forceComplete = false;

    private IEnumerator _coroutine;

    Action onDialogFinished;

    private bool _proceeded = false;
    private Dialog _dialog;

    /// <summary>
    /// Need to save this so we don't set sprites to be null
    /// </summary>
    [SerializeField]
    private Sprite _defaultImg;


    private void Update()
    {
        if(_dialog && _proceeded)
        {
            ForceCompleteText();
        }
    }

    public void ForceCompleteText()
    {
        if(isTyping && !forceComplete)
        {
            _proceeded = false;
            forceComplete = true;           
        }

    }

    /// <summary>
    /// Tells ShowNextLine to proceed with the dialog
    /// </summary>
    /// <param name="ctx"></param>
    private void Proceed(InputAction.CallbackContext ctx)
    {
        _proceeded = true;
    }

    /// <summary>
    /// Used in the coroutine, waits for this to be true
    /// </summary>
    /// <returns></returns>
    public bool ShowNextLine()
    {
        return false;
    }

    public void StartDialog(Dialog dialog)
    {
        if (_coroutine != null) return;
        _dialog = dialog;
        onDialogFinished = null;
        _coroutine = DialogCO();
        inputProceed.Action.started += Proceed;
        StartCoroutine(_coroutine);
    }

    public void StartDialog(Dialog dialog, Action onFinished = null)
    {
        if (_coroutine != null) return;
        _dialog = dialog;
        onDialogFinished = onFinished;
        _coroutine= DialogCO();
        inputProceed.Action.started += Proceed;
        StartCoroutine(_coroutine);
    } 

    IEnumerator DialogCO()
    {
        ShowDialogBox();
        for(int i = 0; i < speakerImages.Count; i++)
        {
            speakerImages[i].gameObject.SetActive(false);
        }
        foreach (Line line in _dialog.lines)
        {            
            SetSpeaker(line.speaker, line.emotion, line.speakerNum);

            yield return TypewriteDialog(line.text);
            yield return new WaitUntil(()=>_proceeded);
            _proceeded = false;


            yield return new WaitForEndOfFrame();
        }
        FinishDialog();
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
              
//              yield return new WaitUntil(() => ShowNextLine());
                break;
            }
        }
        isTyping = false;
    }

    public void FinishDialog()
    {
        _dialog.OnDialogOver?.Call();
        onFinish.Call();
        onDialogFinished?.Invoke();
        inputProceed.Action.started -= Proceed;
        _coroutine = null;
        _dialog = null;
        HideDialogBox();
    }

    private void SetSpeaker(Speaker speaker, SpeakerEmotion emote = SpeakerEmotion.Default, int speakerNum = 0)
    {
        for(int i = 0; i< speakerImages.Count; i++)
        {
            if(speakerNum == i)
            {
                Sprite sprite = speaker.GetSprite(emote);
                speakerImages[i].sprite = sprite == null ? _defaultImg : sprite;
                speakerImages[i].color = sprite == null ? new Color(0,0,0,0) : Color.white;
                speakerImages[i].SetNativeSize();
                speakerImages[i].gameObject.SetActive(true);
                SetSpriteTransparent(speakerImages[i], 1);
                
            }
            else
            {
                //fade image
                SetSpriteTransparent(speakerImages[i], 0f);
            }
        }

        speakerNameText.text = speaker.speakerName;
        speakerNameBox.color = speaker.nameBoxColor;
        AudioClip audio = speaker.GetAudioClip();
        if (audio != null)
        {
            //play sound clip
        }

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

    public void SetSpriteTransparent(Image sprite, float transparency)
    {
        Color tempColor = sprite.color;
        Color newColor = new Color(tempColor.r, tempColor.g, tempColor.b, transparency);
        sprite.color = newColor;
    }
}
