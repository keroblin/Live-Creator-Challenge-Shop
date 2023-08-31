using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Choice : MonoBehaviour
{
    public string question;
    public List<ChoiceOption> options;
    [System.Serializable]
    public class ChoiceOption
    {
        public string optionName;
        public Button button;
        public UnityEvent onChosen;
    }
}
