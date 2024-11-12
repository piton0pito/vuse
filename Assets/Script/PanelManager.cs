using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels; // Массив панелей
    public GameObject mainPanel; // Ссылка на главную панель
    public GameObject canvas; // Ссылка на Canvas

    private void Start()
    {
        // Выключаем все панели
        DisableAllPanels();
    }

    // Метод для выключения всех панелей
    private void DisableAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        mainPanel.SetActive(true);
        DisableCanvas();
    }

    // Метод для переключения панелей
    public void SwitchPanel(GameObject panel)
    {
        // Выключаем главную панель
        mainPanel.SetActive(false);

        if (panel != null)
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel with name {panel.name} not found in the panels array!");
        }
    }

    public void ExitMainPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Panel with name {panel} not found in the panels array!");
        }

        // Выключаем главную панель
        mainPanel.SetActive(true);
    }

    // Метод для включения Canvas (можно вызывать, когда нужно)
    public void EnableCanvas()
    {
        canvas.gameObject.SetActive(true);
    }

    public void DisableCanvas()
    {
        canvas.gameObject.SetActive(false);
    }
}