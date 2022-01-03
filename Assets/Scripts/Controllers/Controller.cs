using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{
    private Vector3 clickDown = Vector3.zero;
    private Vector3 clickUp = Vector3.zero;
    private bool isClickDown = false;

    private Outline outline;

    [ColorUsage(true,true)]
    public Color hoverColor;

    [ColorUsage(true, true)]
    public Color clickColor;

    [Range(0.5f,1.5f)]
    public float maxTargetHelperScale;

    // private Vector3 initialDirection;

    public GameObject rocket;

    public float instantiateFactor = 0.55f;

    public bool canPlay = false;

    // Show Panel
    //private RectTransform aimTextObject;
    //private Image aimTextImage;
    //private TextMeshProUGUI aimText;
    //private RectTransform aimTextTransform;

    public Vector3 debugPos;
    private float framePower = 0f;
    private float speedFactor = 0.01f;

    public Camera cam;
    public LineRenderer lineRenderer;

    private const float ARROW_SCALE_1 = 0.33578f;
    private const float ARROW_SCALE_2 = 0.68057f;
    private const float ARROW_SCALE_3 = 1.02504f;
    private const float ARROW_SCALE_4 = 1.37033f;
    private const float ARROW_SCALE_5 = 1.71512f;


    // Start is called before the first frame update
    void Start()
    {
        DisableTargetHelper();
        outline = gameObject.GetComponent<Outline>();

        //aimTextObject = GameObject.FindGameObjectWithTag("AimTextObject").GetComponent<RectTransform>();
        //aimTextImage = GameObject.FindGameObjectWithTag("AimTextImage").GetComponent<Image>();
        //aimText = GameObject.FindGameObjectWithTag("AimText").GetComponent<TextMeshProUGUI>();
        //aimTextTransform = GameObject.FindGameObjectWithTag("AimText").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canPlay) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.name == "PS_Planet_Earth" && outline.isActiveAndEnabled == false)
        {
            outline.OutlineColor = hoverColor;
            outline.OutlineWidth = 2f;
            outline.enabled = true;
        }
        if(!(Physics.Raycast(ray, out hit) && hit.transform.name == "PS_Planet_Earth") && outline.isActiveAndEnabled == true)
        {
            outline.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit) && hit.transform.name == "PS_Planet_Earth")
        {
            isClickDown = true;
            clickDown = Input.mousePosition;
            //Debug.Log("Left click down on earth! " + "Mouse position: " + clickDown);
            outline.OutlineColor = clickColor;
            outline.OutlineWidth = 2f;
        }
        if (Input.GetMouseButtonUp(0) && isClickDown)
        {
            isClickDown = false;
            clickUp = Input.mousePosition;
            //Debug.Log("Left click up! " + "Mouse position: " + clickUp);
            DisableTargetHelper();
            outline.OutlineColor = hoverColor;
            outline.OutlineWidth = 2f;
            //aimTextImage.enabled = false;
            //aimText.enabled = false;

            LaunchRocket(clickDown, clickUp);
            canPlay = false;
        }
        if (isClickDown)
        {
            outline.enabled = true;
            DrawTargetHelper(clickDown);
            //ShowInfoPanel(clickDown);
        }
    }

    void DrawTargetHelper(Vector3 startPos)
    {
        Vector3 endPos = Input.mousePosition;
        Vector3 dir = (startPos - endPos).normalized;
        float sqrMgn = (startPos - endPos).sqrMagnitude;

        float scale = sqrMgn * 0.00001f;
        //Debug.Log(scale);

        if (scale > maxTargetHelperScale) scale = maxTargetHelperScale;

        framePower = (scale - 0.52f) * 263.157894737f;

        lineRenderer.enabled = true;

        float fakeScale;
        if (scale < ARROW_SCALE_1) fakeScale = ARROW_SCALE_1;
        else if (scale < ARROW_SCALE_2) fakeScale = ARROW_SCALE_2;
        else if (scale < ARROW_SCALE_3) fakeScale = ARROW_SCALE_3;
        else if (scale < ARROW_SCALE_4) fakeScale = ARROW_SCALE_4;
        else fakeScale = ARROW_SCALE_5;

        lineRenderer.SetPosition(0, dir);
        lineRenderer.SetPosition(1, dir + (dir * fakeScale));
    }

    void DisableTargetHelper()
    {
        //lineRenderer.SetPositions(new Vector3[] { new Vector3(0, 0, 0) });
        lineRenderer.enabled = false;
    }

    void LaunchRocket(Vector3 startPoint, Vector3 endPoint)
    {
        // In case the user just clicks on earth do nothing.
        if (startPoint == endPoint) return;
        // In case the user gave no power do nothing.
        if (Mathf.Floor(framePower) == 0f) return;
        Vector3 direction = (startPoint - endPoint).normalized;
        Rigidbody clone;
        Vector3 startingPosition = new Vector3(direction.x * instantiateFactor, direction.y * instantiateFactor, 0f);

        clone = Instantiate(rocket.GetComponent<Rigidbody>(), startingPosition, Quaternion.identity);
        // calc the angle
        float startingAngle = Mathf.Rad2Deg * Mathf.Atan2((startPoint - endPoint).y, (startPoint - endPoint).x);

        clone.transform.Rotate(-startingAngle, 90f, 0f, Space.Self);
        clone.AddForce(direction * framePower * speedFactor, ForceMode.Impulse);
    }

    //void ShowInfoPanel(Vector3 startPos)
    //{

    //    Vector3 endPos = Input.mousePosition;
    //    Vector3 dir = (startPos - endPos).normalized;
    //    float sqrMgn = (startPos - endPos).sqrMagnitude;
    //    float angle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
    //    if (angle < 0f) angle += 360f;

    //if (angle < 90f)
    //{
    //    aimTextObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    //    aimTextObject.GetChild(1).GetComponent<RectTransform>().transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
    //    aimTextObject.GetChild(1).GetComponent<RectTransform>().transform.localPosition = new Vector3(120f, -77f, 0f);
    //}
    //else if (angle < 180f)
    //{
    //    aimTextObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    //    aimTextObject.GetChild(1).GetComponent<RectTransform>().transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    //    aimTextObject.GetChild(1).GetComponent<RectTransform>().transform.localPosition = new Vector3(125f, -77f, 0f);
    //}
    //else if (angle < 270f)
    //{
    //    aimTextObject.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
    //    aimTextObject.GetChild(1).GetComponent<RectTransform>().transform.localRotation = Quaternion.Euler(0f, 180f, 180f);
    //    aimTextObject.GetChild(1).GetComponent<RectTransform>().transform.localPosition = new Vector3(125f, -72f, 0f);
    //}
    //else
    //{
    //    aimTextObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    //    aimTextObject.GetChild(1).GetComponent<RectTransform>().transform.localRotation = Quaternion.Euler(180f, 180f, 0f);
    //    aimTextObject.GetChild(1).GetComponent<RectTransform>().transform.localPosition = new Vector3(120f, -72f, 0f);
    //}
    //aimText.SetText(Mathf.Floor(angle).ToString() + " deg\n" + Mathf.Floor(framePower).ToString() + "%");
    //aimTextImage.enabled = true;
    //aimText.enabled = true;
    //}
}
