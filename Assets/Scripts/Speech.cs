using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class Speech : Interactable
{
    bool uiHidden;

    [System.Serializable]
    public class Line
    {
        public Character character;
        public string line;
        public UnityEvent lineEvent;
    }
    [System.Serializable]
    public class Chapter
    {
        public string name;
        public UnityEvent chapterSetEvent = new();
        public List<Line> lines = new();
        public UnityEvent chapterEndedEvent = new();
    }
    public List<Chapter> chapters = new();

    Line currentLine;
    int lineIndex = 0;
    Chapter currentChapter;

    [HideInInspector] public Character currentCharacter;

    public UnityEvent EndGame;
    void Start()
    {
        currentChapter = chapters[0];
        currentLine = currentChapter.lines[0];
    }

    public override void Interact()
    {
        if (!SpeechManager.instance.isSet)
        {
            base.Interact();
            UpdateVisuals();
            Debug.Log("InitialInteraction");
        }
        else
        {
            Skip();
            Debug.Log("SkipInteraction");
        }
    }


    public void SwitchChapter(int index)
    {
        currentChapter = chapters[index];
        currentChapter.chapterSetEvent.Invoke();
        lineIndex = 0;
        Debug.Log("Switched chapter to " + currentChapter.name);
        currentLine = currentChapter.lines[0];
    }

    public void UpdateVisuals()
    {
        if (uiHidden)
        {
            return;
        }
        Debug.Log("Invoked line event");
        currentLine.lineEvent.Invoke();
        if (!SpeechManager.instance.isSet)
        {
            SpeechManager.instance.OnSet();
        }
        SpeechManager.instance.currentLine = currentLine;
        SpeechManager.instance.OnSpeak();
    }

    public void NextLine()
    {
        Debug.Log("Next line ran");
        if (lineIndex + 1 <= currentChapter.lines.Count - 1)
        {
            lineIndex++;
            currentLine = currentChapter.lines[lineIndex];
            Debug.Log("Progressed line");
            UpdateVisuals();
        }
        else
        {
            Debug.Log("Chapter end event invoked");
            currentChapter.chapterEndedEvent.Invoke();
            SpeechManager.instance.OnUnset();
        }
    }

    public void Skip()
    {
        if (SpeechManager.instance.isWriting)
        {
            SpeechManager.instance.SkipText();
            if (SpeechManager.instance.arrow != null)
            {
                SpeechManager.instance.arrow.SetActive(true);
            }
            return;
        }
        else
        {
            if (SpeechManager.instance.arrow != null)
            {
                SpeechManager.instance.arrow.SetActive(false);
            }
            NextLine();
        }
    }

    public void ShowUI()
    {
        uiHidden = false;
        UpdateVisuals();
    }

    public void HideUI()
    {
        Debug.Log("Hid ui");
        SpeechManager.instance.OnUnset();
        uiHidden = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}