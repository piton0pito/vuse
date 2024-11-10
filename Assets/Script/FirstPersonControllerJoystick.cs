using UnityEngine;

public class FirstPersonControllerJoystick : MonoBehaviour
{
    public CharacterController characterController;
    public Joystick joystick; // Ссылка на объект Joystick

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private float ySpeed;
    private bool isGrounded;
    private Vector3 velocity;

    void Update()
    {
        if (FindObjectOfType<PauseMenuMobile>().IsPaused())
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
    }
}