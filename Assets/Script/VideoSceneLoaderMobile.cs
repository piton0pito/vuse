using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneLoaderMobile : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ������ �� ��������� VideoPlayer
    public string nextSceneName; // ��� ��������� ����� ��� ��������

    void Start()
    {
        // ������������� �� ������� ���������� �����
        videoPlayer.loopPointReached += OnVideoFinished;

        // ��������� �����
        videoPlayer.Play();
    }

    void Update()
    {
        // ���������, ���� �� ������� ������
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
        // ��������� ��������� �����
        SceneManager.LoadScene(nextSceneName);
    }

    void SkipVideo()
    {
        // ������������� �����
        videoPlayer.Stop();

        // ��������� ��������� �����
        SceneManager.LoadScene(nextSceneName);
    }
}
