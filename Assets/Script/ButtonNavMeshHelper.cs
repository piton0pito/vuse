using UnityEngine;
using UnityEngine.UI;

public class ButtonNavMeshHelper : MonoBehaviour
{
    public GameObject parentObject;
    private GameObject childObject;

    public GameObject ChildObject => childObject; // Публичное свойство для доступа к дочернему объекту

    public void SetTargetToButtonName(Button button)
    {
        Transform foundChild = parentObject.transform.Find(button.name);

        if (foundChild != null)
        {
            childObject = foundChild.gameObject;
            Debug.Log("Найден дочерний объект: " + childObject.name);
        }
        else
        {
            Debug.Log("Дочерний объект с именем " + button.name + " не найден.");
        }
    }
}