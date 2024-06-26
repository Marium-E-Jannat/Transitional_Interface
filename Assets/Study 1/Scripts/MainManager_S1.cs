using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager_S1 : MonoBehaviour
{
    /// <summary>
    /// Contains the general controls for the code. Controls the currently active pattern, the number of cycles the user has completed, etc. Pretty straightforward imo.
    /// </summary>

    // Control parameters
    [Header("General Parameters")]
    [Tooltip("The number of times each pattern is to be repeated")] public int RequiredNumOfCycles = 10;
    [Tooltip("The number of times the current pattern has been completed by the user")] public int CompletedCycles = 0;
    [Tooltip("Sets the application to practice mode and user inputs are not logged during testing")] public bool PracticeMode;
    [Tooltip("Helps keep track of which bar is the next target")] public bool Bar2IsSelected;
    [Tooltip("The index of the currently active pattern, ranges from 0 to 8")] public int ActivePattern;

    [Space(8)]

    [Tooltip("List of UIs, fill up in the order mentioned in the <SelectedUIIndex> variable")] public List<GameObject> UIList;
    [Tooltip("Specify the index of the selected UI type:\n" +
        "1 -> Floating UI\n" +
        "2 -> UI fixed on desktop\n" +
        "3 -> UI anchored to left wrist")]
    public int SelectedUIIndex;

    // Declarations for references
    ParentCanvasControl_S1 Canvascontrol;
    CurrentPanelControl_S1 CurrentPanel;

    void Start()
    {
        // Resetting values
        CompletedCycles = 0;
        SelectedUIIndex = 1;
        PracticeMode = true;
        Bar2IsSelected = false;
        ActivePattern = -1;
        Canvascontrol = UIList[0].GetComponent<Transform>().GetChild(0).GetComponent<ParentCanvasControl_S1>();
    }

    void Update()
    {
        // If user has completed the required number of cycles per pattern we change the pattern
        if (CompletedCycles >= RequiredNumOfCycles)
        {
            CompletedCycles = 0;
            Canvascontrol.BufferPanel.SetActive(true);
            Canvascontrol.BufferActive = true;
            Canvascontrol.Patterns[ActivePattern].SetActive(false);
        }

        if (!Canvascontrol.BufferActive)
        {
            CurrentPanel = UIList[SelectedUIIndex - 1].GetComponent<Transform>().GetChild(0).GetChild(ActivePattern).GetComponent<CurrentPanelControl_S1>();
        }
    }

    /// <summary>
    /// Deactivate the current panel, and activate the next panel in the hierarchy.
    /// If the number of completed patterns hits 9 we call the <see cref="FinishSet"/> function.
    /// </summary>
    [ContextMenu("Change Pattern")]
    public void ActivatePattern()
    {
        Canvascontrol.BufferPanel.SetActive(false);

        ActivePattern += 1;

        if (ActivePattern >= 9)
        {
            FinishSet();
        }
        else if (ActivePattern < 9)
        {
            Canvascontrol.Patterns[ActivePattern].SetActive(true);
        }

        Bar2IsSelected = false;
        Canvascontrol.BufferActive = false;

        CurrentPanel = UIList[SelectedUIIndex - 1].GetComponent<Transform>().GetChild(0).GetChild(ActivePattern).GetComponent<CurrentPanelControl_S1>();
    }

    /// <summary>
    /// Disables the current UI and activates the next UI in the list <see cref="ParentCanvasControl_S1.Patterns"/>.
    /// Also changes the CanvasControl variable to refer to the newly activated UI panel.
    /// </summary>
    public void FinishSet()
    {
        UIList[SelectedUIIndex - 1].SetActive(false);
        SelectedUIIndex += 1;
        ActivePattern = -1;

        if (SelectedUIIndex < 4)
        {
            UIList[SelectedUIIndex - 1].SetActive(true);
        }
        else if (SelectedUIIndex > 4)
        {
            return;
        }

        Canvascontrol = UIList[SelectedUIIndex - 1].GetComponent<Transform>().GetChild(0).GetComponent<ParentCanvasControl_S1>();
    }

    [ContextMenu("Execute Click")]
    public void Clicked()
    {
        if (!Canvascontrol.BufferActive)
        {
            if (!Bar2IsSelected)
            {
                CurrentPanel.Bar1.GetComponent<Image>().color = Color.white;
                CurrentPanel.Bar2.GetComponent<Image>().color = Color.blue;
                Bar2IsSelected = true;
            }
            else if (Bar2IsSelected)
            {
                CurrentPanel.Bar2.GetComponent<Image>().color = Color.white;
                CurrentPanel.Bar1.GetComponent<Image>().color = Color.blue;
                Bar2IsSelected = false;

                CompletedCycles += 1;
            }
        }
        else if (Canvascontrol.BufferActive)
        {
            return;
        }

    }

    [ContextMenu("Recenter")]
    public void RecenterView()
    {
        OVRManager.display.RecenterPose();
    }
}