using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechManager : MonoBehaviour
{
    public static SpeechManager instance;
    public GameObject visualsObject;
    public Animator animator;
    public string inAnimName;
    public string outAnimName;
    public TextMeshProUGUI title;
    public TextMeshProUGUI speech;
    public GameObject arrow;
    public Speech.Line currentLine;
    public float speed = .2f;
    [HideInInspector] public float currentSpeed;
    public bool ignoreTitle = false;
    public bool isWriting = false;
    public bool isSet = false;
    Coroutine currentWriter;

    private void OnEnable()
    {
        instance = this;
        visualsObject.SetActive(false);
    }

    public void OnSet()
    {
        if (!visualsObject.activeSelf)
        {
            visualsObject.SetActive(true);

            Debug.Log("Turned on visuals for " + name);
        }
        if (animator)
        {
            animator.Play(inAnimName);
        }
        isSet = true;
        Debug.Log("Set " + name);
    }

    IEnumerator TypeWriter()
    {
        isWriting = true;
        speech.maxVisibleCharacters = 0;
        for (int i = 0; i <= speech.text.Length; i++)
        {
            speech.maxVisibleCharacters = i;
            yield return new WaitForSeconds(currentSpeed);
        }
        isWriting = false;
    }

    public void SkipText()
    {
        speech.maxVisibleCharacters = speech.text.Length;
        if (currentWriter != null)
        {
            StopCoroutine(currentWriter);
        }
        if (arrow != null)
        {
            arrow.SetActive(true);
        }
        Debug.Log("Skipped and stopped writing");
        isWriting = false;
    }

    public void OnSpeak()
    {
        if (!isSet)
        {
            OnSet();
        }
        if (!ignoreTitle)
        {
            if (title.text != currentLine.character.charName)
            {
                title.text = currentLine.character.charName;
                //play anims here
            }
        }
        speech.text = currentLine.line;
        currentWriter = StartCoroutine(TypeWriter());
    }

    public void OnUnset()
    {
        StartCoroutine(Unset());
        isSet = false;
    }

    IEnumerator Unset()
    {
        if (animator)
        {
            animator.Play(outAnimName);
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                yield return null;
            }
        }
        visualsObject.SetActive(false);
        currentLine = null;
        speech.maxVisibleCharacters = 0;
        if (!ignoreTitle)
        {
            title.text = "";
        }
        speech.text = "";
        StopCoroutine(currentWriter);
        Debug.Log("Unset " + name);
    }

    private void OnDisable()
    {
        currentWriter = null;
        StopAllCoroutines();
    }
}