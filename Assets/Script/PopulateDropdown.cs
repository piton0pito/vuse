using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PopulateDropdown : MonoBehaviour
{
    public GameObject locations; // Ссылка на объект "locations"
    public TMP_Dropdown dropdown; // Ссылка на ваш TMP_Dropdown

    void Start()
    {
        PopulateDropdownList();
    }

    void PopulateDropdownList()
    {
        // Очистить текущие элементы в dropdown
        dropdown.ClearOptions();

        // Получить все дочерние объекты
        Transform[] children = locations.GetComponentsInChildren<Transform>();

        // Создать список для хранения названий
        List<string> options = new List<string>();

        // Перебрать дочерние объекты и добавить их названия в список
        foreach (Transform child in children)
        {
            // Игнорируем сам объект "locations"
            if (child != locations.transform)
            {
                options.Add(child.name);
            }
        }

        // Добавить опции в dropdown
        dropdown.AddOptions(options);
    }
}