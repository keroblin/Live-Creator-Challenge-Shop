using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : Interactable
{
    public Item item;
    public UnityEvent onPickup;
    
    public override void Interact()
    {
        onPickup.Invoke();
        item.Obtain();

        base.Interact();

        Destroy(gameObject);
    }
}