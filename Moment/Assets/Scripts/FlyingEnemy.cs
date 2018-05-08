using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    Rigidbody2D myBody;
    public LayerMask enemyMask,playerMask;
    public float speed = 1f;
    Transform myTrans;
    float myWidth, myHeight;
    public GameObject playerCheck;
    public bool under;
    PlayerUnderCheck underCheck;
    public bool wasGrounded;
    public float riseTimer = 2.0f;
    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
        under = false;
        wasGrounded = false;
       
    }

    void FixedUpdate()
    {
        PlayerUnderCheck underCheck = this.transform.GetChild(0).GetComponent<PlayerUnderCheck>();
        //Check to see if there's in front of us before moving forward 
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        Vector2 lineCastPos1 = myTrans.position.toVector2() - myTrans.up.toVector2();



        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        Debug.DrawLine(lineCastPos1, lineCastPos1 + Vector2.down);

        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f, enemyMask);
        bool isGrounded = Physics2D.Linecast(lineCastPos1, lineCastPos1 - myTrans.up.toVector2() * .05f, enemyMask);


        //if there's no ground or you are blocked, turn around
        if (isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;

        }
        under = underCheck.under;
        Vector2 myVel = myBody.velocity;
        //Always move forward 
        if (!under)
        {
            
            myVel.x = -myTrans.right.x * speed;
            myVel.y = 0;
            myBody.velocity = myVel;
        }

        else if (under && !isGrounded && !wasGrounded)
        {
            myVel.y = -myTrans.up.y * speed * 2;
            myVel.x = 0;
            myBody.velocity = myVel;
            
        }
        else if (under && isGrounded)
        {
            wasGrounded = true;
        }
        else if (wasGrounded)
        {
            myVel.y = myTrans.up.y * speed;
            myVel.x = 0;
            myBody.velocity = myVel;
        }

        if(under && wasGrounded)
        {
            StartCoroutine(HorizontalTimer());
        }

        

    }

    public IEnumerator HorizontalTimer()
    {
        yield return new WaitForSeconds(riseTimer);
        under = false;
        wasGrounded = false;
    }
}
