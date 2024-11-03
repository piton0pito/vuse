using UnityEngine;
using UnityEngine.UI;
using TMPro; // Не забудьте добавить эту строку для использования TextMeshPro
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

    private string currentAction;

    public Color originalColor;
    public Color waitingColor = new Color(0.5f, 0.5f, 0.5f); // Более темный цвет (можно настроить)

    void Start()
    {
        // Сохраняем оригинальный цвет кнопок
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

    void StartRebinding(string action, Button button)
    {
        currentAction = action;
        StartCoroutine(RebindKey(button));
    }

    private IEnumerator RebindKey(Button button)
    {
        // Изменяем цвет кнопки на более темный
        ChangeButtonColor(button, waitingColor);

        // Ожидаем нажатия клавиши
        yield return new WaitUntil(() => Input.anyKeyDown);

        // Получаем нажатую клавишу
        KeyCode newKey = KeyCode.None;
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                newKey = key;
                break;
            }
        }

        if (newKey != KeyCode.None)
        {
            // Сохраняем новую клавишу
            SaveKeyBinding(currentAction, newKey);
            UpdateButtonLabels();
            WriteKeyBindingsToFile();
        }

        // Восстанавливаем оригинальный цвет кнопки
        ChangeButtonColor(button, originalColor);
    }

    private void ChangeButtonColor(Button button, Color color)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color;
        }
    }

    private void SaveKeyBinding(string action, KeyCode key)
    {
        PlayerPrefs.SetString(action, key.ToString());
        PlayerPrefs.Save();
    }

    private void UpdateButtonLabels()
    {
        if (moveForwardButton != null && moveForwardButton.GetComponentInChildren<TMP_Text>() != null)
            moveForwardButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetString("MoveForward", "W");

        if (moveBackwardButton != null && moveBackwardButton.GetComponentInChildren<TMP_Text>() != null)
            moveBackwardButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetString("MoveBackward", "A");

        if (moveLeftButton != null && moveLeftButton.GetComponentInChildren<TMP_Text>() != null)
            moveLeftButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetString("MoveLeft", "S");

        if (moveRightButton != null && moveRightButton.GetComponentInChildren<TMP_Text>() != null)
            moveRightButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetString("MoveRight", "D");

        if (jumpButton != null && jumpButton.GetComponentInChildren<TMP_Text>() != null)
            jumpButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetString("Jump", "Space");

        if (crouchButton != null && crouchButton.GetComponentInChildren<TMP_Text>() != null)
            crouchButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetString("Crouch", "LeftControl");

        if (runButton != null && runButton.GetComponentInChildren<TMP_Text>() != null)
            runButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetString("Run", "LeftShift");

        if (flyButton != null && flyButton.GetComponentInChildren<TMP_Text>() != null)
            flyButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetString("Fly", "F");
    }

    private void WriteKeyBindingsToFile()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/KeyBindings.txt");

        // Создаем директорию, если она не существует
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine("MoveForward=" + PlayerPrefs.GetString("MoveForward", "W"));
            writer.WriteLine("MoveBackward=" + PlayerPrefs.GetString("MoveBackward", "A"));
            writer.WriteLine("MoveLeft=" + PlayerPrefs.GetString("MoveLeft", "S"));
            writer.WriteLine("MoveRight=" + PlayerPrefs.GetString("MoveRight", "D"));
            writer.WriteLine("Jump=" + PlayerPrefs.GetString("Jump", "Space"));
            writer.WriteLine("Crouch=" + PlayerPrefs.GetString("Crouch", "LeftControl"));
            writer.WriteLine("Run=" + PlayerPrefs.GetString("Run", "LeftShift"));
            writer.WriteLine("Fly=" + PlayerPrefs.GetString("Fly", "F"));
        }
    }
}