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

    public Button applyButton; // ������ "���������"
    public Button resetButton; // ������ "��������"

    private Resolution[] resolutions;

    void Start()
    {
        // �������� �� null
        if (qualityDropdown == null || fullscreenToggle == null || resolutionSlider == null || resolutionText == null || applyButton == null || resetButton == null)
        {
            Debug.LogError("���� ��� ��������� ����� �� ��������� � ����������!");
            return;
        }

        // �������� ��������� ����������
        resolutions = Screen.resolutions;

        // ��������� Dropdown ��� �������� �������
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        // ����������� ��������� �������������� ������
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // ����������� Slider ��� ����������
        resolutionSlider.maxValue = resolutions.Length - 1;
        resolutionSlider.value = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length - 1);
        resolutionSlider.onValueChanged.AddListener(SetResolution);
        UpdateResolutionText();

        // ��������� ����������� ������� ��� ������
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

    // ����� ��� ���������� ��������
    public void ApplySettings()
    {
        // ���������� ������� ��������
        PlayerPrefs.SetInt("QualityLevel", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionIndex", (int)resolutionSlider.value);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();

        // ���������� ��������
        SetQuality(qualityDropdown.value);
        SetFullscreen(fullscreenToggle.isOn);
        SetResolution(resolutionSlider.value);
    }

    // ����� ��� ������ ��������
    public void ResetSettings()
    {
        // ����� � ��������� ����������
        qualityDropdown.value = 0; // ���������� �������� �� ��������� ��� ��������
        fullscreenToggle.isOn = false; // ���������� ������������� ����� �� ���������
        resolutionSlider.value = resolutions.Length - 1; // ���������� ������������ ����������
        UpdateResolutionText(); // �������� ����� ����������
    }
}