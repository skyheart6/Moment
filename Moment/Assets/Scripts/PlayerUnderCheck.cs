using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnderCheck : MonoBehaviour {
    public bool under = false;
    public float distance;
    public GameObject player;
    FlyingEnemy flyingEnemy;
    public float distanceLimit;
	// Use this for initialization
	void Start () {

        
    }

    // Update is called once per frame
    void FixedUpdate () {
        FlyingEnemy flyingEnemy = this.transform.parent.GetComponent<FlyingEnemy>();
        distance = Vector2.Distance(player.transform.position, this.transform.position);
        if (distance <= distanceLimit)
        {
            under = true;
        }
        else if (!flyingEnemy.under)
        {
            under = false;
        }

    }


}
