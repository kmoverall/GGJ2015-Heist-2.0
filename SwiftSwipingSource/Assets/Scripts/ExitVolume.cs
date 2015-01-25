using UnityEngine;
using System.Collections;

public class ExitVolume : MonoBehaviour {
    private GameStateController gameState;

	// Use this for initialization
	void Start () {
        //Retrieve game state
        gameState = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameStateController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        Debug.Log ("Trigger!");
        if (other.gameObject.tag == "Player") {
            Debug.Log ("Escaped!");
            Player playerScript = other.gameObject.GetComponent<Player>();
            if(playerScript != null) {
                playerScript.escaped = true;
                gameState.TotalMoney = gameState.TotalMoney + playerScript.moneyHeld;
            }
        }
    }
}
