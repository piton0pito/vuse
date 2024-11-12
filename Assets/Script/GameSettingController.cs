using UnityEngine;
using UnityEngine.UI;

public class GameSettingController : MonoBehaviour
{
    public Camera playerCamera; // ������ �� ������
    public Joystick joystick; // ������ �� ��������

    public Slider sensitivitySlider; // ������� ��� ����������������
    public Slider fovSlider; // ������� ��� ���� ������
    public Slider joystickSizeSlider; // ������� ��� ������� ���������

    private float mouseSensitivity; // ���������������� ����

    void Start()
    {
        // ������������� ��������� �������� ���������
        mouseSensitivity = PlayerPrefs.GetFloat("CameraSensitivity", 2f);
        sensitivitySlider.value = mouseSensitivity;
        fovSlider.value = PlayerPrefs.GetFloat("CameraFOV", 60f);
        joystickSizeSlider.value = PlayerPrefs.GetFloat("JoystickSize", 1f);

        // ��������� ��������� ��� ������
        UpdateCameraSettings();
        UpdateJoystickSize();

        // ������������� �� ������� ��������� �������� ���������
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChange);
        fovSlider.onValueChanged.AddListener(OnFOVChange);
        joystickSizeSlider.onValueChanged.AddListener(OnJoystickSizeChange);
    }

    public void OnSensitivityChange(float value)
    {
        mouseSensitivity = value; // �������� ���������������� ������
        PlayerPrefs.SetFloat("CameraSensitivity", value); // ��������� ��������
        UpdateCameraSettings(); // ��������� ��������� ������
    }

    public void OnFOVChange(float value)
    {
        playerCamera.fieldOfView = value; // �������� ���� ������ ������
        PlayerPrefs.SetFloat("CameraFOV", value); // ��������� ��������
        UpdateCameraSettings(); // ��������� ��������� ������
    }

    public void OnJoystickSizeChange(float value)
    {
        RectTransform joystickRect = joystick.GetComponent<RectTransform>();
        joystickRect.localScale = new Vector3(value, value, 1f); // �������� ������ ���������
        PlayerPrefs.SetFloat("JoystickSize", value); // ��������� ��������
        UpdateJoystickSize(); // ��������� ������ ���������
    }

    public float GetMouseSensitivity()
    {
        return mouseSensitivity; // ����� ��� ��������� ����������������
    }

    private void UpdateCameraSettings()
    {
        playerCamera.fieldOfView = fovSlider.value; // ��������� ���� ������ ������
    }

    private void UpdateJoystickSize()
    {
        RectTransform joystickRect = joystick.GetComponent<RectTransform>();
        joystickRect.localScale = new Vector3(joystickSizeSlider.value, joystickSizeSlider.value, 1f); // �������� ������ ���������
    }
}
