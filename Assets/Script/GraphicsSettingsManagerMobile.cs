using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GraphicsSettingsManagerMobile : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown; // Dropdown ��� ������ �������� �������

    void Start()
    {
        // �������� �� null
        if (qualityDropdown == null)
        {
            Debug.LogError("Dropdown ��� �������� ������� �� �������� � ����������!");
            return;
        }

        // ��������� Dropdown ��� �������� �������
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex); // ���������� �������� ������ ��������
        PlayerPrefs.Save(); // ���������� PlayerPrefs
    }
}
