using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManagerMobile : MonoBehaviour
{
    private SceneTransition sceneTransition; // ������ �� SceneTransition

    void Start()
    {
        // �������� ��������� SceneTransition
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        sceneTransition.LoadScene("MainMenu");
    }
}