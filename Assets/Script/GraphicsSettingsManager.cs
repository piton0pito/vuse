using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GraphicsSettingsManager : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Slider resolutionSlider;
    public TMP_Text resolutionText;

    public Button applyButton; // Кнопка "Применить"
    public Button resetButton; // Кнопка "Сбросить"

    private Resolution[] resolutions;

    void Start()
    {
        // Проверка на null
        if (qualityDropdown == null || fullscreenToggle == null || resolutionSlider == null || resolutionText == null || applyButton == null || resetButton == null)
        {
            Debug.LogError("Одно или несколько полей не назначены в инспекторе!");
            return;
        }

        // Получаем доступные разрешения
        resolutions = Screen.resolutions;

        // Заполняем Dropdown для качества графики
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        // Настраиваем состояние полноэкранного режима
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Настраиваем Slider для разрешения
        resolutionSlider.maxValue = resolutions.Length - 1;
        resolutionSlider.value = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length - 1);
        resolutionSlider.onValueChanged.AddListener(SetResolution);
        UpdateResolutionText();

        // Назначаем обработчики событий для кнопок
        applyButton.onClick.AddListener(ApplySettings);
        resetButton.onClick.AddListener(ResetSettings);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(float index)
    {
        int resolutionIndex = (int)index;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        UpdateResolutionText();
    }

    private void UpdateResolutionText()
    {
        int resolutionIndex = (int)resolutionSlider.value;
        Resolution resolution = resolutions[resolutionIndex];
        resolutionText.text = resolution.width + " x " + resolution.height;
    }

    // Метод для применения настроек
    public void ApplySettings()
    {
        // Сохранение текущих настроек
        PlayerPrefs.SetInt("QualityLevel", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionIndex", (int)resolutionSlider.value);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();

        // Применение настроек
        SetQuality(qualityDropdown.value);
        SetFullscreen(fullscreenToggle.isOn);
        SetResolution(resolutionSlider.value);
    }

    // Метод для сброса настроек
    public void ResetSettings()
    {
        // Сброс к начальным настройкам
        qualityDropdown.value = 0; // Установите значение по умолчанию для качества
        fullscreenToggle.isOn = false; // Установите полноэкранный режим по умолчанию
        resolutionSlider.value = resolutions.Length - 1; // Установите максимальное разрешение
        UpdateResolutionText(); // Обновите текст разрешения
    }
}