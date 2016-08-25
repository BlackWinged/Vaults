using UnityEngine;
using System.Collections;

public class DrawVaultControl : MonoBehaviour {

    private GameController gameController;
    private int force, mechanics, demolitions;
    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        force = GetComponent<CardControl>().Force;
        mechanics = GetComponent<CardControl>().Mechanics;
        demolitions = GetComponent<CardControl>().Explosives;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseUp()
    {
        if (this.enabled)
        {
            PlayerControl clickingPlayer = gameController.playerStateMachine.currentPlayer.GetComponent<PlayerControl>();
            if (clickingPlayer.force >= force && clickingPlayer.mechanics>= mechanics && clickingPlayer.demolition >= demolitions)
            {
                //GetComponent<CardControl>().moveCard(clickingPlayer.vaultPosition.position);
                GetComponent<CardControl>().moveCard(clickingPlayer.vaultPosition.position,true);
                clickingPlayer.addToStack(GetComponent<SpriteRenderer>(), clickingPlayer.vault);
            }
        }
    }
}
