using UnityEngine;

public class PauseMenuMobile : MonoBehaviour
{
    public GameObject pauseMenuUI; // ������������� ����������

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // ������������� ����

        pauseMenuUI.SetActive(true); // ���������� ����� ��� ����������
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // ���������� ���� � ���������� �����

        pauseMenuUI.SetActive(false); // ���������� ����� ��� ����������
    }

    public void PauseResumeLoc()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f; // ������������� ����
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}