using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    private float xRotation = 0f;

    // ������ �� ������, �� ������� ����� �������������� �������
    public RectTransform[] touchPanels;

    private GameSettingController gameSettingController; // ������ �� CameraSettings

    void Start()
    {
        gameSettingController = FindObjectOfType<GameSettingController>(); // ���� ������ CameraSettings
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
                // ��������, ��������� �� ������� � ����� �� �������
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
        float joystickX = deltaPosition.x * gameSettingController.GetMouseSensitivity() * Time.deltaTime; // ���������� ���������������� �� CameraSettings
        float joystickY = deltaPosition.y * gameSettingController.GetMouseSensitivity() * Time.deltaTime; // ���������� ���������������� �� CameraSettings

        xRotation -= joystickY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * joystickX);
    }

    private RectTransform GetTouchedPanel(Vector2 touchPosition)
    {
        foreach (RectTransform panel in touchPanels)
        {
            // ����������� ������� ������� � ��������� ���������� ������
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, touchPosition, null, out Vector2 localPoint);

            // ���������, ��������� �� ����� ������� ������ ������� ������
            if (panel.rect.Contains(localPoint))
            {
                return panel; // ���������� ������, ���� ������� ������
            }
        }
        return null; // ���� ������� �� ������ �� � ���� �� �������
    }
}