using System.Security.Cryptography;
using UnityEditor.AssetImporters;
using UnityEngine;

public class ManualSwitchControl_S3 : MonoBehaviour
{
    Master_S3 Master;
    public bool UIIsDocked;

    //BoxCollider collider;
    //LineRenderer LR;

    void Start()
    {
        Master = GetComponent<Master_S3>();
        Master.DockedAnchor = Master.Anchors[0];
        UIIsDocked = true;
        //collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIIsDocked && Master.DockedAnchor == Master.Anchors[0])
        {
            Master.UIElement.position = Master.Anchors[0].transform.position;
            Master.UIElement.LookAt(GameObject.FindGameObjectsWithTag("MainCamera")[0].transform);
            Master.UIElement.eulerAngles = Master.UIElement.eulerAngles + Master.FloatingUIRotation;
            Master.UIElement.localScale = Master.FloatingUISize;
            Master.UIElement.parent = null;
        }
        else if (UIIsDocked && Master.DockedAnchor == Master.Anchors[1])
        {
            Master.UIElement.position = Master.Anchors[1].transform.position;
            Master.UIElement.localScale = Master.DeskUISize;
            Master.UIElement.eulerAngles = Master.DesktopUIRotation;
            Master.UIElement.parent = null;
        }
        else if (UIIsDocked && Master.DockedAnchor == Master.Anchors[2])
        {
            Master.UIElement.localScale = Master.HandUISize;
            Master.UIElement.parent = Master.Anchors[2].transform.parent;
            Master.UIElement.localPosition = Master.HandUIPosition;
            Master.UIElement.localEulerAngles = Master.HandUIRotation;
        }

        if (UIIsDocked == true && Master.DockedAnchor == null)
        {
            Master.DockedAnchor = Master.Anchors[0];
        }

        //if (Master.TransitionMode != Master_S3.SwitchingMode.Manual)
        //{
        //    UIIsDocked = false;
        //    Master.DockedAnchor = null;
        //    Master.UIElement.parent = null;
        //    Master.UIElement.position = Master.Anchors[0].transform.position;
        //}
    }

    //private void OnEnable()
    //{
    //    LR = gameObject.AddComponent<LineRenderer>();
    //}

    //private void OnDisable()
    //{
    //    Destroy(LR);
    //}

    public void MoveUI()
    {
        UIIsDocked = false;
        Master.DockedAnchor = null;
        Master.UIElement.localScale = new Vector3(0.05f, 0.05f, Master.UIElement.localScale.z);
        Master.UIElement.parent = null;
    }

    [ContextMenu("Dock UI")]
    public void DockUI()
    {
        UIIsDocked = true;

        if (Vector3.Distance(Master.Anchors[0].transform.position, Master.UIElement.position) < 0.1)
        {
            Master.DockedAnchor = Master.Anchors[0];
        }
        else if (Vector3.Distance(Master.Anchors[1].transform.position, Master.UIElement.position) < 0.1)
        {
            Master.DockedAnchor = Master.Anchors[1];
        }
        else if (Vector3.Distance(Master.Anchors[2].transform.position, Master.UIElement.position) < 0.1)
        {
            Master.DockedAnchor = Master.Anchors[2];
        }
        else
        {
            Master.DockedAnchor = Master.Anchors[0];
        }
    }

    void highlightAroundCollider()
    {
        //// Get the bounds of the Box Collider in local space
        //Bounds bounds = collider.bounds;

        //// Get the local center and size of the bounding box
        //Vector3 localCenter = bounds.center;
        //Vector3 localSize = bounds.size;

        //// Get the rotation of the collider
        //Quaternion localRotation = collider.transform.localRotation;

        //// Calculate the half extents after considering rotation
        //Vector3 rotatedHalfExtents = localRotation * new Vector3(0.5f * localSize.x, 0.5f * localSize.y, 0.5f * localSize.z);

        //// Calculate the world coordinates for each corner
        //Vector3[] cornersWorld = new Vector3[8];
        //Transform transform = collider.transform;

        //cornersWorld[0] = transform.TransformPoint(localCenter + new Vector3(-rotatedHalfExtents.x, -rotatedHalfExtents.y, -rotatedHalfExtents.z));
        //cornersWorld[1] = transform.TransformPoint(localCenter + new Vector3(-rotatedHalfExtents.x, -rotatedHalfExtents.y, rotatedHalfExtents.z));
        //cornersWorld[2] = transform.TransformPoint(localCenter + new Vector3(-rotatedHalfExtents.x, rotatedHalfExtents.y, rotatedHalfExtents.z));
        //cornersWorld[3] = transform.TransformPoint(localCenter + new Vector3(-rotatedHalfExtents.x, rotatedHalfExtents.y, -rotatedHalfExtents.z));
        //cornersWorld[4] = transform.TransformPoint(localCenter + new Vector3(rotatedHalfExtents.x, rotatedHalfExtents.y, -rotatedHalfExtents.z));
        //cornersWorld[5] = transform.TransformPoint(localCenter + new Vector3(rotatedHalfExtents.x, rotatedHalfExtents.y, rotatedHalfExtents.z));
        //cornersWorld[6] = transform.TransformPoint(localCenter + new Vector3(rotatedHalfExtents.x, -rotatedHalfExtents.y, rotatedHalfExtents.z));
        //cornersWorld[7] = transform.TransformPoint(localCenter + new Vector3(rotatedHalfExtents.x, -rotatedHalfExtents.y, -rotatedHalfExtents.z));

        //LR.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        //LR.startColor = Color.white;
        //LR.endColor = Color.yellow;
        //LR.startWidth = 0.5f;
        //LR.endWidth = 0.5f;
        //LR.alignment = LineAlignment.TransformZ;
        //LR.positionCount = cornersWorld.Length;

        //// Display the coordinates of all eight corner points
        //for (int i = 0; i < cornersWorld.Length; i++)
        //{
        //    Vector3 finalLine = cornersWorld[i];
        //    LR.SetPosition(i, finalLine);
        //}

        //LR.loop = true;
    }
}

