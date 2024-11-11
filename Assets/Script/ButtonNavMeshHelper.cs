using UnityEngine;
using UnityEngine.UI;

public class ButtonNavMeshHelper : MonoBehaviour
{
    public GameObject parentObject;
    private GameObject childObject;

    public GameObject ChildObject => childObject; // ��������� �������� ��� ������� � ��������� �������

    public void SetTargetToButtonName(Button button)
    {
        Transform foundChild = parentObject.transform.Find(button.name);

        if (foundChild != null)
        {
            childObject = foundChild.gameObject;
            Debug.Log("������ �������� ������: " + childObject.name);
        }
        else
        {
            Debug.Log("�������� ������ � ������ " + button.name + " �� ������.");
        }
    }
}