using UnityEngine;

public class FirstPersonControllerJoystick : MonoBehaviour
{
    public Camera playerCamera;
    public CharacterController characterController;
    public Joystick joystick; // Ссылка на объект Joystick

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private float ySpeed;
    private bool isGrounded;
    private Vector3 velocity;
    private float xRotation = 0f;

    private SettingsManager settingsManager; // Ссылка на SettingsManager

    void Start()
    {
        settingsManager = FindObjectOfType<SettingsManager>(); // Находим SettingsManager в сцене
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

        // Сброс вертикальной скорости, если персонаж на земле
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // Ввод для движения с джостика
        float moveX = joystick.Horizontal; // Используем горизонтальное значение джойстика
        float moveZ = joystick.Vertical;     // Используем вертикальное значение джойстика

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Определение скорости
        float currentSpeed = Input.GetButton("Fire3") ? runSpeed : walkSpeed; // Используем "Fire3" для бега

        characterController.Move(move * currentSpeed * Time.deltaTime);

        // Применение гравитации
        if (!isGrounded)
        {
            ySpeed += Physics.gravity.y * Time.deltaTime; // Используем стандартную гравитацию Unity
        }
        else
        {
            ySpeed = 0f; // Если на земле, сбрасываем вертикальную скорость
        }

        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        // Ввод для вращения камеры с помощью свайпов
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

        // Установка угла обзора камеры
        playerCamera.fieldOfView = settingsManager.fovSlider.value; // Обновляем угол обзора
    }
}