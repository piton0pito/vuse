using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public GameObject panel; // Ссылка на панель
    public float fadeDuration = 1f; // Длительность затемнения

    private void Start()
    {
        // Устанавливаем цвет панели на полностью непрозрачный (100%)
        Image panelImage = panel.GetComponent<Image>();
        Color color = panelImage.color;
        color.a = 1f; // 100% непрозрачность
        panelImage.color = color;

        // Убираем затемнение при старте сцены
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
        panel.SetActive(true); // Активируем панель
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
        panel.SetActive(true); // Активируем панель
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

        // Деактивируем панель, когда она полностью прозрачна
        panel.SetActive(false);
    }
}