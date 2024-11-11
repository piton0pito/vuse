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
    public void SwitchPanel(string panelName)
    {
        // ��������� ������� ������
        mainPanel.SetActive(false);

        // ������� ��������������� ������ �� �����
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

    public void ExitMainPanel(string panelName)
    {
        // ������� ��������������� ������ �� �����
        GameObject panel = System.Array.Find(panels, p => p.name == panelName);
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Panel with name {panelName} not found in the panels array!");
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