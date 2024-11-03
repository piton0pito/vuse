using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Переименовали переменную

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Проверка на нажатие клавиши паузы
        if (Input.GetKeyDown(KeyCode.Escape)) // Замените на вашу клавишу паузы
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
        Time.timeScale = 0f; // Останавливаем игру

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseMenuUI.SetActive(true); // Используем новое имя переменной
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Возвращаем игру в нормальный режим

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenuUI.SetActive(false); // Используем новое имя переменной
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}