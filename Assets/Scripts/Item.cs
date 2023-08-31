using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public float value;
    public string tagline;
    public string description;
    public virtual void Obtain()
    {
        Debug.Log("Got item");
        Manager.Instance.GetItem(this);
    }
}
