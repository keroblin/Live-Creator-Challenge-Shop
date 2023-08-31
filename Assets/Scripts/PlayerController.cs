using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float xClamp = 0.0f;
    [Range(0f, 1f)] public float sensitivity = 0.5f;
    public float speed;

    [SerializeField] CharacterController chara;
    Controls controls;
    InputAction look;
    InputAction walk;
    InputAction jump;

    Camera cam;
    Vector3 camEuler;
    Vector3 euler;

    public bool canLook = true;
    public bool canWalk = true;

    bool isGrounded;
    private void OnEnable()
    {
    }
    private void Start()
    {
        cam = Camera.main;
        if (controls == null)
        {
            controls = new Controls();
            controls.Normal.Enable();
        }
        look = controls.Normal.Look;
        walk = controls.Normal.Walk;
        SetLookStatus(true);
        SetWalkStatus(true);
    }

    public void SetWalkStatus(bool status)
    {
        if (status)
        {
            walk.Enable();
        }
        else
        {
            walk.Disable();
        }
        canWalk = status;
    }

    public void SetLookStatus(bool status)
    {
        if (status)
        {
            Cursor.lockState = CursorLockMode.Locked;
            look.Enable();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            look.Disable();
        }
        canLook = status;
    }

    void Update()
    {
        if (canLook)
        {
            if (look.triggered)
            {
                Vector2 lookAngle = look.ReadValue<Vector2>(); //mouse delta
                lookAngle.x *= sensitivity; //modulate to sensitivity
                lookAngle.y *= sensitivity;

                camEuler = cam.transform.localEulerAngles; //angles of cam, local so the player has influence
                xClamp -= lookAngle.y; //the clamp value
                camEuler.x -= lookAngle.y;

                euler = transform.eulerAngles;
                euler.y += lookAngle.x;
                transform.rotation = Quaternion.Euler(euler);

                if (xClamp > 90) //if the clamp is above max rotation on x limit
                {
                    xClamp = 90; //set our target rotation to the max allowed
                    camEuler.x = 90;
                }
                else if (xClamp < -90)
                {
                    xClamp = -90;
                    camEuler.x = 270;
                }
                cam.transform.localRotation = Quaternion.Euler(camEuler); //rotate the cam locally

            }
        }
        if (canWalk)
        {

            Vector2 walkDir = walk.ReadValue<Vector2>(); //wasd vector
            Vector3 movement = (transform.forward * walkDir.y + transform.right * walkDir.x).normalized; //direction is our forward vector controlling the walkdir forward and back and right vector controlling side to side

            isGrounded = chara.isGrounded;
            if (isGrounded)
            {
                movement.y = 0f;
            }

            movement.y += -98f * Time.deltaTime;
            chara.Move(movement * speed * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        controls.Normal.Disable();
    }

    public void MoveTo(Vector3 position)
    {
        chara.Move(position);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Interactable"))
        {
            hit.gameObject.SendMessage("PlayerCollision");
        }
    }
}