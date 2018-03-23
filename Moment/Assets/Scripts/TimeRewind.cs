using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour {
    public GameObject player;
    public ArrayList playerPositions;
    bool isRewinding = false;


    void Start () {
        playerPositions = new ArrayList();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            isRewinding = true;
        }
        else
        {
            isRewinding = false;
        }
    }

    void FixedUpdate()
    {
        if (!isRewinding)
        {
            playerPositions.Add(player.transform.position);
        }
        else
        {
            player.transform.position = (Vector3)playerPositions[playerPositions.Count - 1];
            playerPositions.RemoveAt(playerPositions.Count - 1);
        }

    }
}
