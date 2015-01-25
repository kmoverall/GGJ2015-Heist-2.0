using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    private GameStateController gameState;
    private NavMeshAgent agent;

    private List<Order> orderQueue;

    public int moneyHeld;
    private bool isCaught;

    public Transform target;
    private List<Transform> targetQueue;

    public bool isSelected;
    public bool IsSelected { get { return isSelected; } set{ isSelected = value; } }

    public float stealRange;

    private Vector3 targetLocation;

    public bool escaped;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();

        targetQueue = new List<Transform> ();
        orderQueue = new List<Order> ();

        //Retrieve game statef
        gameState = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameStateController>();
	}

	// Update is called once per frame
	void Update () {
        if (!escaped) {
            if (gameState.CurrentGameState == GameStateController.GameState.Setup && gameState.CurrentClick == 0 && isSelected) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                //Draw target if in range of movement and on floor plane
                if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonUp (1)) {
                    Vector3 lastpoint = targetQueue.Count > 0 ? targetQueue[targetQueue.Count-1].position : this.gameObject.transform.position; 
                    if (Vector3.Distance (lastpoint, hit.point) < agent.speed * GameStateController.secondsPerClick
                            && targetQueue.Count < gameState.maxTimeDisplacement) {
                        targetLocation = new Vector3 (hit.point.x, hit.point.y + 0.5f, hit.point.z);
                        Transform tmpObj = (Transform)Instantiate (target, targetLocation, Quaternion.Euler (-90, 0, 0));
                        targetQueue.Add (tmpObj);
                        orderQueue.Add (new Order (Order.Commands.MOVE, targetLocation));
                    }
                }

                if (Input.GetKeyUp (KeyCode.Backspace) && targetQueue.Count > 0 && orderQueue.Count > 0) {
                    Destroy (targetQueue [targetQueue.Count - 1].gameObject);
                    targetQueue.RemoveAt (targetQueue.Count - 1);
                    orderQueue.RemoveAt (orderQueue.Count - 1);
                }
            }

            //Turn off target point rendering if robber is not selected
            foreach (Transform t in targetQueue) {
                t.gameObject.renderer.enabled = isSelected;
            }

            if (gameState.CurrentGameState == GameStateController.GameState.Execution) {
                if (orderQueue.Count > gameState.CurrentClick && orderQueue [gameState.CurrentClick].command != Order.Commands.WAIT) {
                    agent.SetDestination (orderQueue [gameState.CurrentClick].location);
                    if (agent.remainingDistance < stealRange) {
                        Steal ();
                    }
                } else {
                    agent.SetDestination (this.transform.position);
                }
            }
        }

	}

    public void Steal () {
        NPC[] targets = FindObjectsOfType (typeof(NPC)) as NPC[];
        foreach (Stealable s in targets) {
            moneyHeld += s.Steal (this);
        }
        Treasure[] gold = FindObjectsOfType (typeof(Treasure)) as Treasure[];
        foreach (Stealable s in gold) {
            moneyHeld += s.Steal (this);
        }
    }
}