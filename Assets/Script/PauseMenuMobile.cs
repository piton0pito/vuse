using UnityEngine;

public class PauseMenuMobile : MonoBehaviour
{
    public GameObject pauseMenuUI; // Переименовали переменную

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // Останавливаем игру

        pauseMenuUI.SetActive(true); // Используем новое имя переменной
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Возвращаем игру в нормальный режим

        pauseMenuUI.SetActive(false); // Используем новое имя переменной
    }

    public void PauseResumeLoc()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f; // Останавливаем игру
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