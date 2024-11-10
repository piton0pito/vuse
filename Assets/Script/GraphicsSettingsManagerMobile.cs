using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GraphicsSettingsManagerMobile : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown; // Dropdown для выбора качества графики

    void Start()
    {
        // Проверка на null
        if (qualityDropdown == null)
        {
            Debug.LogError("Dropdown для качества графики не назначен в инспекторе!");
            return;
        }

        // Заполняем Dropdown для качества графики
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex); // Сохранение текущего уровня качества
        PlayerPrefs.Save(); // Сохранение PlayerPrefs
    }
}
