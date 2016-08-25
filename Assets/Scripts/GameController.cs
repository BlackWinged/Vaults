using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Dealer dealer;
    public PlayerStateMachine playerStateMachine;
    public GameObject clickCancel;
    float time = 2;
    private bool initialized = false;
    private Text phaseText;
    private Text cashText;
    private Text actionCountText;
    private SpriteRenderer foreGroundCard;
    // Use this for initialization
    void Start () {
        phaseText = GameObject.FindGameObjectWithTag("UIPhase").GetComponentInChildren<Text>();
       // cashText = GameObject.FindGameObjectWithTag("UICash").GetComponentInChildren<Text>();
       // actionCountText = GameObject.FindGameObjectWithTag("UIActionCount").GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!initialized)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                dealer.dealStartCards();
            }
            if (time <= -1)
            {
                playerStateMachine.initPlayerHands();
                playerStateMachine.initStateMachine();
                phaseControl();
                initialized = true;
            }
        }
        if (initialized)
        {
            phaseText.text = playerStateMachine.currentPlayer.gameObject.name + " - " + playerStateMachine.currentPhase;
        }
	}

    public void nextPhase()
    {
        playerStateMachine.nextPhase();
        phaseControl();
    }

    public void deactivateAllPhaseScripts()
    {
        foreach (SpriteRenderer card in dealer.deckAllCards)
        {
            if (card.GetComponent<CardInputControl>() != null)
            {
                card.GetComponent<CardInputControl>().enabled = false;
            }
            if (card.GetComponent<DrawDeckControl>() != null)
            {
                card.GetComponent<DrawDeckControl>().enabled = false;
            }
            if (card.GetComponent<DrawVaultControl>() != null)
            {
                card.GetComponent<DrawVaultControl>().enabled = false;
            }
        }
    }

    public void activateDrawable()
    {
        foreach (SpriteRenderer card in dealer.deck)
        {
            card.GetComponent<DrawDeckControl>().enabled = true;
        }
        foreach (SpriteRenderer card in dealer.policeStation)
        {
            card.GetComponent<DrawDeckControl>().enabled = true;
        }
        foreach (SpriteRenderer card in dealer.evidenceLocker)
        {
            card.GetComponent<DrawDeckControl>().enabled = true;
        }
        foreach (SpriteRenderer card in dealer.disruptDeck)
        {
            card.GetComponent<DrawDeckControl>().enabled = true;
        }
    }

    public void phaseControl()
    {
        string currentPhase = playerStateMachine.currentPhase;
        deactivateAllPhaseScripts();
        if (currentPhase.Equals(PlayPhase.RecruitmentPhase))
        {
            activateDrawable();
        }
        else if (currentPhase.Equals(PlayPhase.PlanningPhase))
        {
            playerStateMachine.currentPlayer.playerActivate();
        }
        else if (currentPhase.Equals(PlayPhase.JobsPhase))
        {
            foreach (SpriteRenderer card in dealer.deckAllCards)
            {
                if (card.tag.Equals("Vault"))
                {
                card.GetComponent<DrawVaultControl>().enabled = true;
                }
            }
        }
        else if (currentPhase.Equals(PlayPhase.HideoutPhase))
        {
            playerStateMachine.currentPlayer.playerActivate();
        }
    }

    public void setToFront(SpriteRenderer card, Canvas customGui)
    {
        clickCancel.SetActive(true);
        foreGroundCard = card;
    }

    public void returnBack()
    {
        if (foreGroundCard != null)
        {
            foreGroundCard.GetComponent<CardControl>().moveBack();
            clickCancel.SetActive(false);
        }
        foreGroundCard = null;
    }
}
