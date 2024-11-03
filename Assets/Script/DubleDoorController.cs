using UnityEngine;

public class DubleDoorController : MonoBehaviour
{
    public Transform leftDoor; // Ссылка на левую дверь
    public Transform rightDoor; // Ссылка на правую дверь
    public Transform doorFrame; // Ссылка на дверной проем
    public float openAngle = 90f; // Угол открытия двери
    public float distanceThreshold = 3f; // Пороговое расстояние для открытия двери
    public float rotationSpeed = 2f; // Скорость поворота

    private Transform player; // Ссылка на персонажа
    private Quaternion leftDoorClosedRotation;
    private Quaternion rightDoorClosedRotation;
    private Quaternion leftDoorOpenRotation;
    private Quaternion rightDoorOpenRotation;

    void Start()
    {
        // Находим игрока автоматически
        player = GameObject.FindWithTag("Player").transform; // Замените Player на имя вашего класса игрока

        // Сохраняем начальные ротации дверей
        leftDoorClosedRotation = leftDoor.rotation;
        rightDoorClosedRotation = rightDoor.rotation;

        // Устанавливаем открытые позиции (по оси Y)
        leftDoorOpenRotation = leftDoorClosedRotation * Quaternion.Euler(0, 0, openAngle); // Поворот влево
        rightDoorOpenRotation = rightDoorClosedRotation * Quaternion.Euler(0, 0, -openAngle); // Поворот вправо
    }

    void Update()
    {
        // Проверяем, был ли найден игрок
        if (player == null)
        {
            Debug.LogWarning("Player not found!");
            return;
        }

        // Вычисляем расстояние до дверного проема
        float distance = Vector3.Distance(doorFrame.position, player.position);

        // Если персонаж ближе порогового расстояния
        if (distance < distanceThreshold)
        {
            // Открываем двери
            leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, leftDoorOpenRotation, Time.deltaTime * rotationSpeed);
            rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, rightDoorOpenRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            // Закрываем двери
            leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, leftDoorClosedRotation, Time.deltaTime * rotationSpeed);
            rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, rightDoorClosedRotation, Time.deltaTime * rotationSpeed);
        }
    }
}