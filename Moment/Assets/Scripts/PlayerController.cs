using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	

	List<Vector2> playerPositions;
	public float maxSpeed = 10f;
	bool facingRight = true;
	Animator anim;
	public float jumpForce = 700f;
	public bool grounded = false;
	bool doubleJump = false;
	public bool stasis = false;
	bool timerTrigger = true;
	public bool isRewinding = false;
    bool shifted = false;
    bool rewound = false;
	Rigidbody2D rb;
    new SpriteRenderer renderer;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
    bool timeForm = true;
    public float stasisTimer;
   


	public KeyCode power1;
    public KeyCode power2;
    public KeyCode formShift;
    public KeyCode jump;

	// Use this for initialization	
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D> ();
        renderer = GetComponent<SpriteRenderer>();
		playerPositions = new List<Vector2> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
        colorChange();
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
        if (grounded)
            shifted = false;
        if (!grounded)
            transform.parent = null;
        anim.SetFloat ("Speed", GetComponent<Rigidbody2D> ().velocity.y);

		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs (move));
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		if (move > 0 && !facingRight)
			Flip();
		else if (move < 0 && facingRight)
			Flip();


        Power1();
        Power2();
    }

	void Update(){
		if (grounded && Input.GetKeyDown (jump)){
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));

		}



	}
    void Power1()
    {
        if (timeForm)
        {
            Stasis();
        }
        else {
            secondJump();
        }
            
    }

    void Power2()
    {
        if (timeForm)
        {
            if (Input.GetKeyDown(power2))
                isRewinding = true;
            if (Input.GetKeyUp(power2) || grounded)
            {
                isRewinding = false;
                
            }
            if (isRewinding && !grounded )
               
            {
                Rewind();
               
            }
            else {

                Record();
            }
        }
        else {
            Record();
            Fall();
        }
    }

    void secondJump()
    {
        if (!doubleJump && !grounded && Input.GetKey(power1))
        {
            doubleJump = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        if (grounded)
            doubleJump = false;
    }

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Stasis(){
		if (!grounded && Input.GetKey (power1) && !stasis) {
			gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			stasis = true;
			StartCoroutine(StasisTimer ());
		}

		if (stasis && timerTrigger) {
			
			gameObject.transform.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		}

        if ((grounded && stasisTimer == 0) || !timeForm)
			stasis = false;
	}

	public IEnumerator StasisTimer(){
		timerTrigger = false;
        stasisTimer = 3.0f;
        yield return new WaitForSeconds(stasisTimer);
        
        stasisTimer = 0f;
        timerTrigger = true;
	}

	void Fall(){
		if (!grounded && Input.GetKey (power2))
			rb.gravityScale = 10;
		if (grounded)
			rb.gravityScale = 1;
	}

	void Record(){
		playerPositions.Insert (0, transform.position);

	}

	void Rewind(){
    
		transform.position = playerPositions [0];
		playerPositions.RemoveAt (0);
        
	}

    void colorChange()
    {
        if (Input.GetKey(formShift) && timeForm && !shifted && timerTrigger){
            renderer.color = new Color(0f, 0f, 0f, 1f);
            timeForm = false;
            shifted = true;
        }
        else if (Input.GetKey(formShift) && !timeForm && !shifted)
        {
            renderer.color = new Color(255f, 255f, 255f, 1f);
            timeForm = true;
            shifted = true;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
        else
            transform.parent = null;
    }
   

    }

