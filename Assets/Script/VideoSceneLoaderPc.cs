using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneLoaderPc : MonoBehaviour
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
        // ���������, ���� �� ������ ������� �������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipVideo();
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