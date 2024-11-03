using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ������ �� ��������� Video Player
    public string sceneToLoad; // ��� �����, ������� ����� ���������

    void Start()
    {
        // �������� ��������������� �����
        videoPlayer.Play();
        // ��������� �������� ��� �������� ��������� �����
        StartCoroutine(LoadSceneAfterVideo());
    }

    private IEnumerator LoadSceneAfterVideo()
    {
        // ����, ���� ����� �� ����������
        while (videoPlayer.isPlaying)
        {
            yield return null; // ���� ���������� �����
        }

        // ��������� ��������� �����
        SceneManager.LoadScene(sceneToLoad);
    }
}