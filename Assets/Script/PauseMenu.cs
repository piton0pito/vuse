using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // ������������� ����������

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // �������� �� ������� ������� �����
        if (Input.GetKeyDown(KeyCode.Escape)) // �������� �� ���� ������� �����
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // ������������� ����

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseMenuUI.SetActive(true); // ���������� ����� ��� ����������
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // ���������� ���� � ���������� �����

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenuUI.SetActive(false); // ���������� ����� ��� ����������
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}