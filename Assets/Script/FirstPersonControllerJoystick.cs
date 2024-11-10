using UnityEngine;

public class FirstPersonControllerJoystick : MonoBehaviour
{
    public CharacterController characterController;
    public Joystick joystick; // ������ �� ������ Joystick

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
    }
}