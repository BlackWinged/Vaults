using UnityEngine;
using System.Collections;

public class DrawDeckControl : MonoBehaviour {

    private GameController gameController;
	// Use this for initialization
	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        if (this.enabled)
        {
            PlayerStateMachine playerState = gameController.playerStateMachine;
            GetComponent<CardControl>().moveCard(playerState.currentPlayer.handPosition.position);
            playerState.currentPlayer.GetComponent<PlayerControl>().addToStack(GetComponent<SpriteRenderer>(), playerState.currentPlayer.GetComponent<PlayerControl>().hand);
        }
    }
}
