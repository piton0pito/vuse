using UnityEngine;
using UnityEngine.UI;

public class GameSettingController : MonoBehaviour
{
    public Camera playerCamera; // Ссылка на камеру
    public Joystick joystick; // Ссылка на джойстик

    public Slider sensitivitySlider; // Слайдер для чувствительности
    public Slider fovSlider; // Слайдер для угла обзора
    public Slider joystickSizeSlider; // Слайдер для размера джойстика

    private float mouseSensitivity; // Чувствительность мыши

    void Start()
    {
        // Устанавливаем начальные значения слайдеров
        mouseSensitivity = PlayerPrefs.GetFloat("CameraSensitivity", 2f);
        sensitivitySlider.value = mouseSensitivity;
        fovSlider.value = PlayerPrefs.GetFloat("CameraFOV", 60f);
        joystickSizeSlider.value = PlayerPrefs.GetFloat("JoystickSize", 1f);

        // Применяем настройки при старте
        UpdateCameraSettings();
        UpdateJoystickSize();

        // Подписываемся на события изменения значений слайдеров
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChange);
        fovSlider.onValueChanged.AddListener(OnFOVChange);
        joystickSizeSlider.onValueChanged.AddListener(OnJoystickSizeChange);
    }

    public void OnSensitivityChange(float value)
    {
        mouseSensitivity = value; // Изменяем чувствительность камеры
        PlayerPrefs.SetFloat("CameraSensitivity", value); // Сохраняем значение
        UpdateCameraSettings(); // Обновляем настройки камеры
    }

    public void OnFOVChange(float value)
    {
        playerCamera.fieldOfView = value; // Изменяем угол обзора камеры
        PlayerPrefs.SetFloat("CameraFOV", value); // Сохраняем значение
        UpdateCameraSettings(); // Обновляем настройки камеры
    }

    public void OnJoystickSizeChange(float value)
    {
        RectTransform joystickRect = joystick.GetComponent<RectTransform>();
        joystickRect.localScale = new Vector3(value, value, 1f); // Изменяем размер джойстика
        PlayerPrefs.SetFloat("JoystickSize", value); // Сохраняем значение
        UpdateJoystickSize(); // Обновляем размер джойстика
    }

    public float GetMouseSensitivity()
    {
        return mouseSensitivity; // Метод для получения чувствительности
    }

    private void UpdateCameraSettings()
    {
        playerCamera.fieldOfView = fovSlider.value; // Обновляем угол обзора камеры
    }

    private void UpdateJoystickSize()
    {
        RectTransform joystickRect = joystick.GetComponent<RectTransform>();
        joystickRect.localScale = new Vector3(joystickSizeSlider.value, joystickSizeSlider.value, 1f); // Изменяем размер джойстика
    }
}
