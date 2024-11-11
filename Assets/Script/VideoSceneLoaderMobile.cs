using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneLoaderMobile : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Ссылка на компонент VideoPlayer
    public string nextSceneName; // Имя следующей сцены для загрузки

    void Start()
    {
        // Подписываемся на событие завершения видео
        videoPlayer.loopPointReached += OnVideoFinished;

        // Запускаем видео
        videoPlayer.Play();
    }

    void Update()
    {
        // Проверяем, было ли касание экрана
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SkipVideo();
            }
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Загружаем следующую сцену
        SceneManager.LoadScene(nextSceneName);
    }

    void SkipVideo()
    {
        // Останавливаем видео
        videoPlayer.Stop();

        // Загружаем следующую сцену
        SceneManager.LoadScene(nextSceneName);
    }
}
