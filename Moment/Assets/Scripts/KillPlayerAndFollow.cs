using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerAndFollow : MonoBehaviour
{

    public LevelManager levelManager;


    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            levelManager.RespawnPlayer();
            levelManager.BossFollow();
        }
    }
}
