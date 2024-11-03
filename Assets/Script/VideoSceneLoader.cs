using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneLoader : MonoBehaviour
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

    void OnVideoFinished(VideoPlayer vp)
    {
        // ��������� ��������� �����
        SceneManager.LoadScene(nextSceneName);
    }
}