using UnityEngine;
using System.Collections;

public class Guard : NPC {

    public float fov = 90.0f;
    private bool chasing;
    private Player chaseTarget;
    private SphereCollider col;

	// Use this for initialization
	new void Start () {
        base.Start ();
        col = GetComponent<SphereCollider> ();
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update ();

        if (chasing) {
            Debug.Log ("CHASE");
            agent.destination = chaseTarget.transform.position;
        }
	}

    void OnTriggerStay(Collider other)
    {
        Debug.Log ("TRIGGERED");
        if (other.tag == "Robber") {
            chasing = false;

            Vector3 dir = other.transform.position - transform.position;
            float angle = Vector3.Angle (dir, transform.forward);

            if (angle < fov * 0.5f) {
                RaycastHit hit;

                if(Physics.Raycast (transform.position + transform.up, dir.normalized, out hit, col.radius))
                {
                    if(hit.collider.gameObject.tag == "Robber") {
                        chasing = true;
                        chaseTarget = hit.collider.gameObject.GetComponent<Player>();
                    }
                }
            }
        }
    }
}
