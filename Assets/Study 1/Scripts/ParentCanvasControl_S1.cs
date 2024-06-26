using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentCanvasControl_S1 : MonoBehaviour
{
    /// <summary>
    /// Contains variables that refer to the currently active UI. Doesn't require any more explanation really.
    /// </summary>

    public List<GameObject> Patterns;                                                               // List holding the child patterns

    [Header("References to the buffer objects between 2 patterns")]
    [Tooltip("Buffer panel between each test cycle")] public GameObject BufferPanel;
    [Tooltip("Refers to the button in the buffer panel")] public Button BufferButton;
    [Tooltip("Checks if the buffer panel is currently active")] public bool BufferActive;

    MainManager_S1 manager;

    private void OnEnable()
    {
        manager = GameObject.FindGameObjectWithTag("Manager Script").GetComponent<MainManager_S1>();

        // Fill in the Patterns list with the first 9 panels
        for (int i = 0; i < 9; i++)
        {
            Patterns.Add(transform.GetChild(i).gameObject);
        }

        manager.ActivePattern = -1;

        // The 10th panel is the buffer panel which will be activated between patterns
        BufferPanel = transform.GetChild(9).gameObject;
        BufferButton = BufferPanel.GetComponentInChildren<Button>();

        BufferButton.onClick.AddListener(() => manager.ActivatePattern());

        BufferPanel.SetActive(true);
        BufferActive = true;
    }

    public void PokeTest()
    {
        Debug.LogError("I poek u");
    }
}