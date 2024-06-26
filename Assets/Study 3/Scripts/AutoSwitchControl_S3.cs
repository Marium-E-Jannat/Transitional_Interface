using System;
using UnityEngine;

public class AutoSwitchControl_S3 : MonoBehaviour
{
    [Header("References")]
    Master_S3 Master;
    UserConfig_S3 UserConfig;
    public Transform Cam;
    Vector3 viewPos;

    public enum CurrentUserAction
    {
        AwayFromDesk,
        SittingNearDesk,
        ArmVisible
    }
    public CurrentUserAction UserState;

    // Start is called before the first frame update
    void Start()
    {
        Master = GetComponent<Master_S3>();
        UserConfig = GameObject.FindGameObjectWithTag("Respawn").GetComponent<UserConfig_S3>();

        if (Cam == null)
        {
            Cam = GameObject.FindGameObjectsWithTag("MainCamera")[1].GetComponent<Transform>();
        }
    }

    private void Update()
    {
        AutoSwitchConfigChecker();

        if (UserState == CurrentUserAction.SittingNearDesk)
        {
            Master.UIElement.position = Master.Anchors[1].transform.position;
            Master.UIElement.eulerAngles = Master.DesktopUIRotation;
            Master.UIElement.localScale = Master.DeskUISize;
            Master.UIElement.parent = null;
        }
        else if (UserState == CurrentUserAction.AwayFromDesk)
        {
            Master.UIElement.position = Master.Anchors[0].transform.position;
            Master.UIElement.LookAt(GameObject.FindGameObjectsWithTag("MainCamera")[0].transform);
            Master.UIElement.eulerAngles = Master.UIElement.eulerAngles + Master.FloatingUIRotation;
            Master.UIElement.localScale = Master.FloatingUISize;
            Master.UIElement.parent = null;
        }
        else if (UserState == CurrentUserAction.ArmVisible)
        {
            Master.UIElement.localScale = Master.HandUISize;
            Master.UIElement.parent = Master.Anchors[2].transform.parent;
            Master.UIElement.localPosition = Master.HandUIPosition;
            Master.UIElement.localEulerAngles = Master.HandUIRotation;
        }
    }

    public void AutoSwitchConfigChecker()
    {
        viewPos = Cam.gameObject.GetComponent<Camera>().WorldToViewportPoint(Master.Anchors[2].transform.position);
        Debug.Log(viewPos);

        if (UserState != CurrentUserAction.SittingNearDesk)
        {
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                UserState = CurrentUserAction.ArmVisible;
            }
            else
            {
                UserState = CurrentUserAction.AwayFromDesk;
            }
        }

        if (Math.Abs(Cam.position.z - UserConfig.SitNear.z) < 0.2f)
        {
            if (Math.Abs(Cam.position.y - UserConfig.SitNear.y) < 0.1f)
            {
                UserState = CurrentUserAction.SittingNearDesk;
            }
            else if (Math.Abs(Cam.position.y - UserConfig.SitNear.y) > 0.3f)
            {
                if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
                {
                    return;
                }
                else
                {
                    UserState = CurrentUserAction.AwayFromDesk;
                }
            }
        }
        else if (Math.Abs(Cam.position.z - UserConfig.SitNear.z) > 0.5f)
        {
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                return;
            }
            else
            {
                UserState = CurrentUserAction.AwayFromDesk;
            }
        }
    }
}
