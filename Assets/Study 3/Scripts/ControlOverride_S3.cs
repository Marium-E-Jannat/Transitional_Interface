using UnityEngine;

public class ControlOverride_S3 : MonoBehaviour
{
    Master_S3 Master;
    Transform ImageHolder;
    int UICount = 0;

    int ActiveAnchor = 0;
    int ActiveUICount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Master = GetComponent<Master_S3>();
        ImageHolder = transform.GetChild(1);

        UICount = ImageHolder.childCount;

        ActiveAnchor = 0;
        ActiveUICount = 0;
    }

    private void OnEnable()
    {
        for (int i = 0; i < UICount; i++)
        {
            ImageHolder.GetChild(i).gameObject.SetActive(false);
        }
        Master.DockedAnchor = Master.Anchors[ActiveAnchor];
        ImageHolder.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ImageHolder.GetChild(ActiveUICount).gameObject.SetActive(false);

            if (ActiveUICount > 0)
            {
                ActiveUICount--;
            }
            else if (ActiveUICount == 0)
            {
                ActiveUICount = UICount - 1;
            }

            ImageHolder.GetChild(ActiveUICount).gameObject.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ImageHolder.GetChild(ActiveUICount).gameObject.SetActive(false);

            if (ActiveUICount < UICount - 1)
            {
                ActiveUICount++;
            }
            else if (ActiveUICount == UICount - 1)
            {
                ActiveUICount = 0;
            }

            ImageHolder.GetChild(ActiveUICount).gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Master.DockedAnchor != Master.Anchors[2])
            {
                ActiveAnchor++;
            }
            else if (Master.DockedAnchor == Master.Anchors[2])
            {
                ActiveAnchor = 0;
            }

            Master.DockedAnchor = Master.Anchors[ActiveAnchor];
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Master.DockedAnchor != Master.Anchors[0])
            {
                ActiveAnchor--;
            }
            else if (Master.DockedAnchor == Master.Anchors[0])
            {
                ActiveAnchor = 2;
            }

            Master.DockedAnchor = Master.Anchors[ActiveAnchor];
        }

        if (Master.DockedAnchor == Master.Anchors[0])
        {
            Master.UIElement.position = Master.Anchors[0].transform.position;
            Master.UIElement.LookAt(GameObject.FindGameObjectsWithTag("MainCamera")[0].transform);
            Master.UIElement.eulerAngles = Master.UIElement.eulerAngles + Master.FloatingUIRotation;
            Master.UIElement.localScale = Master.FloatingUISize;
            Master.UIElement.parent = null;
        }
        else if (Master.DockedAnchor == Master.Anchors[1])
        {
            Master.UIElement.position = Master.Anchors[1].transform.position;
            Master.UIElement.localScale = Master.DeskUISize;
            Master.UIElement.eulerAngles = Master.DesktopUIRotation;
            Master.UIElement.parent = null;
        }
        else if (Master.DockedAnchor == Master.Anchors[2])
        {
            Master.UIElement.localScale = Master.HandUISize;
            Master.UIElement.parent = Master.DockedAnchor.transform.parent;
            Master.UIElement.localPosition = Master.HandUIPosition;
            Master.UIElement.localEulerAngles = Master.HandUIRotation;
        }

    }
}
