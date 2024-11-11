using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class NavMeshAgentControllerMobile : MonoBehaviour
{
    public NavMeshAgent agent;
    public float stoppingDistance = 0.5f;
    private bool isGo = false;
    private CharacterController characterController;

    public Button enableButton; // ������ ��� ��������� ���������
    public Button disableButton; // ������ ��� ���������� ���������
    public Slider Speede;
    public ButtonNavMeshHelper buttonNavMeshHelper; // ������ �� ButtonNavMeshHelper
    private int count = 0;

    private void Start()
    {
        disableButton.gameObject.SetActive(false);
        characterController = GetComponent<CharacterController>();

        // ��������� ����������� ������� ��� ������
        enableButton.onClick.AddListener(EnableAgent);
        disableButton.onClick.AddListener(DisableAgent);

        Speede.onValueChanged.AddListener(SetSpeedeAgent);
        Speede.value = agent.speed;
    }

    private void Update()
    {
        if (FindObjectOfType<PauseMenuMobile>().IsPaused() && isGo)
        {
            DisableAgent();
            count = 1;
            isGo = true;
        }

        if (!FindObjectOfType<PauseMenuMobile>().IsPaused() && isGo && count==1)
        {
            EnableAgent();
            count = 0;
        }

        if (isGo && agent.enabled)
        {
            CheckIfReachedTarget();
        }
    }

    private void EnableAgent()
    {
        if (!agent.enabled)
        {
            agent.enabled = true;
            agent.isStopped = false;
            characterController.enabled = false;

            // ������������� ���� �� ButtonNavMeshHelper
            if (buttonNavMeshHelper.ChildObject != null)
            {
                agent.SetDestination(buttonNavMeshHelper.ChildObject.transform.position);
            }

            disableButton.gameObject.SetActive(true);
            isGo = true; // ������������� ����, ��� ����� �������
        }
    }

    private void DisableAgent()
    {
        if (agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
            characterController.enabled = true;
            disableButton.gameObject.SetActive(false);
            isGo = false; // ������������� ����, ��� ����� �� �������
        }
    }

    private void CheckIfReachedTarget()
    {
        if (agent.remainingDistance <= stoppingDistance && !agent.pathPending)
        {
            Debug.Log("Target reached!");
            DisableAgent();
        }
    }

    public void SetSpeedeAgent(float speedeAgent)
    {
        agent.speed = speedeAgent;
        agent.acceleration = speedeAgent * 15;
    }
}