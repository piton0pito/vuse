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
        EnableCanvas();
    }

    // Метод для переключения панелей
    public void SwitchPanel(string panelName)
    {
        // Выключаем главную панель
        mainPanel.SetActive(false);

        // Находим соответствующую панель по имени
        GameObject panel = System.Array.Find(panels, p => p.name == panelName);
        if (panel != null)
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel with name {panelName} not found in the panels array!");
        }
    }

    // Метод для включения Canvas (можно вызывать, когда нужно)
    public void EnableCanvas()
    {
        canvas.gameObject.SetActive(true);
    }
}