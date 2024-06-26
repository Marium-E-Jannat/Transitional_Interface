using TMPro;
using UnityEngine;

public class UserConfig_S3 : MonoBehaviour
{
    /// <summary>
    /// For recording the user's coordinates while standing up away from the desk and when sitting close to the desk.
    /// Used for semi auto and auto switching of UI.
    /// 
    /// The parameter is used for controlling the auto switching, while semi auto uses a set of bools to determine which options are available.
    /// </summary>

    [Header("Control Parameters")]
    public bool ConfigMode;
    public bool SitPosRecorded;
    public bool StandPosRecorded;

    [Header("Recorded Vars")]
    public Vector3 SitNear = Vector3.zero;
    public Vector3 StandAway = Vector3.zero;

    [Header("References")]
    public GameObject UI;
    public GameObject MainCam;
    public TextMeshProUGUI ConfigHelper;
    public GameObject HandAnchor;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("Manager Script");

        ConfigHelper = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        ConfigHelper.text = "Please wait for instructions";

        if (MainCam == null)
        {
            MainCam = GameObject.FindGameObjectsWithTag("MainCamera")[1];
        }

        HandAnchor = GameObject.FindGameObjectWithTag("Hand UI");

        ConfigMode = false;
        SitPosRecorded = false;
        StandPosRecorded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ConfigMode)
        {
            UI.SetActive(true);
            ConfigHelper.gameObject.SetActive(false);
            return;
        }
        else if (ConfigMode)
        {
            UI.SetActive(false);
            ConfigHelper.gameObject.SetActive(true);

            if (!SitPosRecorded)
            {
                ConfigHelper.text = "Please sit at a comfortable distance from the desk";
            }
            else if (!StandPosRecorded)
            {
                ConfigHelper.text = "Please stand away from the desk";
            }
            else if (SitPosRecorded && StandPosRecorded)
            {
                ConfigHelper.text = "Configuration complete, begin testing";
            }
        }
    }

    [ContextMenu("Record Sitting Position")]
    public void RecordSitPose()
    {
        SitNear = MainCam.transform.position;
        SitPosRecorded = true;
    }

    [ContextMenu("Record Standing Position")]
    public void RecordStandPose()
    {
        StandAway = MainCam.transform.position;
        StandPosRecorded = true;
    }
}
