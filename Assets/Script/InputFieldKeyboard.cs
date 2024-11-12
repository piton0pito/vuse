using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // Не забудьте добавить это пространство имен

public class TMPInputFieldKeyboard : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField tmpInputField; // Ссылка на ваш TMP_InputField

    void Start()
    {
        // Убедитесь, что TMP_InputField назначен
        if (tmpInputField == null)
        {
            Debug.LogError("TMP_InputField не назначен в инспекторе.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Активируем клавиатуру
        ActivateKeyboard();
    }

    private void ActivateKeyboard()
    {
        // Устанавливаем фокус на TMP_InputField
        tmpInputField.ActivateInputField();
        // Открываем клавиатуру
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}