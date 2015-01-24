using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	NavMeshAgent agent;
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
				agent.SetDestination(hit.point);
			
		}
	}
}