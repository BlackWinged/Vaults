using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateMachine : MonoBehaviour {
    public GameObject allPlayers;
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;
    public string currentPhase;
    public PlayerControl currentPlayer;


    private List<PlayerControl> players = new List<PlayerControl>();
    private IEnumerator playerEnum;
    // Use this for initialization
    void Start () {
        players.AddRange(allPlayers.GetComponentsInChildren<PlayerControl>());
        playerEnum = players.GetEnumerator();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initPlayerHands()
    {
        foreach (PlayerControl player in players)
        {
            player.initHand();
        }
    }

    public void initStateMachine()
    {
       
        playerEnum.Reset();
        playerEnum.MoveNext();
        currentPlayer = playerEnum.Current as PlayerControl;
        deactivatePlayers(currentPlayer);
        nextPhase();
    }

    public void deactivatePlayers( PlayerControl playerToActivate)
    {
        foreach (PlayerControl player in players)
        {
            player.playerDeactivate();
        }
        playerToActivate.playerActivate();
    }

    public void nextPhase()
    {
        if (PlayPhase.nextPhase().Equals(PlayPhase.NewTurn))
        {
            if (playerEnum.MoveNext())
            {
                currentPlayer = playerEnum.Current as PlayerControl;
                deactivatePlayers(currentPlayer);
                currentPhase = PlayPhase.getCurrentPhase();
            }
            else
            {
                playerEnum.Reset();
                playerEnum.MoveNext();
                currentPlayer = playerEnum.Current as PlayerControl;
                deactivatePlayers(currentPlayer);
                currentPhase = PlayPhase.getCurrentPhase();
            }
        }
        else
        {
            currentPhase = PlayPhase.getCurrentPhase();
        }
        
    }
    
}
