using UnityEngine;
using System.Collections;

public class Footprints : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.Space)) {
            Destroy (this.gameObject);
        }
	}
}
