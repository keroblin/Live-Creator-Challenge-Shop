using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void OnInteract();
    public OnInteract Interacted;
    public bool inArea;
    public bool interactOnEnter;
    public string description;

    private void Start()
    {
        if(gameObject.tag != "Interactable")
        {
            gameObject.tag = "Interactable";
        }
    }

    public virtual void Interact()
    {

    }

    public void PlayerCollision()
    {
        if (interactOnEnter)
        {
            Interact();
        }
    }
}
