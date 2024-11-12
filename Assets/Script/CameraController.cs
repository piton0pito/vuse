using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    private float xRotation = 0f;

    // Ссылки на панели, на которых будут обрабатываться касания
    public RectTransform[] touchPanels;

    private GameSettingController gameSettingController; // Ссылка на CameraSettings

    void Start()
    {
        gameSettingController = FindObjectOfType<GameSettingController>(); // Ищем объект CameraSettings
    }

    void Update()
    {
        if (FindObjectOfType<PauseMenuMobile>().IsPaused())
        {
            return;
        }

        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Проверка, находится ли касание в одной из панелей
                RectTransform touchedPanel = GetTouchedPanel(touch.position);
                if (touchedPanel != null)
                {
                    RotateCamera(touch.deltaPosition);
                }
            }
        }
    }

    private void RotateCamera(Vector2 deltaPosition)
    {
        float joystickX = deltaPosition.x * gameSettingController.GetMouseSensitivity() * Time.deltaTime; // Используем чувствительность из CameraSettings
        float joystickY = deltaPosition.y * gameSettingController.GetMouseSensitivity() * Time.deltaTime; // Используем чувствительность из CameraSettings

        xRotation -= joystickY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * joystickX);
    }

    private RectTransform GetTouchedPanel(Vector2 touchPosition)
    {
        foreach (RectTransform panel in touchPanels)
        {
            // Преобразуем позицию касания в локальные координаты панели
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, touchPosition, null, out Vector2 localPoint);

            // Проверяем, находится ли точка касания внутри области панели
            if (panel.rect.Contains(localPoint))
            {
                return panel; // Возвращаем панель, если касание внутри
            }
        }
        return null; // Если касание не попало ни в одну из панелей
    }
}