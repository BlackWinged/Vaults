using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
	public Transform handPosition;
    public Transform vaultPosition;
	public List<SpriteRenderer> hand;
    public List<SpriteRenderer> vault;
    public List<List<SpriteRenderer>> crewField = new List<List<SpriteRenderer>>();
    public float cardOverlapCoef = 0.33f;
    public SpriteRenderer handPlaceholder;
    public List<SpriteRenderer> selectedCrew = new List<SpriteRenderer>();
    public int force = 0, mechanics = 0, demolition = 0;

    private Dictionary<List<SpriteRenderer>, SpriteRenderer> crewAvailability = new Dictionary<List<SpriteRenderer>, SpriteRenderer>();
    private List<Vector3> handPositions = new List<Vector3>();
    private List<int> crewStatus = new List<int>();
    private int numberOfCrews = 0;
    private int numberInHand = 0;
	// Use this for initialization
	void Start () {

    }

	// Update is called once per frame
	void Update () {
        stackIntegrityManagement();
	}

    public void returnToHand( List<SpriteRenderer> stack)
    {
        foreach (SpriteRenderer card in stack)
        {
            addToStack(stack, hand);
        }
    }

    void stackIntegrityManagement()
    {
        int statusIndex = 0;
        foreach (List<SpriteRenderer> crew in crewField)
        {
            if (crewStatus.Count != crewField.Count)
            {
                crewStatus.Add(0);
            }
            if (crew.Count != crewStatus[statusIndex])
            {
                crewStatus[statusIndex] = crew.Count;
            List<SpriteRenderer> boss;
            List<SpriteRenderer> member;
            List<SpriteRenderer> tool;
            sortCrewCards(crew, out boss, out member, out tool);
            if (boss.Count == 0 && member.Count == 0)
            {
                returnToHand(tool);
            }
                refreshStack(crew);
                initHand();
            }
            statusIndex++;
        }
        statusIndex = 0;

        //foreach (int index in crewStatus)
        //{
        //    if (index == 0)
        //    {
        //        if (crewField[statusIndex] != null)
        //        {
        //            crewField.Remove(crewField[statusIndex]);
        //        }
        //    }
        //        statusIndex++;
        //}

    }


    public void initHand()
    {
        int offsetCount = hand.Count-1;
        foreach (SpriteRenderer card in hand)
        {
            ((CardControl)card.GetComponent<CardControl>()).Owner = gameObject;
            ((CardControl)card.GetComponent<CardControl>()).currentStack = hand;
            Vector3 target = handPosition.position;
            target.x -= card.bounds.extents.x * offsetCount;
            offsetCount -= 2;
            ((CardControl)card.GetComponent<CardControl>()).moveCard(target);

        }
    }

    public void refreshHandPosition()
    {

    }

    public List<SpriteRenderer> getAllOwnedCards()
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        result.AddRange(hand);
        foreach (List<SpriteRenderer> crew in crewField)
        {
            result.AddRange(crew);
        }
        return result;
    }

    public List<SpriteRenderer> newStack(SpriteRenderer card)
    {
        if (card.tag.Equals("Boss") || card.tag.Equals("Member")) 
        { 
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        card.GetComponent<CardControl>().currentStack.Remove(card);
        card.GetComponent<CardControl>().currentStack = result;
        result.Add(card);
        crewField.Add(result);
        initHand();
        return result;
    }
        initHand();
    return null;
    }

		public List<SpriteRenderer> addToStack(SpriteRenderer heldCard, SpriteRenderer targetCard)
		{
			CardControl heldCardControl = heldCard.GetComponent<CardControl>();
			CardControl targetCardControl = targetCard.GetComponent<CardControl>();
        if (heldCardControl.currentStack == targetCardControl.currentStack)
        {
            refreshStack(heldCardControl.currentStack);
            initHand();
            return heldCardControl.currentStack;
        }
            if (targetCardControl.currentStack == hand)
            {
                heldCardControl.currentStack.Remove(heldCard);
                heldCardControl.currentStack = targetCardControl.currentStack;
                heldCardControl.currentStack.Add(heldCard);
                initHand();
            }
            else
            {
            List<SpriteRenderer> boss;
            List<SpriteRenderer> member;
            List<SpriteRenderer> tool;
            sortCrewCards(targetCardControl.currentStack, out boss, out member, out tool);
        if (heldCard.tag.Equals("Boss"))
        {
            if (boss.Count < 1)
            {
                heldCardControl.currentStack.Remove(heldCard);
                heldCardControl.currentStack = targetCardControl.currentStack;
                heldCardControl.currentStack.Add(heldCard);
            }
        }
        if (heldCard.tag.Equals("Member"))
        {
            if (member.Count < 4)
            {
                heldCardControl.currentStack.Remove(heldCard);
                heldCardControl.currentStack = targetCardControl.currentStack;
                heldCardControl.currentStack.Add(heldCard);
            }
        }
        if (heldCard.tag.Equals("Tool"))
        {
            if (tool.Count < 3)
            {
                heldCardControl.currentStack.Remove(heldCard);
                heldCardControl.currentStack = targetCardControl.currentStack;
                heldCardControl.currentStack.Add(heldCard);
            }
        }
            }
            initHand();
            refreshStack(heldCardControl.currentStack);
        return heldCardControl.currentStack;
		}

        public void addToStack(List<SpriteRenderer> sourceStack, List<SpriteRenderer> targetStack)
        {
            foreach (SpriteRenderer heldCard in sourceStack)
            {
                CardControl heldCardControl = heldCard.GetComponent<CardControl>();
                    heldCardControl.currentStack.Remove(heldCard);
                    heldCardControl.currentStack = targetStack;
                    heldCardControl.currentStack.Add(heldCard);
                    initHand();
            }
        }

        public void addToStack(SpriteRenderer heldCard, List<SpriteRenderer> targetStack)
        {
                CardControl heldCardControl = heldCard.GetComponent<CardControl>();
                heldCardControl.currentStack.Remove(heldCard);
                heldCardControl.currentStack = targetStack;
                heldCardControl.currentStack.Add(heldCard);
                initHand();
        }
        

        void sortCrewCards(List<SpriteRenderer> crew, out List<SpriteRenderer> boss, out List<SpriteRenderer> member, out List<SpriteRenderer> tool)
        {
            boss = new List<SpriteRenderer>();
            member = new List<SpriteRenderer>();
            tool = new List<SpriteRenderer>();

            foreach (SpriteRenderer card in crew)
            {
                if (card.tag.Equals("Boss")){
                    boss.Add(card);
                }
                if (card.tag.Equals("Member"))
                {
                    member.Add(card);
                }
                if (card.tag.Equals("Tool"))
                {
                    tool.Add(card);
                }
            }
        }

        public void refreshStack(List<SpriteRenderer> crew)
        {
            if (crew == hand || crew.Count==0)
            {
                return;
            }
            List<SpriteRenderer> boss;
            List<SpriteRenderer> member;
            List<SpriteRenderer> tool;
            SpriteRenderer firstCard = crew[0];
            if (firstCard.tag.Equals("Tool"))
            {
                foreach (SpriteRenderer card in crew)
                {
                    if (card.tag.Equals("Boss") || card.tag.Equals("Member"))
                    {
                        firstCard = card;
                        break;
                    }
                }
            }
            SpriteRenderer lastCardProcessed = new SpriteRenderer(); ;
            sortCrewCards(crew, out boss, out member, out tool);

            if (firstCard.tag.Equals("Boss"))
            {
                if (member.Count > 0)
                    lastCardProcessed = null;
                foreach (SpriteRenderer card in member)
                {
                    if (lastCardProcessed == null)
                    {
                        Vector3 target = firstCard.transform.position;
                        target.x += card.bounds.size.x;
                        card.GetComponent<CardControl>().moveCard(target);
                        lastCardProcessed = card;
                    }
                    else
                    {
                        Vector3 target = lastCardProcessed.transform.position;
                        target.x += (card.bounds.size.x * cardOverlapCoef);
                        card.GetComponent<CardControl>().moveCard(target);
                        lastCardProcessed = card;
                    }
                }
                if (tool.Count > 0)
                    lastCardProcessed = null;
                foreach (SpriteRenderer card in tool)
                {
                    if (lastCardProcessed == null)
                    {
                        Vector3 target = firstCard.transform.position;
                        target.x -= card.bounds.size.x;
                        card.GetComponent<CardControl>().moveCard(target);
                        lastCardProcessed = card;
                    }
                    else
                    {
                        Vector3 target = lastCardProcessed.transform.position;
                        target.x -= (card.bounds.size.x * cardOverlapCoef);
                        card.GetComponent<CardControl>().moveCard(target);
                        lastCardProcessed = card;
                    }
                }
            }
            if (firstCard.tag.Equals("Member"))
            {
                lastCardProcessed = member[0];
                foreach (SpriteRenderer card in member)
                {
                    if (card != firstCard)
                    {
                        Vector3 target = lastCardProcessed.transform.position;
                        target.x += (card.bounds.size.x * cardOverlapCoef);
                        card.GetComponent<CardControl>().moveCard(target);
                        lastCardProcessed = card;
                    }
                }
                if (boss.Count > 0)
                {
                    lastCardProcessed = null;
                    foreach (SpriteRenderer card in boss)
                    {
                        if (lastCardProcessed == null)
                        {
                            Vector3 target = firstCard.transform.position;
                            target.x -= card.bounds.size.x;
                            card.GetComponent<CardControl>().moveCard(target);
                            lastCardProcessed = card;
                        }
                        else
                        {
                            Vector3 target = lastCardProcessed.transform.position;
                            target.x -= (card.bounds.size.x * cardOverlapCoef);
                            card.GetComponent<CardControl>().moveCard(target);
                            lastCardProcessed = card;
                        }
                    }
                }
                if (tool.Count > 0)
                {
                    lastCardProcessed = null;
                    foreach (SpriteRenderer card in tool)
                    {
                        if (lastCardProcessed == null)
                        {
                            Vector3 target = firstCard.transform.position;
                            if (boss.Count > 0)
                            {
                                target.x -= boss[0].bounds.size.x * 2;
                            }
                            else
                            {
                                target.x -= card.bounds.size.x;
                            }
                            card.GetComponent<CardControl>().moveCard(target);
                            lastCardProcessed = card;
                        }
                        else
                        {
                            Vector3 target = lastCardProcessed.transform.position;
                            target.x -= (card.bounds.size.x * cardOverlapCoef);
                            card.GetComponent<CardControl>().moveCard(target);
                            lastCardProcessed = card;
                        }
                    }
                }
            }
        }
        public void playerDeactivate()
        {
            foreach (SpriteRenderer card in getAllOwnedCards())
            {
                card.GetComponent<CardInputControl>().enabled = false;
            }
        }
        public void playerActivate()
        {
            foreach (SpriteRenderer card in getAllOwnedCards())
            {
                card.GetComponent<CardInputControl>().enabled = true;
            }
        }

    public SpriteRenderer checkCrewAvailability(List<SpriteRenderer> crew)
    {
        SpriteRenderer result;
        if (crewAvailability.TryGetValue(crew, out result))
        {
            return result;
        } else
        {
            return null;
        }
    }

    public void addCrewCrackStatus(List<SpriteRenderer> crew, SpriteRenderer vault)
    {
        SpriteRenderer result;
        if (!crewAvailability.TryGetValue(crew, out result))
        {
            crewAvailability.Add(crew, vault);
        }
    }

    public void setSelectedCrew(List<SpriteRenderer> crew) {
        selectedCrew = crew;
        calculateCrewStats(crew);
    }

    public void calculateCrewStats(List<SpriteRenderer> crew)
    {
        int force = 0, mechanics = 0, demolition = 0;
        if (this.enabled)
        {
            foreach (SpriteRenderer card in crew)
            {
                force += card.GetComponent<CardControl>().Force;
                mechanics += card.GetComponent<CardControl>().Mechanics;
                demolition += card.GetComponent<CardControl>().Explosives;
            }
        }
        this.force = force;
        this.mechanics = mechanics;
        this.demolition = demolition;
    }
}
