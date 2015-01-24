using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct PathNode {
	public int click;
	public Transform position;
}

public class NPC : MonoBehaviour, Stealable {
    private const float footstepFreq = 0.2f; //How often are footstep sprites put in the npc's path. 0 < footstepFreq < 1. Pref. divides 1 evenly
	public List<PathNode> path; //Done to make pathDict editable in the inspector. Converted to a dictionary at runtime
	private Dictionary<int, Transform> pathDict = new Dictionary<int, Transform>();
	private GameStateController gameState;
	public int moneyHeld;
	public bool combinationHeld;
    public Transform footprints;
    private List<GameObject> footprintList;
    private NavMeshAgent agent;

    private bool footprintsSpawned = false;
	
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();

		//Retrieve game state
		gameState = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameStateController>();

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

            if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.Space)) {
                footprintsSpawned = false;
            }

            if (!footprintsSpawned) {
                if (gameState.CurrentClick < 0) {
                    for (float j = footstepFreq; j < 1; j += footstepFreq) {
                        Vector3 lerp = pathDict [gameState.CurrentClick].position * (1-j) + pathDict [gameState.CurrentClick + 1].position * j;
                        lerp.y += 0.1f;
                        Instantiate(footprints, lerp, Quaternion.Euler (-90f,0f,0f));
                    }
                    footprintsSpawned = true;
                } else if (gameState.CurrentClick > 0) {
                    for (float j = footstepFreq; j < 1; j += footstepFreq) {
                        Vector3 lerp = pathDict [gameState.CurrentClick].position * (1-j) + pathDict [gameState.CurrentClick - 1].position * j;
                        lerp.y += 0.1f;
                        Instantiate(footprints, lerp, Quaternion.Euler (-90f,0f,0f));
                    }
                    footprintsSpawned = true;
               }
           }

		}

        if (gameState.CurrentGameState == GameStateController.GameState.Execution) {
            agent.SetDestination(pathDict[gameState.CurrentClick+1].position);
        }
	}

    public int Steal(Player culprit) {
        if (Vector3.Distance (this.gameObject.transform.position, culprit.gameObject.transform.position) < culprit.stealRange) {
            int tmpmon = moneyHeld;
            moneyHeld = 0;
            return tmpmon;
        } else
            return 0;
    }
}