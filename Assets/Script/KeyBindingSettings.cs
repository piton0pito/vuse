using UnityEngine;
using UnityEngine.UI;
using TMPro; // �� �������� �������� ��� ������ ��� ������������� TextMeshPro
using System.Collections;
using System.IO;

public class KeyBindingSettings : MonoBehaviour
{
    public Button moveForwardButton;
    public Button moveBackwardButton;
    public Button moveLeftButton;
    public Button moveRightButton;
    public Button jumpButton;
    public Button crouchButton;
    public Button runButton;
    public Button flyButton;

    public GameObject settingsPanel; // ��������� ���������� ��� ������

    private string currentAction;
    private InputManager inputManager; // ������ �� InputManager

    public Color originalColor;
    public Color waitingColor = new Color(0.5f, 0.5f, 0.5f); // ����� ������ ���� (����� ���������)

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>(); // ������� InputManager � �����
        if (inputManager == null)
        {
            Debug.LogError("InputManager not found in the scene.");
            return;
        }

        // ��������� ������������ ���� ������
        originalColor = moveForwardButton.GetComponent<Image>().color;

        moveForwardButton.onClick.AddListener(() => StartRebinding("MoveForward", moveForwardButton));
        moveBackwardButton.onClick.AddListener(() => StartRebinding("MoveBackward", moveBackwardButton));
        moveLeftButton.onClick.AddListener(() => StartRebinding("MoveLeft", moveLeftButton));
        moveRightButton.onClick.AddListener(() => StartRebinding("MoveRight", moveRightButton));
        jumpButton.onClick.AddListener(() => StartRebinding("Jump", jumpButton));
        crouchButton.onClick.AddListener(() => StartRebinding("Crouch", crouchButton));
        runButton.onClick.AddListener(() => StartRebinding("Run", runButton));
        flyButton.onClick.AddListener(() => StartRebinding("Fly", flyButton));

        UpdateButtonLabels();
    }

    void OnEnable() // ��������� ����� �� ������� ��� ��������� ������
    {
        if (settingsPanel.activeSelf) // ���������, ������� �� ������
        {
            LoadKeyBindingsFromFile(); // ��������� �������� ������ �� �����
            UpdateButtonLabels();
        }
    }

    void StartRebinding(string action, Button button)
    {
        currentAction = action;
        StartCoroutine(RebindKey(button));
    }

    private IEnumerator RebindKey(Button button)
    {
        // �������� ���� ������ �� ����� ������
        ChangeButtonColor(button, waitingColor);

        // ������� ������� �������
        while (true)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    // ��������� �������� �������
                    inputManager.UpdateKeyBinding(currentAction, key);
                    inputManager.WriteKeyBindingsToFile(); // ���������� ��������� � ����
                    UpdateButtonLabels();
                    ChangeButtonColor(button, originalColor); // ���������� ������������ ����
                    yield break; // ��������� ��������
                }
            }
            yield return null; // ���� ���������� �����
        }
    }

    private void ChangeButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }

    private void UpdateButtonLabels()
    {
        moveForwardButton.GetComponentInChildren<TextMeshProUGUI>().text = inputManager.GetKey("MoveForward").ToString();
        moveBackwardButton.GetComponentInChildren<TextMeshProUGUI>().text = inputManager.GetKey("MoveBackward").ToString();
        moveLeftButton.GetComponentInChildren<TextMeshProUGUI>().text = inputManager.GetKey("MoveLeft").ToString();
        moveRightButton.GetComponentInChildren<TextMeshProUGUI>().text = inputManager.GetKey("MoveRight").ToString();
        jumpButton.GetComponentInChildren<TextMeshProUGUI>().text = inputManager.GetKey("Jump").ToString();
        crouchButton.GetComponentInChildren<TextMeshProUGUI>().text = inputManager.GetKey("Crouch").ToString();
        runButton.GetComponentInChildren<TextMeshProUGUI>().text = inputManager.GetKey("Run").ToString();
        flyButton.GetComponentInChildren<TextMeshProUGUI>().text = inputManager.GetKey("Fly").ToString();
    }

    private void LoadKeyBindingsFromFile()
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
                    inputManager.UpdateKeyBinding(action, key);
                }
            }
        }
        else
        {
            Debug.LogError("KeyBindings.txt not found at: " + filePath);
        }
    }
}