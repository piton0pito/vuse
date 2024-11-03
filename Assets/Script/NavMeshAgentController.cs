using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; // Не забудьте добавить этот using для работы с UI
using TMPro;

public class NavMeshAgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public float stoppingDistance = 0.5f;
    private bool isPause = false;
    private bool isGo = false;
    private bool isRotateCamera = false;
    private CharacterController characterController;
    private float Rotation = 0f;

    public TMP_Dropdown objectDropdown; // Ссылка на ваш Dropdown
    public Slider SpeedeAgent; // Ссылка на Slider 

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        objectDropdown.onValueChanged.AddListener(delegate { SetTarget(objectDropdown.value); });

        SpeedeAgent.value = agent.speed;

        SpeedeAgent.onValueChanged.AddListener(SetSpeedeAgent);

        Rotation = agent.angularSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isGo)
        {
            EnableAgent();
            isGo = true;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            DisableAgent();
            isGo = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            DisableAgent();
            isPause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGo)
            {
                EnableAgent();
            }
            isPause = false;
        }

        if (isGo && agent.enabled)
        {
            CheckIfReachedTarget();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isRotateCamera)
            {
                agent.angularSpeed = Rotation;
                isRotateCamera = false;
            }
            else
            {
                agent.angularSpeed = 0f;
                isRotateCamera = true;
            }

        }
    }

    private void SetTarget(int index)
    {
        // Получаем объект по индексу и устанавливаем его как цель
        GameObject selectedObject = GameObject.Find(objectDropdown.options[index].text);
        if (selectedObject != null)
        {
            target = selectedObject.transform; // Устанавливаем цель
        }
    }

    private void EnableAgent()
    {
        if (!agent.enabled)
        {
            agent.enabled = true;
            agent.isStopped = false;
            characterController.enabled = false;
            agent.SetDestination(target.position);
        }
    }

    private void DisableAgent()
    {
        if (agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
            characterController.enabled = true;
        }
    }

    private void CheckIfReachedTarget()
    {
        if (agent.remainingDistance <= stoppingDistance && !agent.pathPending)
        {
            Debug.Log("Target reached!");
            DisableAgent();
            isGo = false;
        }
    }

    public void SetSpeedeAgent(float speedeAgent)
    {
        agent.speed = speedeAgent;
        agent.acceleration = speedeAgent * 15;
    }
}