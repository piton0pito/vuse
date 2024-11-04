using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; // Не забудьте добавить этот using для работы с UI
using TMPro;
using System.Collections.Generic;
using System.IO;

public class NavMeshAgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public float stoppingDistance = 0.5f;
    private bool isGo = false;
    private bool isRotateCamera = false;
    private CharacterController characterController;
    private float Rotation = 0f;

    public TMP_Dropdown objectDropdown; // Ссылка на ваш Dropdown
    public Slider SpeedeAgent; // Ссылка на Slider 

    private Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();
    private PauseMenu pauseMenu; // Ссылка на PauseMenu

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        objectDropdown.onValueChanged.AddListener(delegate { SetTarget(objectDropdown.value); });

        SpeedeAgent.value = agent.speed;
        SpeedeAgent.onValueChanged.AddListener(SetSpeedeAgent);

        Rotation = agent.angularSpeed;

        LoadKeyBindings(); // Загрузка привязок клавиш

        pauseMenu = FindObjectOfType<PauseMenu>(); // Получаем ссылку на PauseMenu
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyBindings["EnableNavigation"]) && !isGo)
        {
            EnableAgent();
            isGo = true;
        }
        else if (Input.GetKeyDown(keyBindings["DisableNavigation"]))
        {
            DisableAgent();
            isGo = false;
        }

        if (FindObjectOfType<PauseMenu>().IsPaused() && isGo)
        {
            DisableAgent();
        }

        if (!FindObjectOfType<PauseMenu>().IsPaused() && isGo)
        {
            EnableAgent();
        }

        if (isGo && agent.enabled)
        {
            CheckIfReachedTarget();
        }

        if (Input.GetKeyDown(keyBindings["ToggleCameraRotation"]))
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

    private void LoadKeyBindings()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "KeyBindings.txt");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string action = parts[0].Trim();
                    KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), parts[1].Trim());
                    keyBindings[action] = key;
                    Debug.Log($"Loaded key binding: {action} = {key}"); // Отладочное сообщение
                }
            }
        }
        else
        {
            Debug.LogError("KeyBindings.txt not found at: " + filePath);
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