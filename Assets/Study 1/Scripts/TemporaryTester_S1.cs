using UnityEngine;

public class TemporaryTester_S1 : MonoBehaviour
{
    MainManager_S1 mainManager;
    ParentCanvasControl_S1 CanvasControl;

    [Header("Control Panel")]
    public int InputUIIndex;
    public int InputPatternIndex;

    void Start()
    {
        mainManager = GetComponent<MainManager_S1>();
        InputUIIndex = mainManager.SelectedUIIndex;
        InputPatternIndex = mainManager.ActivePattern;
    }

    void FixedUpdate()
    {
        if (InputUIIndex != mainManager.SelectedUIIndex)
        {
            ResetAll();
            mainManager.UIList[InputUIIndex - 1].SetActive(true);
            
            mainManager.SelectedUIIndex = InputUIIndex;

            CanvasControl = mainManager.UIList[mainManager.SelectedUIIndex - 1].GetComponent<Transform>().GetChild(0).GetComponent<ParentCanvasControl_S1>();
        }

        if (InputPatternIndex != mainManager.ActivePattern)
        {
            ResetAll();
            mainManager.UIList[mainManager.SelectedUIIndex - 1].SetActive(true);

            mainManager.ActivePattern = InputPatternIndex;
        }
    }

    public void ResetAll()
    {
        CanvasControl = mainManager.UIList[mainManager.SelectedUIIndex - 1].GetComponent<Transform>().GetChild(0).GetComponent<ParentCanvasControl_S1>();

        mainManager.ActivePattern = -1;
        mainManager.CompletedCycles = 0;
        mainManager.Bar2IsSelected = false;

        CanvasControl.BufferActive = true;
        CanvasControl.BufferPanel.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            mainManager.UIList[i].SetActive(false);
        }
        for (int i = 0; i < 9; i++)
        {
            CanvasControl.Patterns[i].SetActive(false);
        }
        mainManager.PracticeMode = false;
    }
}
