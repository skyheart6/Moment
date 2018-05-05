using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityParticles : MonoBehaviour {
    bool timeForm;
    public GameObject player;
    bool grounded;
    public GameObject fall;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        PlayerController playerController = player.GetComponent<PlayerController>();
        timeForm = playerController.timeForm;
        grounded = playerController.grounded;
        if (!grounded)
        {
            if (Input.GetKey(KeyCode.X) && !timeForm)
            {
                fall.SetActive(true);

            }
        }
        if (grounded)
        {
            fall.SetActive(false);
        }
    }
}
