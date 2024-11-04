using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
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
    public float lookSpeed = 2f;
    public float mouseSensitivity = 2f;

    private Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalHeight = characterController.height;

        LoadKeyBindings();
    }

    void LoadKeyBindings()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "KeyBindings.txt");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string action = parts[0].Trim();
                    KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), parts[1].Trim());
                    keyBindings[action] = key;
                }
            }
        }
        else
        {
            Debug.LogError("KeyBindings.txt not found at: " + filePath);
        }
    }

    void Update()
    {
        // �������� �� �����
        if (FindObjectOfType<PauseMenu>().IsPaused())
        {
            return; // ���������� ����������, ���� ���� �� �����
        }

        if (!characterController.enabled)
        {
            return; // ���������� ����������, ���� CharacterController ��������
        }

        isGrounded = characterController.isGrounded;

        // ����������
        if (Input.GetKey(keyBindings["Crouch"]))
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

        if (Input.GetKey(keyBindings["MoveLeft"])) moveX = -1;
        if (Input.GetKey(keyBindings["MoveRight"])) moveX = 1;
        if (Input.GetKey(keyBindings["MoveForward"])) moveZ = 1;
        if (Input.GetKey(keyBindings["MoveBackward"])) moveZ = -1;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // ����������� ��������
        float currentSpeed = Input.GetKey(keyBindings["Run"]) ? runSpeed : walkSpeed;
        if (Input.GetKey(keyBindings["Crouch"]))
        {
            currentSpeed = crouchSpeed;
        }

        characterController.Move(move * currentSpeed * Time.deltaTime);

        // ������
        if (Input.GetKeyDown(keyBindings["Jump"]) && isGrounded)
        {
            ySpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            // ���������� ����������
            ySpeed += gravity * Time.deltaTime;
            velocity.y = ySpeed;
        }

        characterController.Move(velocity * Time.deltaTime);

        // ���� ��� �������� ������
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}