using UnityEngine;

public class PauseMenuPc : MonoBehaviour
{
    public GameObject pauseMenuUI; // Переименовали переменную
    private MainMenuManagerPc mainMenuManager;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        mainMenuManager = FindObjectOfType<MainMenuManagerPc>(); // Получаем ссылку на MainMenuManager
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

        // Отключаем панели настроек, если они открыты
        if (mainMenuManager != null)
        {
            mainMenuManager.BackToMainMenu(); // Вызываем метод, чтобы закрыть панели настроек
        }

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