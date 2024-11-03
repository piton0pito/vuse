using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsPanel; // ������ ��������
    public GameObject settingsPanelGame;
    public GameObject settingsPanelGraphic;
    public GameObject settingsPanelManagement;
    public GameObject BackToSettingMenu;
    public GameObject BackToMainMenuInSetting;

    private SceneTransition sceneTransition; // ������ �� SceneTransition

    void Start()
    {
        // �������� ������ �������� ��� ������
        settingsPanel.SetActive(false);
        settingsPanelGame.SetActive(false);
        settingsPanelGraphic.SetActive(false);
        settingsPanelManagement.SetActive(false);
        BackToSettingMenu.SetActive(false);
        BackToMainMenuInSetting.SetActive(false);

        // �������� ��������� SceneTransition
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void StartGame()
    {
        // ����� �� ������ ��������� ����� � �����
        sceneTransition.LoadScene("LoadGameScene"); // �������� "GameScene" �� ��� ����� �����
    }

    public void OpenSettings()
    {
        // ��������� ������ ��������
        settingsPanel.SetActive(true);
        BackToMainMenuInSetting.SetActive(true);
    }

    public void ExitGame()
    {
        // ����� �� ����
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        // ������������ � ������� ����
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