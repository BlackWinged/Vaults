using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
    public Vector3 touchPosition;
    public string gesture;
    public static bool cardClicked = false;
    // Use this for initialization

    private static InputController inputController;
    private bool clickDown = false;
    private bool scrollEnable = false;
    private Vector3 referenceWorldPoint = new Vector3();

    public static InputController instance
    {
        get
        {
            if (!inputController)
            {
                inputController = FindObjectOfType(typeof(InputController)) as InputController;
            }
            if (!inputController)
            {
                Debug.LogError("No active InputController managers loaded in scene");
            }
            return inputController;
        }
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.mousePresent)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0;
        } else if (Input.touchCount==1)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        if (Input.GetMouseButtonDown(0) || Input.touchCount == 1)
        {
            clickDown = true;
        }
        else if (Input.GetMouseButtonUp(0) && Input.touchCount == 0)
        {
            clickDown = false;
            scrollEnable = false;
        }
        motionTracking();
	}

    public static Vector3 getMousePosition()
    {
        return instance.touchPosition;
    }

    public void motionTracking()
    {
        if (!cardClicked && clickDown)
        {
            if (!scrollEnable)
            {
                referenceWorldPoint = touchPosition;
                scrollEnable = true;
            }
            else
            {
                Vector3 screenReference = referenceWorldPoint - touchPosition;
                Camera.main.transform.Translate(screenReference);
                Debug.Log("ScreenPoint: " + Input.mousePosition.ToString() + " TP: " + touchPosition.ToString());
            }
        }
    }
}
