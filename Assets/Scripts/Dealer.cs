using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dealer : MonoBehaviour {
    public GameObject allCards;
    public List<SpriteRenderer> deck;
    public List<SpriteRenderer> deckAllCards;
    public List<SpriteRenderer> policeStation;
    public List<SpriteRenderer> evidenceLocker;
    public List<SpriteRenderer> disruptDeck;
    public List<List<SpriteRenderer>> vaults = new List<List<SpriteRenderer>>();
    public PlayerControl playerOne;
    public PlayerControl playerTwo;
    public GameObject allPlayers;

    private List<PlayerControl> players = new List<PlayerControl>();
    private bool dealtStartCards = false;
    // Use this for initialization
    void Start () {
        deck.AddRange(allCards.GetComponentsInChildren<SpriteRenderer>());
        deckAllCards.AddRange(allCards.GetComponentsInChildren<SpriteRenderer>());
        foreach (SpriteRenderer card in deck)
        {
            card.GetComponent<CardControl>().Owner = this.gameObject;
            card.GetComponent<CardControl>().currentStack = deck;
        }
        players.AddRange(allPlayers.GetComponentsInChildren<PlayerControl>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void dealStartCards()
    {
        List<SpriteRenderer> dealtCards = new List<SpriteRenderer>();
        float zComponent = 0;
        if (!dealtStartCards)
        {
        zComponent = deck[0].transform.position.z;
        PlayerControl nextPlayer;
        int vaultCount = 1;

        for (int i = 0; i < 3; i++)
        {
            vaults.Add(new List<SpriteRenderer>());
        }
            foreach (SpriteRenderer card in deck)
            {
                Vector3 sortPos = card.transform.position;
                zComponent += 0.001f;
                sortPos.z = zComponent;
                card.transform.position = sortPos;

                if (card.tag.Equals("Vault"))
                {
                    if (vaultCount == 1)
                    {
                        ((CardControl)card.GetComponent<CardControl>()).moveCard(Positions.getVaultOnePos());
                        addToStack(card, vaults[0]);
                    }
                    if (vaultCount == 2)
                    {
                        ((CardControl)card.GetComponent<CardControl>()).moveCard(Positions.getVaultTwoPos());
                        addToStack(card, vaults[1]);
                    }
                    if (vaultCount == 3)
                    {
                        ((CardControl)card.GetComponent<CardControl>()).moveCard(Positions.getVaultThreePos());
                        addToStack(card, vaults[2]);
                        vaultCount = 1;
                    }
                    else
                    {
                        vaultCount++;
                    }
                    dealtCards.Add(card);
                }
                nextPlayer = players[0];
                foreach (PlayerControl player in players)
                {
                    if (player.hand.Count < nextPlayer.hand.Count)
                    {
                        nextPlayer = player;
                    }
                }
                if (card.tag.Equals("Member") || card.tag.Equals("Boss") || card.tag.Equals("Tool"))
                {
                    if (nextPlayer.hand.Count < 5)
                    {
                        nextPlayer.hand.Add(card);
                        ((CardControl)card.GetComponent<CardControl>()).moveCard(nextPlayer.handPosition.position);
                        dealtCards.Add(card);
                    }

                }
                if (card.tag.Equals("Disrupt"))
                {
                    card.GetComponent<CardControl>().moveCard(Positions.getDisruptPos());
                    addToStack(card, disruptDeck, false);
                    dealtCards.Add(card);
                }
            }
        }

        deck.RemoveRange(0, dealtCards.Count);
        dealtStartCards = true;
    }

    public void addToStack(SpriteRenderer heldCard, List<SpriteRenderer> targetStack, bool remove = false)
    {
        if (targetStack == null)
        {
            targetStack = new List<SpriteRenderer>();
        }
        CardControl heldCardControl = heldCard.GetComponent<CardControl>();
        if (remove)
        {
        heldCardControl.currentStack.Remove(heldCard);
        }
        heldCardControl.currentStack = targetStack;
        heldCardControl.currentStack.Add(heldCard);
    }

    void sendCardToLocation()
    {

    }
}
