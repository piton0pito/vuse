using UnityEngine;
using UnityEngine.AI;

public class PathVisualizer : MonoBehaviour
{
    public NavMeshAgent agent; // ������ �� NavMeshAgent
    public LineRenderer lineRenderer; // ������ �� LineRenderer ��� ������������ ����

    void Update()
    {
        // ���������, ���������� �� �����
        if (agent == null || lineRenderer == null)
            return;

        // �������� ������� ���� ������
        NavMeshPath path = agent.path;

        // ������������� ���������� ����� � LineRenderer
        lineRenderer.positionCount = path.corners.Length;

        // ��������� ����� ����
        for (int i = 0; i < path.corners.Length; i++)
        {
            lineRenderer.SetPosition(i, path.corners[i]);
        }
    }
}