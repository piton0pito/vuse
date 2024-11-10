using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    public float mouseSensitivity = 2f; // „увствительность мыши
    private float xRotation = 0f;

    // —сылки на панели, на которых будут обрабатыватьс€ касани€
    public RectTransform[] touchPanels;

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
                // ѕроверка, находитс€ ли касание в одной из панелей
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
        float joystickX = deltaPosition.x * mouseSensitivity * Time.deltaTime; // »спользуем mouseSensitivity
        float joystickY = deltaPosition.y * mouseSensitivity * Time.deltaTime; // »спользуем mouseSensitivity

        xRotation -= joystickY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * joystickX);
    }

    private RectTransform GetTouchedPanel(Vector2 touchPosition)
    {
        foreach (RectTransform panel in touchPanels)
        {
            // ѕреобразуем позицию касани€ в локальные координаты панели
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, touchPosition, null, out Vector2 localPoint);

            // ѕровер€ем, находитс€ ли точка касани€ внутри области панели
            if (panel.rect.Contains(localPoint))
            {
                return panel; // ¬озвращаем панель, если касание внутри
            }
        }
        return null; // ≈сли касание не попало ни в одну из панелей
    }
}