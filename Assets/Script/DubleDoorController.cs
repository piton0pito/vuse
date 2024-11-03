using UnityEngine;

public class DubleDoorController : MonoBehaviour
{
    public Transform leftDoor; // ������ �� ����� �����
    public Transform rightDoor; // ������ �� ������ �����
    public Transform doorFrame; // ������ �� ������� �����
    public float openAngle = 90f; // ���� �������� �����
    public float distanceThreshold = 3f; // ��������� ���������� ��� �������� �����
    public float rotationSpeed = 2f; // �������� ��������

    private Transform player; // ������ �� ���������
    private Quaternion leftDoorClosedRotation;
    private Quaternion rightDoorClosedRotation;
    private Quaternion leftDoorOpenRotation;
    private Quaternion rightDoorOpenRotation;

    void Start()
    {
        // ������� ������ �������������
        player = GameObject.FindWithTag("Player").transform; // �������� Player �� ��� ������ ������ ������

        // ��������� ��������� ������� ������
        leftDoorClosedRotation = leftDoor.rotation;
        rightDoorClosedRotation = rightDoor.rotation;

        // ������������� �������� ������� (�� ��� Y)
        leftDoorOpenRotation = leftDoorClosedRotation * Quaternion.Euler(0, 0, openAngle); // ������� �����
        rightDoorOpenRotation = rightDoorClosedRotation * Quaternion.Euler(0, 0, -openAngle); // ������� ������
    }

    void Update()
    {
        // ���������, ��� �� ������ �����
        if (player == null)
        {
            Debug.LogWarning("Player not found!");
            return;
        }

        // ��������� ���������� �� �������� ������
        float distance = Vector3.Distance(doorFrame.position, player.position);

        // ���� �������� ����� ���������� ����������
        if (distance < distanceThreshold)
        {
            // ��������� �����
            leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, leftDoorOpenRotation, Time.deltaTime * rotationSpeed);
            rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, rightDoorOpenRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            // ��������� �����
            leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, leftDoorClosedRotation, Time.deltaTime * rotationSpeed);
            rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, rightDoorClosedRotation, Time.deltaTime * rotationSpeed);
        }
    }
}