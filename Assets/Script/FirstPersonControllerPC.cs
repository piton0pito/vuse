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

    private InputManager inputManager; // Ссылка на InputManager

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalHeight = characterController.height;
        inputManager = FindObjectOfType<InputManager>();

        // Установка значения поля зрения камеры (можно установить значение по умолчанию)
        playerCamera.fieldOfView = 60f; // Замените на желаемое значение по умолчанию
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

        // Приседание
        if (inputManager.IsKeyPressed("Crouch"))
        {
            characterController.height = crouchedHeight;
        }
        else
        {
            characterController.height = originalHeight;
        }

        // Сброс вертикальной скорости, если персонаж на земле
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // Ввод для движения
        float moveX = 0;
        float moveZ = 0;

        if (inputManager.IsKeyPressed("MoveLeft")) moveX = -1;
        if (inputManager.IsKeyPressed("MoveRight")) moveX = 1;
        if (inputManager.IsKeyPressed("MoveForward")) moveZ = 1;
        if (inputManager.IsKeyPressed("MoveBackward")) moveZ = -1;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Определение скорости
        float currentSpeed = inputManager.IsKeyPressed("Run") ? runSpeed : walkSpeed;
        if (inputManager.IsKeyPressed("Crouch"))
        {
            currentSpeed = crouchSpeed;
        }

        characterController.Move(move * currentSpeed * Time.deltaTime);

        // Прыжок
        if (inputManager.IsKeyDown("Jump") && isGrounded)
        {
            ySpeed = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        else
        {
            // Применение гравитации
            ySpeed += gravity * Time.deltaTime;
            velocity.y = ySpeed;
        }

        characterController.Move(velocity * Time.deltaTime);

        // Ввод для вращения камеры
        float mouseX = Input.GetAxis("Mouse X"); // Убрано умножение на mouseSensitivity
        float mouseY = Input.GetAxis("Mouse Y"); // Убрано умножение на mouseSensitivity

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}