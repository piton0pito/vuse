using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();

    void Start()
    {
        LoadKeyBindings(); // Загрузка привязок клавиш в методе Start
    }

    void LoadKeyBindings()
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
                }
            }
        }
        else
        {
            Debug.LogError("KeyBindings.txt not found at: " + filePath);
        }
    }

    public KeyCode GetKey(string action)
    {
        if (keyBindings.TryGetValue(action, out KeyCode key))
        {
            return key;
        }
        return KeyCode.None; // Возвращаем None, если действие не найдено
    }

    public bool IsKeyPressed(string action)
    {
        return Input.GetKey(GetKey(action));
    }

    public bool IsKeyDown(string action)
    {
        return Input.GetKeyDown(GetKey(action));
    }

    public void UpdateKeyBinding(string action, KeyCode newKey)
    {
        keyBindings[action] = newKey;
        PlayerPrefs.SetString(action, newKey.ToString());
        PlayerPrefs.Save();
    }

    public void WriteKeyBindingsToFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "KeyBindings.txt");

        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            foreach (var binding in keyBindings)
            {
                writer.WriteLine($"{binding.Key}={binding.Value}");
            }
        }
    }
}