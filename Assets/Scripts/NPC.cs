using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct PathNode {
	public int click;
	public Transform position;
}

public class NPC : MonoBehaviour {
    private const float footstepFreq = 0.2f; //How often are footstep sprites put in the npc's path. 0 < footstepFreq < 1. Pref. divides 1 evenly
	public List<PathNode> path; //Done to make pathDict editable in the inspector. Converted to a dictionary at runtime
	private Dictionary<int, Transform> pathDict = new Dictionary<int, Transform>();
	private GameStateController gameState;
	public int moneyHeld;
	public bool combinationHeld;
    public Transform footprints;
	
	// Use this for initialization
	void Start () {
		//Retrieve game state
		gameState = GameObject.FindGameObjectWithTag ("GameState").GetComponent<GameStateController>();

		//Generate path dictionary
		foreach (PathNode p in path) {
			pathDict.Add (p.click, p.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState.CurrentGameState == GameStateController.GameState.Setup) {
            //Move NPC to appropriate location for currentClick
			if (this.gameObject.transform.position != pathDict [gameState.CurrentClick].position) {
				this.gameObject.transform.position = pathDict [gameState.CurrentClick].position;
			}

            //Linearly interpolate between current position and next/prev position to place footprints
            if (gameState.CurrentClick < 0) {
                for (float j = footstepFreq; j < 1; j += footstepFreq) {
                    Vector3 lerp = pathDict [gameState.CurrentClick].position * (1-j) + pathDict [gameState.CurrentClick + 1].position * j;
                    Instantiate(footprints, lerp, Quaternion.identity);
                }
            } else if (gameState.CurrentClick > 0) {
                for (float j = footstepFreq; j < 1; j += footstepFreq) {
                    Vector3 lerp = pathDict [gameState.CurrentClick].position * (1-j) + pathDict [gameState.CurrentClick - 1].position * j;
                    Instantiate(footprints, lerp, Quaternion.identity);
                }
           }
		}

		
        if (gameState.CurrentGameState == GameStateController.GameState.Execution) {

        }
	}
}