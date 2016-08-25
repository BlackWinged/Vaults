using UnityEngine;
using System.Collections;

public class CrewSelectControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        if (this.enabled)
        {
            GetComponent<CardControl>().Owner.GetComponent<PlayerControl>().setSelectedCrew(GetComponent<CardControl>().currentStack);
        }
    }
}
