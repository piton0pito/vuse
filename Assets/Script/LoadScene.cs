using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    public Image slider; // ������ �� UI-������� Image ��� ����������� ��������� ��������
    public string sceneToLoad = "Game"; // ��� �����, ������� �� ���������


    void Start()
    {
        StartCoroutine(LoadSceneGame());
    }

    IEnumerator LoadSceneGame()
    {
        // ��������� ����� ����������
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneToLoad);

        // �������, ���� �������� �� ��������
        while (!loadAsync.isDone)
        {
            // �������� �������� ����������� �� 0 �� 0.9
            float progress = Mathf.Clamp01(loadAsync.progress / 0.9f);
            slider.fillAmount = progress;

            yield return null; // ���� ���������� �����
        }
    }
}