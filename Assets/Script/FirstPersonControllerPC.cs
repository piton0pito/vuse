using System.Collections.Generic;
using UnityEngine;

public class FirstPersonControllerPC : MonoBehaviour
{
    public Camera playerCamera;
    public CharacterController characterController;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpHeight = 1.5f;
    public float gravity = -20f;
    private float ySpeed;
    private bool isGrounded;
    private Vector3 velocity;
    private float xRotation = 0f;
    private float originalHeight;
    public float crouchedHeight = 1f;

    private InputManager inputManager; // ������ �� InputManager

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalHeight = characterController.height;
        inputManager = FindObjectOfType<InputManager>();

        // ��������� �������� ���� ������ ������ (����� ���������� �������� �� ���������)
        playerCamera.fieldOfView = 60f; // �������� �� �������� �������� �� ���������
    }

    void Update()
    {
        if (FindObjectOfType<PauseMenuPc>().IsPaused())
        {
            return;
        }

        if (!characterController.enabled)
        {
            return;
        }

        isGrounded = characterController.isGrounded;

        // ����������
        if (inputManager.IsKeyPressed("Crouch"))
        {
            characterController.height = crouchedHeight;
        }
        else
        {
            characterController.height = originalHeight;
        }

        // ����� ������������ ��������, ���� �������� �� �����
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // ���� ��� ��������
        float moveX = 0;
        float moveZ = 0;

        if (inputManager.IsKeyPressed("MoveLeft")) moveX = -1;
        if (inputManager.IsKeyPressed("MoveRight")) moveX = 1;
        if (inputManager.IsKeyPressed("MoveForward")) moveZ = 1;
        if (inputManager.IsKeyPressed("MoveBackward")) moveZ = -1;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // ����������� ��������
        float currentSpeed = inputManager.IsKeyPressed("Run") ? runSpeed : walkSpeed;
        if (inputManager.IsKeyPressed("Crouch"))
        {
            currentSpeed = crouchSpeed;
        }

        characterController.Move(move * currentSpeed * Time.deltaTime);

        // ������
        if (inputManager.IsKeyDown("Jump") && isGrounded)
        {
            ySpeed = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        else
        {
            // ���������� ����������
            ySpeed += gravity * Time.deltaTime;
            velocity.y = ySpeed;
        }

        characterController.Move(velocity * Time.deltaTime);

        // ���� ��� �������� ������
        float mouseX = Input.GetAxis("Mouse X"); // ������ ��������� �� mouseSensitivity
        float mouseY = Input.GetAxis("Mouse Y"); // ������ ��������� �� mouseSensitivity

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}