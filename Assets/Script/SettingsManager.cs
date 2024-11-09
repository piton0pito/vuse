using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    public Slider fovSlider; // Слайдер для угла обзора
    public Slider sensitivitySlider; // Слайдер для чувствительности мыши
    private string settingsFilePath;

    void Start()
    {
        settingsFilePath = Path.Combine(Application.streamingAssetsPath, "GameSetting.txt");
        LoadSettings();
    }

    public void OnFovSliderChanged(float value)
    {
        // Обновляем угол обзора в FirstPersonController
        FirstPersonController controller = FindObjectOfType<FirstPersonController>();
        if (controller != null)
        {
            controller.cameraFieldOfView = value;
        }

        // Сохраняем настройки в файл
        SaveSettings();
    }

    public void OnSensitivitySliderChanged(float value)
    {
        // Обновляем чувствительность мыши в FirstPersonController
        FirstPersonController controller = FindObjectOfType<FirstPersonController>();
        if (controller != null)
        {
            controller.lookSpeed = value;
        }

        // Сохраняем настройки в файл
        SaveSettings();
    }

    public void UpdateFov(float value)
    {
        fovSlider.value = value; // Обновляем значение слай