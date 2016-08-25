using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardControl : MonoBehaviour {
    public Sprite faceUp;
    public Sprite faceDown;
	public float movementSmoothing = 5;
    public GameObject Owner;
    public List<SpriteRenderer> currentStack;
    public int Force = 1;
    public int Mechanics = 1;
    public int Explosives = 1;

    private GameController gameController;
	private Vector3 target;
    private Vector3 zoomTarget;
    private Vector3 scale;

    private Vector3 oldPosition;
    private bool wasFlipped;

    private bool moveFrontFlag = false;
    private bool moveBackFlag = false;
    // Use this for initialization
    void Start () {
		target = transform.position;
        zoomTarget = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        scale = transform.localScale;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		moveCard (target);
	}

	public void moveCard (Vector3 target){
		this.target = target;
		if (transform.position!=target) {
            target.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * movementSmoothing);
            if (moveFrontFlag){
                moveCardToFront();
            } else if (moveBackFlag)
            {
                moveCardToBack();
            }
            //transform.Translate(Vector3.Normalize(target - transform.position) * 0.3f * movementSmoothing);
            //float a = (target - transform.position).magnitude;
            //if (a < 0.75f)
            //{
            //   transform.position = target;
            //}
		} 
	}

    public void moveCard(Vector3 target, bool moveToFront)
    {
        this.target = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
        this.target.z -= 8; 
        this.moveFrontFlag = moveToFront;
        this.oldPosition = target;
        wasFlipped = isCovered();
        gameController.setToFront(GetComponent<SpriteRenderer>(), null);
        
    }

    public void moveBack()
    {
        moveFrontFlag = false;
        moveBackFlag = true;
        this.target = oldPosition;
    }

    public void moveCardToBack()
    {
            transform.localScale = Vector3.Lerp(transform.localScale, scale, Time.deltaTime * movementSmoothing);
            if (wasFlipped)
            {
                if (!isCovered())
                {
                   transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,170,0), Time.deltaTime * movementSmoothing);
                    if (transform.rotation.eulerAngles.y>= 90)
                    {
                        coverCard();
                    }
                }
                else
                {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * movementSmoothing);
                }
            }

        }

    public void moveCardToFront()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, scale * 4, Time.deltaTime * movementSmoothing);
        if (wasFlipped)
        {
            if (isCovered())
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 170, 0), Time.deltaTime * movementSmoothing);
                if (transform.rotation.eulerAngles.y >= 90)
                {
                    showCard();
                }
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * movementSmoothing);
            }
        }


    }

    public void coverCard()
    {
        GetComponent<SpriteRenderer>().sprite = faceDown;
    }
    public void showCard()
    {
        GetComponent<SpriteRenderer>().sprite = faceUp;
    }

    private bool isCovered()
    {
        if (GetComponent<SpriteRenderer>().sprite.name.Equals(this.faceDown.name))
        {
            return true;
        }
        else { 
        return false;
        }
    }
}
