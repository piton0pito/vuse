using UnityEngine;

public class FirstPersonControllerJoystick : MonoBehaviour
{
    public Camera playerCamera;
    public CharacterController characterController;
    public Joystick joystick; // ������ �� ������ Joystick

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private float ySpeed;
    private bool isGrounded;
    private Vector3 velocity;
    private float xRotation = 0f;

    private SettingsManager settingsManager; // ������ �� SettingsManager

    void Start()
    {
        settingsManager = FindObjectOfType<SettingsManager>(); // ������� SettingsManager � �����
    }

    void Update()
    {
        if (FindObjectOfType<PauseMenu>().IsPaused())
        {
            return;
        }

        if (!characterController.enabled)
        {
            return;
        }

        isGrounded = characterController.isGrounded;

        // ����� ������������ ��������, ���� �������� �� �����
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // ���� ��� �������� � ��������
        float moveX = joystick.Horizontal; // ���������� �������������� �������� ���������
        float moveZ = joystick.Vertical;     // ���������� ������������ �������� ���������

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // ����������� ��������
        float currentSpeed = Input.GetButton("Fire3") ? runSpeed : walkSpeed; // ���������� "Fire3" ��� ����

        characterController.Move(move * currentSpeed * Time.deltaTime);

        // ���������� ����������
        if (!isGrounded)
        {
            ySpeed += Physics.gravity.y * Time.deltaTime; // ���������� ����������� ���������� Unity
        }
        else
        {
            ySpeed = 0f; // ���� �� �����, ���������� ������������ ��������
        }

        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        // ���� ��� �������� ������ � ������� �������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float joystickX = touch.deltaPosition.x * settingsManager.sensitivitySlider.value * Time.deltaTime;
                float joystickY = touch.deltaPosition.y * settingsManager.sensitivitySlider.value * Time.deltaTime;

                xRotation -= joystickY;
                xRotation = Mathf.Clamp(xRotation, -60f, 60f);
                playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                transform.Rotate(Vector3.up * joystickX);
            }
        }

        // ��������� ���� ������ ������
        playerCamera.fieldOfView = settingsManager.fovSlider.value; // ��������� ���� ������
    }
}