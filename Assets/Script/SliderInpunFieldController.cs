using UnityEngine;
using UnityEngine.UI; // �� �������� �������� ���� using ��� Slider
using TMPro; // �� �������� �������� ���� using ��� TextMeshPro

public class SliderInpunFieldController : MonoBehaviour
{
    public Slider slider; // ������ �� ��������� Slider
    public TMP_InputField inputField; // ������ �� ��������� InputField

    void Start()
    {
        // ��������� ���������� �������� � InputField
        UpdateInputField(slider.value);

        // �������� �� ������� ��������� �������� Slider
        slider.onValueChanged.AddListener(UpdateInputField);

        // �������� �� ������� ��������� ������ � InputField
        inputField.onEndEdit.AddListener(UpdateSliderFromInputField);
    }

    // ����� ��� ���������� InputField � ����������� �� �������� Slider
    public void UpdateInputField(float value)
    {
        inputField.text = value.ToString("F2"); // �������������� ����� � ����� ������� ����� �������
    }

    // ����� ��� ���������� Slider � ����������� �� �������� InputField
    public void UpdateSliderFromInputField(string input)
    {
        if (float.TryParse(input, out float value))
        {
            slider.value = value; // ��������� �������� Slider
        }
    }
}
