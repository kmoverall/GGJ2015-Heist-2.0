using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour, Stealable {
    private GameStateController gameState;
    public int despawnTime;
    public int value;

	// Use this for initialization
	void Start () {
        gameState = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameStateController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (gameState.CurrentClick >= despawnTime) {
            gameObject.renderer.enabled = false;
        } else {
            if (value != 0) {
                gameObject.renderer.enabled = true;
            }
        }
	}

    public int Steal(Player culprit) {
        if (gameObject.renderer.enabled && Vector3.Distance (this.gameObject.transform.position, culprit.gameObject.transform.position) < culprit.stealRange) {
            gameObject.renderer.enabled = false;
            return value;
        } else {
            return 0;
        }
    }
}
