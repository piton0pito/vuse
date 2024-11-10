using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; // �� �������� �������� ���� using ��� ������ � UI
using TMPro;

public class NavMeshAgentControllerMobile : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public float stoppingDistance = 0.5f;
    private bool isGo = false;
    private CharacterController characterController;

    public TMP_Dropdown objectDropdown; // ������ �� ��� Dropdown
    public Slider SpeedeAgent; // ������ �� Slider 
    public Button enableButton; // ������ ��� ��������� ���������
    public Button disableButton; // ������ ��� ���������� ���������

    private void Start()
    {
        disableButton.gameObject.SetActive(false);
        characterController = GetComponent<CharacterController>();
        objectDropdown.onValueChanged.AddListener(delegate { SetTarget(objectDropdown.value); });

        SpeedeAgent.value = agent.speed;
        SpeedeAgent.onValueChanged.AddListener(SetSpeedeAgent);

        // ��������� ����������� ������� ��� ������
        enableButton.onClick.AddListener(EnableAgent);
        disableButton.onClick.AddListener(DisableAgent);
    }

    private void Update()
    {
        if (isGo && agent.enabled)
        {
            CheckIfReachedTarget();
        }
    }

    private void SetTarget(int index)
    {
        // �������� ������ �� ������� � ������������� ��� ��� ����
        GameObject selectedObject = GameObject.Find(objectDropdown.options[index].text);
        if (selectedObject != null)
        {
            target = selectedObject.transform; // ������������� ����
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