using UnityEngine;
using UnityEngine.UI;

public class ButtonNavMeshHelper : MonoBehaviour
{
    public GameObject parentObject;
    public Button assignedButton; // ��������� ���������� ��� ���������� ������
    private GameObject childObject;

    public GameObject ChildObject => childObject; // ��������� �������� ��� ������� � ��������� �������
    public Color selectedColor = Color.red; // ����, ������� ����� ���������� ��� ��������� ������
    private Button lastSelectedButton; // ��������� ������� ������

    void Start()
    {
        // ������������� �������� ������ �� ������ ����������� ������
        SetTargetToButtonName(assignedButton);

        // ������������� �� ������� ������� ������
        assignedButton.onClick.AddListener(() => SetTargetToButtonName(assignedButton));
    }

    public void SetTargetToButtonName(Button button)
    {
        Transform foundChild = parentObject.transform.Find(button.name);

        if (foundChild != null)
        {
            childObject = foundChild.gameObject;
            Debug.Log("������ �������� ������: " + childObject.name);

            // ��������� ����� ��������� ������� ������
            if (lastSelectedButton != null)
            {
                // ���������� ���� ���������� ������ � ������������
                ChangeButtonColor(lastSelectedButton, Color.white); // ������������� ����������� ����
            }

            // ������������� ���� ��� ������� ������
            ChangeButtonColor(button, selectedColor);

            // ��������� ������� ������ ��� ��������� �������
            lastSelectedButton = button;
        }
        else
        {
            Debug.Log("�������� ������ � ������ " + button.name + " �� ������.");
        }
    }

    private void ChangeButtonColor(Button button, Color color)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color; // ������������� ���� ��������������� ��� ����������� ������
        }
    }
}