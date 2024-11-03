using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public GameObject panel; // ������ �� ������
    public float fadeDuration = 1f; // ������������ ����������

    private void Start()
    {
        // ������������� ���� ������ �� ��������� ������������ (100%)
        Image panelImage = panel.GetComponent<Image>();
        Color color = panelImage.color;
        color.a = 1f; // 100% ��������������
        panelImage.color = color;

        // ������� ���������� ��� ������ �����
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        yield return FadeOut();
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOut()
    {
        panel.SetActive(true); // ���������� ������
        Image panelImage = panel.GetComponent<Image>();
        Color color = panelImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            panelImage.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        panel.SetActive(true); // ���������� ������
        Image panelImage = panel.GetComponent<Image>();
        Color color = panelImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            panelImage.color = color;
            yield return null;
        }

        // ������������ ������, ����� ��� ��������� ���������
        panel.SetActive(false);
    }
}