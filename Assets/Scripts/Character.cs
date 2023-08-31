using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character", order = 2)]
public class Character : ScriptableObject
{
    public string charName;
    public Color background;
    public Color text;
}