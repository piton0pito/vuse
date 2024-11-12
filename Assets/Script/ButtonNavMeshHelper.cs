using UnityEngine;
using UnityEngine.UI;

public class ButtonNavMeshHelper : MonoBehaviour
{
    public GameObject parentObject;
    public Button assignedButton; // Публичная переменная для назначения кнопки
    private GameObject childObject;

    public GameObject ChildObject => childObject; // Публичное свойство для доступа к дочернему объекту
    public Color selectedColor = Color.red; // Цвет, который будет установлен для выбранной кнопки
    private Button lastSelectedButton; // Последняя нажатая кнопка

    void Start()
    {
        // Устанавливаем дочерний объект на основе назначенной кнопки
        SetTargetToButtonName(assignedButton);

        // Подписываемся на событие нажатия кнопки
        assignedButton.onClick.AddListener(() => SetTargetToButtonName(assignedButton));
    }

    public void SetTargetToButtonName(Button button)
    {
        Transform foundChild = parentObject.transform.Find(button.name);

        if (foundChild != null)
        {
            childObject = foundChild.gameObject;
            Debug.Log("Найден дочерний объект: " + childObject.name);

            // Изменение цвета последней нажатой кнопки
            if (lastSelectedButton != null)
            {
                // Возвращаем цвет предыдущей кнопки к стандартному
                ChangeButtonColor(lastSelectedButton, Color.white); // Устанавливаем стандартный цвет
            }

            // Устанавливаем цвет для текущей кнопки
            ChangeButtonColor(button, selectedColor);

            // Сохраняем текущую кнопку как последнюю нажатую
            lastSelectedButton = button;
        }
        else
        {
            Debug.Log("Дочерний объект с именем " + button.name + " не найден.");
        }
    }

    private void ChangeButtonColor(Button button, Color color)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color; // Устанавливаем цвет непосредственно для изображения кнопки
        }
    }
}