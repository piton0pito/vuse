using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    public Image slider; // Ссылка на UI-элемент Image для отображения прогресса загрузки
    public string sceneToLoad = "Game"; // Имя сцены, которую мы загружаем


    void Start()
    {
        StartCoroutine(LoadSceneGame());
    }

    IEnumerator LoadSceneGame()
    {
        // Загружаем сцену асинхронно
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneToLoad);

        // Ожидаем, пока загрузка не начнется
        while (!loadAsync.isDone)
        {
            // Прогресс загрузки варьируется от 0 до 0.9
            float progress = Mathf.Clamp01(loadAsync.progress / 0.9f);
            slider.fillAmount = progress;

            yield return null; // Ждем следующего кадра
        }
    }
}