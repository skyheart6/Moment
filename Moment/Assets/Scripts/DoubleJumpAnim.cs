using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpAnim : MonoBehaviour {
	public GameObject anim;
	float animTimer;
	bool timerTrigger;
	bool timeForm; 
	bool shifted = false;
	public GameObject player;
	bool doubleJumped = false;
	bool grounded;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		PlayerController playerController = player.GetComponent<PlayerController> ();
		timeForm = playerController.timeForm; 
		grounded = playerController.grounded;
		if (!doubleJumped && !grounded) {
			if (Input.GetKey (KeyCode.Z) && !timeForm) {
				anim.SetActive (true);
				StartCoroutine (AnimTimer ());
				doubleJumped = true;

			}
		}
		if (grounded) {
			doubleJumped = false;
		}

		

	}



	public IEnumerator AnimTimer(){
		timerTrigger = false;
		animTimer = 0.3f;
		yield return new WaitForSeconds(animTimer);
		timerTrigger = true;
		anim.SetActive (false);
	}
}
