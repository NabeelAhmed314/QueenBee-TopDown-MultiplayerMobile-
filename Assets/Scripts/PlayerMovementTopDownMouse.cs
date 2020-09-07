using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTopDownMouse : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float rotateSpeed = 3f;
    Vector2 movement;
    Vector2 mousePos;
    public Camera cam;

    public Joystick joystick;
    public FixedButton1 rotateButton;

    private void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        if (rotateButton.Pressed)
        {
            Debug.Log("Rotation");
            transform.Rotate(0f, 0f, rotateSpeed * -1);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }



}
