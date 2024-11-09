using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    public Slider fovSlider; // ������� ��� ���� ������
    public Slider sensitivitySlider; // ������� ��� ���������������� ����
    private string settingsFilePath;

    void Start()
    {
        settingsFilePath = Path.Combine(Application.streamingAssetsPath, "GameSetting.txt");
        LoadSettings();
    }

    public void OnFovSliderChanged(float value)
    {
        // ��������� ���� ������ � FirstPersonController
        FirstPersonController controller = FindObjectOfType<FirstPersonController>();
        if (controller != null)
        {
            controller.cameraFieldOfView = value;
        }

        // ��������� ��������� � ����
        SaveSettings();
    }

    public void OnSensitivitySliderChanged(float value)
    {
        // ��������� ���������������� ���� � FirstPersonController
        FirstPersonController controller = FindObjectOfType<FirstPersonController>();
        if (controller != null)
        {
            controller.lookSpeed = value;
        }

        // ��������� ��������� � ����
        SaveSettings();
    }

    public void UpdateFov(float value)
    {
        fovSlider.value = value; // ��������� �������� ����