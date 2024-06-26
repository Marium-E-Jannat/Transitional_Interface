using System;
using UnityEngine;
using UnityEngine.UI;
using static AutoSwitchControl_S3;

public class SemiAutoSwitchControl_S3 : MonoBehaviour
{
    [Header("References")]
    Master_S3 Master;
    UserConfig_S3 UserConfig;
    public Transform Cam;
    Vector3 viewPos;

    // UI helper and buttons
    Transform UIHelper;
    Button SwitchToAir;
    Button SwitchToDesk;
    Button SwitchToArm;

    [Header("Parameters")]
    public bool DeskAvailable;
    public bool ArmAvailable;
    public CurrentUserAction CurrentAnchor;

    void Start()
    {
        Master = GetComponent<Master_S3>();
        UserConfig = GameObject.FindGameObjectWithTag("Respawn").GetComponent<UserConfig_S3>();

        if (Cam == null)
        {
            Cam = GameObject.FindGameObjectsWithTag("MainCamera")[1].GetComponent<Transform>();
        }

        DeskAvailable = false;
        ArmAvailable = false;

        UIHelper = Master.UIElement.GetChild(0);
    }

    private void OnEnable()
    {
        SwitchToAir = UIHelper.GetChild(0).GetComponent<Button>();
        SwitchToDesk = UIHelper.GetChild(1).GetComponent<Button>();
        SwitchToArm = UIHelper.GetChild(2).GetComponent<Button>();

        SwitchToAir.onClick.AddListener(() => SwitchAnchors(1));
        SwitchToDesk.onClick.AddListener(() => SwitchAnchors(0));
        SwitchToArm.onClick.AddListener(() => SwitchAnchors(2));
    }

    private void Update()
    {
        CheckAvailableAnchors();
        UpdateOptions();

        if (CurrentAnchor == CurrentUserAction.SittingNearDesk)
        {
            Debug.LogError("Desk");
            Master.DockedAnchor = Master.Anchors[1];
            Master.UIElement.position = Master.Anchors[1].transform.position;
            Master.UIElement.eulerAngles = Master.DesktopUIRotation;
            Master.UIElement.localScale = Master.DeskUISize;
            Master.UIElement.parent = null;
        }
        else if (CurrentAnchor == CurrentUserAction.AwayFromDesk)
        {
            Debug.LogError("Air");
            Master.DockedAnchor = Master.Anchors[0];
            Master.UIElement.position = Master.Anchors[0].transform.position;
            Master.UIElement.LookAt(GameObject.FindGameObjectsWithTag("MainCamera")[0].transform);
            Master.UIElement.eulerAngles = Master.UIElement.eulerAngles + Master.FloatingUIRotation;
            Master.UIElement.localScale = Master.FloatingUISize;
            Master.UIElement.parent = null;
        }
        else if (CurrentAnchor == CurrentUserAction.ArmVisible)
        {
            Debug.LogError("Arm");
            Master.DockedAnchor = Master.Anchors[2];
            Master.UIElement.localScale = Master.HandUISize;
            Master.UIElement.parent = Master.DockedAnchor.transform.parent;
            Master.UIElement.localPosition = Master.HandUIPosition;
            Master.UIElement.localEulerAngles = Master.HandUIRotation;
        }
    }

    public void CheckAvailableAnchors()
    {
        viewPos = Cam.gameObject.GetComponent<Camera>().WorldToViewportPoint(Master.Anchors[2].transform.position);
        //Debug.Log(viewPos);

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            ArmAvailable = true;
        }
        else
        {
            ArmAvailable = false;
        }

        if (Math.Abs(Cam.position.z - UserConfig.SitNear.z) < 0.2f && Math.Abs(Cam.position.y - UserConfig.SitNear.y) < 0.1f)
        {
            DeskAvailable = true;
        }
        else
        {
            DeskAvailable = false;
        }
    }

    // Enable and disable buttons for semi auto switching
    public void UpdateOptions()
    {
        if (CurrentAnchor != CurrentUserAction.AwayFromDesk)
        {
            SwitchToAir.interactable = true;
        }
        else if (CurrentAnchor == CurrentUserAction.AwayFromDesk)
        {
            SwitchToAir.interactable = false;
        }

        if (CurrentAnchor != CurrentUserAction.SittingNearDesk && DeskAvailable)
        {
            SwitchToDesk.interactable = true;
        }
        else
        {
            SwitchToDesk.interactable = false;
        }

        if (CurrentAnchor != CurrentUserAction.ArmVisible && ArmAvailable)
        {
            SwitchToArm.interactable = true;
        }
        else
        {
            SwitchToArm.interactable = false;
        }
    }

    // Switch anchors based on the selected buttons
    public void SwitchAnchors(int BtnIdx)
    {
        if (BtnIdx == 0)
        {
            CurrentAnchor = CurrentUserAction.SittingNearDesk;
        }
        else if (BtnIdx == 1)
        {
            CurrentAnchor = CurrentUserAction.AwayFromDesk;
        }
        else if (BtnIdx == 2)
        {
            CurrentAnchor = CurrentUserAction.ArmVisible;
        }
    }
}
