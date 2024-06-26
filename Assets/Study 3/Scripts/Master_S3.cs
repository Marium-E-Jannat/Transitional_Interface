using Oculus.Interaction.HandGrab;
using System.Collections.Generic;
using UnityEngine;

public class Master_S3 : MonoBehaviour
{
    [Header("UI Sizes")]
    public Vector3 FloatingUISize;
    public Vector3 DeskUISize;
    public Vector3 HandUISize;

    [Header("UI Positions")]
    public Vector3 FloatingUIRotation;
    public Vector3 DesktopUIRotation;
    public Vector3 HandUIRotation;
    public Vector3 HandUIPosition;

    public enum SwitchingMode
    {
        Manual,
        Automatic,
        Semi_Automatic,
        Override
    }
    [Space(7)]
    public SwitchingMode TransitionMode;

    [Space(5)]
    public List<GameObject> Anchors;

    [Header("References")]
    public Transform UIElement;
    public GameObject DockedAnchor = null;
    ManualSwitchControl_S3 manualSwitchControl;
    AutoSwitchControl_S3 autoSwitchControl;
    SemiAutoSwitchControl_S3 semiAutoSwitchControl;
    ControlOverride_S3 controlOverride;
    UserImageSwitch_S3 userImageSwitchControl;

    void Start()
    {
        UIElement = GetComponent<Transform>();
        manualSwitchControl = GetComponent<ManualSwitchControl_S3>();
        autoSwitchControl = GetComponent<AutoSwitchControl_S3>();
        semiAutoSwitchControl = GetComponent<SemiAutoSwitchControl_S3>();
        controlOverride = GetComponent<ControlOverride_S3>();
        userImageSwitchControl = GetComponent<UserImageSwitch_S3>();

        UIElement.localScale = FloatingUISize;
        TransitionMode = SwitchingMode.Manual;

        Anchors.Add(GameObject.FindGameObjectWithTag("Floating UI"));
        Anchors.Add(GameObject.FindGameObjectWithTag("Desktop UI"));
        Anchors.Add(GameObject.FindGameObjectWithTag("Hand UI"));
    }

    void Update()
    {
        if (TransitionMode == SwitchingMode.Manual)
        {
            manualSwitchControl.enabled = true;
            autoSwitchControl.enabled = false;
            semiAutoSwitchControl.enabled = false;
            controlOverride.enabled = false;
            UIElement.GetChild(0).gameObject.SetActive(false);
            userImageSwitchControl.enabled = true;
            gameObject.GetComponent<HandGrabInteractable>().enabled = true;
        }
        else if (TransitionMode == SwitchingMode.Automatic)
        {
            manualSwitchControl.enabled = false;
            autoSwitchControl.enabled = true;
            semiAutoSwitchControl.enabled = false;
            controlOverride.enabled = false;
            UIElement.GetChild(0).gameObject.SetActive(false);
            userImageSwitchControl.enabled = true;
            gameObject.GetComponent<HandGrabInteractable>().enabled = false;
        }
        else if (TransitionMode == SwitchingMode.Semi_Automatic)
        {
            manualSwitchControl.enabled = false;
            autoSwitchControl.enabled = false;
            semiAutoSwitchControl.enabled = true;
            controlOverride.enabled = false;
            UIElement.GetChild(0).gameObject.SetActive(true);
            userImageSwitchControl.enabled = true;
            gameObject.GetComponent<HandGrabInteractable>().enabled = false;
        }
        else if (TransitionMode == SwitchingMode.Override)
        {
            manualSwitchControl.enabled = false;
            autoSwitchControl.enabled = false;
            semiAutoSwitchControl.enabled = false;
            controlOverride.enabled = true;
            UIElement.GetChild(0).gameObject.SetActive(false);
            userImageSwitchControl.enabled = false;
            gameObject.GetComponent<HandGrabInteractable>().enabled = false;
        }
    }
}
