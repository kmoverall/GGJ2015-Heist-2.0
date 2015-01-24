using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour, Stealable {
    private GameStateController gameState;
    public int despawnTime;
    public int value;
    private SpriteRenderer render;

	// Use this for initialization
	void Start () {
        gameState = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameStateController>();
        render = this.gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	    if (gameState.CurrentClick >= despawnTime) {
            render.enabled = false;
        } else {
            render.enabled = true;
        }
	}

    public int Steal(Player culprit) {
        if (render.enabled && Vector3.Distance (this.gameObject.transform.position, culprit.gameObject.transform.position) < culprit.stealRange) {
            render.enabled = false;
            return value;
        } else {
            return 0;
        }
    }
}
