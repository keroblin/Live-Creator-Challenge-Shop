using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Interactable currentInteractable;
    public GameObject indicator;
    public TextMeshProUGUI label;

    public float maxDistance = 10f;
    Camera cam;
    bool isDown;

    bool canInteract = true;
    void Start()
    {
        cam = Camera.main;
        if (tag != "Interactable")
        {
            tag = "Interactable";
        }
    }

    void Update()
    {
        //input
        if (canInteract)
        {
            if (currentInteractable != null && Input.GetKeyDown(KeyCode.E) && !isDown)
            {
                currentInteractable.Interact();
                isDown = true;
            }
            if(Input.GetKeyUp(KeyCode.E) && isDown)
            {
                isDown = false;
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, maxDistance))
        {
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                if (currentInteractable == null || currentInteractable.gameObject != hit.collider.gameObject)
                {
                    if (currentInteractable) //reset old one
                    {
                        currentInteractable.inArea = false;
                    }
                    currentInteractable = hit.collider.gameObject.GetComponent<Interactable>();
                    currentInteractable.inArea = true;
                    indicator.SetActive(true);
                    label.text = currentInteractable.description;
                }
            }
            else
            {
                if (currentInteractable)
                {
                    currentInteractable.inArea = false;
                }
                indicator.SetActive(false);
                currentInteractable = null;
            }
        }
        else
        {
            if (currentInteractable)
            {
                currentInteractable.inArea = false;

            }
            indicator.SetActive(false);
            currentInteractable = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            if (currentInteractable != null)
            {
                currentInteractable.inArea = true;
                if (currentInteractable.interactOnEnter)
                {
                    currentInteractable.Interact();
                }
            }
            else
            {

            }
            
        }
        
    }
}