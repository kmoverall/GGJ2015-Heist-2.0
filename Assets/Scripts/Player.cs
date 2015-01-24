using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    private GameStateController gameState;
    private NavMeshAgent agent;

    private List<Order> orderQueue;

    private int moneyHeld;
    private bool isCaught;

    public Transform target;
    private List<Transform> targetQueue;

    public bool isSelected;
    public bool IsSelected { get { return isSelected; } set{ isSelected = value; } }
    public KeyCode selectNum;

    public float stealRange;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();

        targetQueue = new List<Transform> ();
        orderQueue = new List<Order> ();

        //Retrieve game state
        gameState = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameStateController>();
	}

	// Update is called once per frame
	void Update () {
        if (gameState.CurrentGameState == GameStateController.GameState.Setup && gameState.CurrentClick == 0) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            //Draw target if in range of movement and on floor plane
            if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonUp (1)) {
                if (Vector3.Distance (this.gameObject.transform.position, hit.point) < agent.speed * GameStateController.secondsPerClick) {
                    Vector3 targetPos = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
                    Transform tmpObj = (Transform)Instantiate(target, targetPos, Quaternion.Euler (-90,0,0));
                    targetQueue.Add (tmpObj);
                    orderQueue.Add (new Order(Order.Commands.MOVE, hit.point));
                }
            }

            if (Input.GetKeyUp (KeyCode.Backspace) && targetQueue.Count > 0 && orderQueue.Count > 0) {
                Destroy (targetQueue[targetQueue.Count - 1].gameObject);
                targetQueue.RemoveAt (targetQueue.Count - 1);
                orderQueue.RemoveAt (orderQueue.Count -1);
            }
        }

        if (gameState.CurrentGameState == GameStateController.GameState.Execution) {
            if(orderQueue[gameState.CurrentClick].command != Order.Commands.WAIT) {
                agent.SetDestination(orderQueue[gameState.CurrentClick].location);
                if (orderQueue[gameState.CurrentClick].command == Order.Commands.STEAL && agent.remainingDistance < stealRange) {
                    Steal ();
                }
            }
        }
	}

    void Steal () {
        Stealable[] targets = FindObjectsOfType (typeof(Stealable)) as Stealable[];
        foreach (Stealable s in targets) {
            moneyHeld += s.Steal (this);
        }
    }
}