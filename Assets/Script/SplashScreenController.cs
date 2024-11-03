using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Ссылка на компонент Video Player
    public string sceneToLoad; // Имя сцены, которую нужно загрузить

    void Start()
    {
        // Начинаем воспроизведение видео
        videoPlayer.Play();
        // Запускаем корутину для загрузки следующей сцены
        StartCoroutine(LoadSceneAfterVideo());
    }

    private IEnumerator LoadSceneAfterVideo()
    {
        // Ждем, пока видео не закончится
        while (videoPlayer.isPlaying)
        {
            yield return null; // Ждем следующего кадра
        }

        // Загружаем следующую сцену
        SceneManager.LoadScene(sceneToLoad);
    }
}