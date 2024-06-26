using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class CurrentPanelControl_S1 : MonoBehaviour
{
    [Header("Target Strip Parameters")]
    [Tooltip("Specifies the thickness of the bars in the current pattern")] public int Thickness;
    [Tooltip("Specifies the distance of the center of the bar from the center of the panel")] public float DistanceFromOrigin;
    [Space(15)]

    public Button Bar1;
    public Button Bar2;

    private void OnEnable()
    {
        Bar1 = gameObject.transform.GetChild(0).GetComponent<Button>();
        Bar2 = gameObject.transform.GetChild(1).GetComponent<Button>();
    }

    private void Update()
    {
        Bar1.GetComponent<RectTransform>().localPosition = new Vector2(-DistanceFromOrigin, 0);
        Bar2.GetComponent<RectTransform>().localPosition = new Vector2(DistanceFromOrigin, 0);

        Bar1.GetComponent<RectTransform>().sizeDelta = new Vector2(Thickness, gameObject.GetComponentInParent<RectTransform>().rect.height);
        Bar2.GetComponent<RectTransform>().sizeDelta = new Vector2(Thickness, gameObject.GetComponentInParent<RectTransform>().rect.height);
    }
}
