using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsPanel; // Панель настроек
    public GameObject settingsPanelGame;
    public GameObject settingsPanelGraphic;
    public GameObject settingsPanelManagement;
    public GameObject BackToSettingMenu;
    public GameObject BackToMainMenuInSetting;

    private SceneTransition sceneTransition; // Ссылка на SceneTransition

    void Start()
    {
        // Скрываем панель настроек при старте
        settingsPanel.SetActive(false);
        settingsPanelGame.SetActive(false);
        settingsPanelGraphic.SetActive(false);
        settingsPanelManagement.SetActive(false);
        BackToSettingMenu.SetActive(false);
        BackToMainMenuInSetting.SetActive(false);

        // Получаем компонент SceneTransition
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void StartGame()
    {
        // Здесь вы можете загрузить сцену с игрой
        sceneTransition.LoadScene("LoadGameScene"); // Замените "GameScene" на имя вашей сцены
    }

    public void OpenSettings()
    {
        // Открываем панель настроек
        settingsPanel.SetActive(true);
        BackToMainMenuInSetting.SetActive(true);
    }

    public void ExitGame()
    {
        // Выход из игры
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        // Возвращаемся в главное меню
        settingsPanel.SetActive(false);
        settingsPanelGame.SetActive(false);
        settingsPanelGraphic.SetActive(false);
        settingsPanelManagement.SetActive(false);

        BackToSettingMenu.SetActive(false);
        BackToMainMenuInSetting.SetActive(false);
    }

    public void SettingGame()
    {
        settingsPanelGraphic.SetActive(false);
        settingsPanelManagement.SetActive(false);
        settingsPanelGame.SetActive(true);

        BackToSettingMenu.SetActive(true);
    }

    public void SettingGraphic()
    {
        settingsPanelGame.SetActive(false);
        settingsPanelManagement.SetActive(false);
        settingsPanelGraphic.SetActive(true);

        BackToSettingMenu.SetActive(true);
    }

    public void SettingManagement()
    {
        settingsPanelGame.SetActive(false);
        settingsPanelGraphic.SetActive(false);
        settingsPanelManagement.SetActive(true);

        BackToSettingMenu.SetActive(true);
    }

    public void BackToSetting()
    {
        settingsPanelGame.SetActive(false);
        settingsPanelGraphic.SetActive(false);
        settingsPanelManagement.SetActive(false);

        BackToSettingMenu.SetActive(false);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        sceneTransition.LoadScene("MainMenu");
    }
}