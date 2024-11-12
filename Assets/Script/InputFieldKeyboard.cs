using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // �� �������� �������� ��� ������������ ����

public class TMPInputFieldKeyboard : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField tmpInputField; // ������ �� ��� TMP_InputField

    void Start()
    {
        // ���������, ��� TMP_InputField ��������
        if (tmpInputField == null)
        {
            Debug.LogError("TMP_InputField �� �������� � ����������.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ���������� ����������
        ActivateKeyboard();
    }

    private void ActivateKeyboard()
    {
        // ������������� ����� �� TMP_InputField
        tmpInputField.ActivateInputField();
        // ��������� ����������
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}