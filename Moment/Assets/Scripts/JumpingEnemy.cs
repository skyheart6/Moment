using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    Rigidbody2D myBody;
    public LayerMask enemyMask;
    public float speed = 1f;
    Transform myTrans;
    float myWidth, myHeight;
    public float jumpForce;
    bool jump;
    float jumpTimer = .5f;

    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
        jump = false;

        
    }

    void FixedUpdate()
    {
        if (!jump)
        {
            StartCoroutine(JumpTimer());
        }
        //Check to see if there's in front of us before moving forward 
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f, enemyMask);
        //if there's no ground or you are blocked, turn around
    
        if (isGrounded && jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

        }
        if (isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;

        }

        //Always move forward 
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;

    }
    public IEnumerator JumpTimer()
    {
        jump = true;
        yield return new WaitForSeconds(jumpTimer);
        jump = false;
    }
}
