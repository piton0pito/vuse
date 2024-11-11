using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // ������ �� Canvas
    //void Start()
    //{
    //    panel.SetActive(false);
    //}

    // ����� ��� ������������ ��������� Canvas
    public void TogglePanelVisibility()
    {
        if (panel != null)
        {
            // ����������� ���������� Canvas
            panel.SetActive(!panel.activeSelf);
        }
    }
}