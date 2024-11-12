using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels; // ������ �������
    public GameObject mainPanel; // ������ �� ������� ������
    public GameObject canvas; // ������ �� Canvas

    private void Start()
    {
        // ��������� ��� ������
        DisableAllPanels();
    }

    // ����� ��� ���������� ���� �������
    private void DisableAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        mainPanel.SetActive(true);
        DisableCanvas();
    }

    // ����� ��� ������������ �������
    public void SwitchPanel(GameObject panel)
    {
        // ��������� ������� ������
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

        // ��������� ������� ������
        mainPanel.SetActive(true);
    }

    // ����� ��� ��������� Canvas (����� ��������, ����� �����)
    public void EnableCanvas()
    {
        canvas.gameObject.SetActive(true);
    }

    public void DisableCanvas()
    {
        canvas.gameObject.SetActive(false);
    }
}