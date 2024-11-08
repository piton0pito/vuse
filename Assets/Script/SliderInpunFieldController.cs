using UnityEngine;
using UnityEngine.UI; // Не забудьте добавить этот using для Slider
using TMPro; // Не забудьте добавить этот using для TextMeshPro

public class SliderInpunFieldController : MonoBehaviour
{
    public Slider slider; // Ссылка на компонент Slider
    public TMP_InputField inputField; // Ссылка на компонент InputField

    void Start()
    {
        // Установка начального значения в InputField
        UpdateInputField(slider.value);

        // Подписка на событие изменения значения Slider
        slider.onValueChanged.AddListener(UpdateInputField);

        // Подписка на событие изменения текста в InputField
        inputField.onEndEdit.AddListener(UpdateSliderFromInputField);
    }

    // Метод для обновления InputField в зависимости от значения Slider
    public void UpdateInputField(float value)
    {
        inputField.text = value.ToString("F2"); // Форматирование числа с двумя знаками после запятой
    }

    // Метод для обновления Slider в зависимости от значения InputField
    public void UpdateSliderFromInputField(string input)
    {
        if (float.TryParse(input, out float value))
        {
            slider.value = value; // Обновляем значение Slider
        }
    }
}
