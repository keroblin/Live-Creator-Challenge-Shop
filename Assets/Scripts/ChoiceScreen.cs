using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoiceScreen : MonoBehaviour
{
    //eventually have pooled buttons and get stuff from there
    public static ChoiceScreen instance;

    public GameObject visuals;
    public List<Button> buttons;

    public CursorLockMode lastMode;

    private void Start()
    {
        instance = this;
    }
    

    public void StartChoice(Choice choice)
    {
        lastMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.Confined;
        for(int i= 0; i<choice.options.Count; i++)
        {
            int index = i;
            choice.options[i].button = buttons[i];
            choice.options[index].button.onClick.AddListener(delegate { choice.options[index].onChosen.Invoke(); });
            choice.options[i].button.GetComponentInChildren<TextMeshProUGUI>().text = choice.options[i].optionName;
            choice.options[i].button.gameObject.SetActive(true);
        }

        visuals.SetActive(true);
    }
    public void ChoiceOver()
    {
        foreach(Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
        Cursor.lockState = lastMode;
        visuals.SetActive(false);
    }
}
