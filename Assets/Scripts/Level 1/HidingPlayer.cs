using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f; // Mouse sensitivity
    public float smoothing = 2f; // Smoothing factor
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public LayerMask groundLayer;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Transform cameraTransform;

    private Vector2 smoothV;
    private float verticalLookRotation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, controller.height, groundLayer);

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * speed * Time.deltaTime);

        // Camera rotation (mouse input)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * smoothing;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * smoothing;

        smoothV.x = Mathf.Lerp(smoothV.x, mouseX, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseY, 1f / smoothing);
        verticalLookRotation += smoothV.y;

        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
        transform.Rotate(Vector3.up * smoothV.x);

        // Jumping
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}