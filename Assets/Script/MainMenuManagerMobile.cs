using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManagerMobile : MonoBehaviour
{
    private SceneTransition sceneTransition; // Ссылка на SceneTransition

    void Start()
    {
        // Получаем компонент SceneTransition
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void StartGame()
    {
        // Здесь вы можете загрузить сцену с игрой
        sceneTransition.LoadScene("LoadGameScene"); // Замените "GameScene" на имя вашей сцены
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        sceneTransition.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        // Выход из игры
        Application.Quit();
    }
}