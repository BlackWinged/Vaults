using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CardInputControl : MonoBehaviour {
    public float moveTreshold = 0.2f;

    private Vector3 positionBuff;
    // Use this for initialization
    void Start () {
        positionBuff = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (this.enabled)
        {
            positionBuff = transform.position;
        }
    }
    
    void OnMouseDrag()
    {
        if (this.enabled)
        {
            InputController.cardClicked = true;
            if (Input.touchCount <= 1)
            {
                GetComponent<CardControl>().moveCard(InputController.getMousePosition());
            }
        }
    }

    void OnMouseUp()
    {
        if (this.enabled)
        {
        if ((transform.position - positionBuff).magnitude < moveTreshold)
        {
            GetComponent<CardControl>().moveCard(positionBuff);
            return;
        }
            bool matchFound = false;
            PlayerControl ownerControl = ((PlayerControl)((CardControl)GetComponent<CardControl>()).Owner.GetComponent<PlayerControl>());
            List<SpriteRenderer> allOwnedCards = ownerControl.getAllOwnedCards();
            foreach (SpriteRenderer card in allOwnedCards)
            {
                if ((card.bounds.Intersects(GetComponent<SpriteRenderer>().bounds)) && (card != GetComponent<SpriteRenderer>()))
                {
                    ownerControl.addToStack(GetComponent<SpriteRenderer>(), card);
                    matchFound = true;
                }
            }
            if (!matchFound)
            {
                ownerControl.newStack(GetComponent<SpriteRenderer>());
            }
            InputController.cardClicked = false;
            if (GetComponent<CardControl>().currentStack != GetComponent<CardControl>().Owner.GetComponent<PlayerControl>().hand)
            {
                if (GetComponent<CardControl>().Owner.GetComponent<PlayerControl>().checkCrewAvailability(GetComponent<CardControl>().currentStack) == null)
                {
                    GetComponent<CardControl>().Owner.GetComponent<PlayerControl>().setSelectedCrew(GetComponent<CardControl>().currentStack);
                }
            }
        }
    }
}
