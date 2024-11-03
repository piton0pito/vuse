using UnityEngine;
using UnityEngine.AI;

public class PathVisualizer : MonoBehaviour
{
    public NavMeshAgent agent; // Ссылка на NavMeshAgent
    public LineRenderer lineRenderer; // Ссылка на LineRenderer для визуализации пути

    void Update()
    {
        // Проверяем, установлен ли агент
        if (agent == null || lineRenderer == null)
            return;

        // Получаем текущий путь агента
        NavMeshPath path = agent.path;

        // Устанавливаем количество точек в LineRenderer
        lineRenderer.positionCount = path.corners.Length;

        // Заполняем точки пути
        for (int i = 0; i < path.corners.Length; i++)
        {
            lineRenderer.SetPosition(i, path.corners[i]);
        }
    }
}