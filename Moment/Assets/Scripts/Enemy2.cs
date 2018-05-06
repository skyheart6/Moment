//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MovingObject {
//    private Animation animator;
//    private Transform transform;
//    private bool skipMove;

//	// Use this for initialization
//	protected override void Start () {
//        animator = GetComponent<Animator>();
//        target = GameObject.FindObjectWithTag("Player").transform;
//        base.Start();
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//	}

//    protected override void AttemptMove <T> (int xDir, int yDir)
//    {
//        if (skipMove)
//        {
//            skipMove = false;
//            return;
//        }

//        base.AttemptMove<T>(xDir, yDir);
//        skipMove = true;
//    }

//    public void MoveEnemy()
//    {
//        int xDir = 0;
//        int yDir = 0;

//        if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
//        {
//            yDir = target.position.y > transform.position.y ? 1 : -1;
//        }
//        else
//        {
//           xDir = target.position.x > trasnform.position.x  ? 1 : -1;
//        }

//        AttemptMove<Player>(xDir, yDir);

//    }

//    protected override void OnCantMove <T> (T component)
//    {
//        Player hitPlayer = component as Player;
//    }
//}
