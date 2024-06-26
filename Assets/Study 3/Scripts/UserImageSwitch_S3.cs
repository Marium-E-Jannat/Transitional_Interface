using UnityEngine;

public class UserImageSwitch_S3 : MonoBehaviour
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

        for (int i = 0; i < UICount; i++)
        {
            ImageHolder.GetChild(i).gameObject.SetActive(false);
        }
        Master.DockedAnchor = Master.Anchors[ActiveAnchor];
        ImageHolder.GetChild(0).gameObject.SetActive(true);
    }

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
    }
}
