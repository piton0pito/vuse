using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // Ссылка на Canvas
    //void Start()
    //{
    //    panel.SetActive(false);
    //}

    // Метод для переключения состояния Canvas
    public void TogglePanelVisibility()
    {
        if (panel != null)
        {
            // Переключаем активность Canvas
            panel.SetActive(!panel.activeSelf);
        }
    }
}