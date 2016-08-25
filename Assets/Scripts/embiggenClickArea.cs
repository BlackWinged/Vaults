using UnityEngine;
using System.Collections;

public class embiggenClickArea : MonoBehaviour {

    // Use this for initialization
    private GameController gameController;
	void Start () {
        GetComponent<BoxCollider2D>().size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)*2);
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        gameController.returnBack();
    }
}
