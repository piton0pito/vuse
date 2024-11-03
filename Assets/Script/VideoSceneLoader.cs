using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneLoader : MonoBehaviour
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

    void OnVideoFinished(VideoPlayer vp)
    {
        // Загружаем следующую сцену
        SceneManager.LoadScene(nextSceneName);
    }
}