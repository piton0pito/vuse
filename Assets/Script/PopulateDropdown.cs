using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PopulateDropdown : MonoBehaviour
{
    public GameObject locations; // ������ �� ������ "locations"
    public TMP_Dropdown dropdown; // ������ �� ��� TMP_Dropdown

    void Start()
    {
        PopulateDropdownList();
    }

    void PopulateDropdownList()
    {
        // �������� ������� �������� � dropdown
        dropdown.ClearOptions();

        // �������� ��� �������� �������
        Transform[] children = locations.GetComponentsInChildren<Transform>();

        // ������� ������ ��� �������� ��������
        List<string> options = new List<string>();

        // ��������� �������� ������� � �������� �� �������� � ������
        foreach (Transform child in children)
        {
            // ���������� ��� ������ "locations"
            if (child != locations.transform)
            {
                options.Add(child.name);
            }
        }

        // �������� ����� � dropdown
        dropdown.AddOptions(options);
    }
}