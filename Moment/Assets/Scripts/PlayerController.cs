using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	

	List<Vector2> position;
	public float maxSpeed = 10f;
	bool facingRight = true;
	Animator anim;
	public float jumpForce = 700f;
	bool grounded = false;
	bool doubleJump = false;
	bool stasis = false;
	bool timerTrigger = false;
	bool isRewinding = false;
    
	Rigidbody2D rb;
    new SpriteRenderer renderer;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
    bool timeForm = true;

	public KeyCode power1;

	// Use this for initialization	
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D> ();
        renderer = GetComponent<SpriteRenderer>();
		position = new List<Vector2> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
        colorChange();
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);


		anim.SetFloat ("Speed", GetComponent<Rigidbody2D> ().velocity.y);

		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs (move));
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		if (move > 0 && !facingRight)
			Flip();
		else if (move < 0 && facingRight)
			Flip();
		if (Input.GetKeyDown(KeyCode.V))
			isRewinding = true;
		if (Input.GetKeyUp (KeyCode.V))
			isRewinding = false;
		if (isRewinding && !grounded)
			Rewind();
		else
			Record ();
		}

	void Update(){
		if (grounded && Input.GetKeyDown (KeyCode.Space)){
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));

		}

		if (!doubleJump && !grounded && Input.GetKeyDown (power1)) {
			doubleJump = true;
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		}

		if(grounded)
			doubleJump = false;
		Stasis ();
		Fall ();

	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Stasis(){
		if (!grounded && Input.GetKeyDown (KeyCode.S) && !stasis) {
			gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			stasis = true;
			StartCoroutine(StasisTimer ());
		}

		if (stasis && timerTrigger) {
			
			gameObject.transform.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		}

		if (grounded)
			stasis = false;
	}

	public IEnumerator StasisTimer(){
		timerTrigger = false;
		yield return new WaitForSeconds (3f);
		timerTrigger = true;
	}

	void Fall(){
		if (!grounded && Input.GetKeyDown (KeyCode.X))
			rb.gravityScale = 10;
		if (grounded)
			rb.gravityScale = 1;
	}

	void Record(){
		position.Insert (0, transform.position);

	}

	void Rewind(){
		
		transform.position = position [0];
		position.RemoveAt (0);
	}

	public void startRewinding(){
		isRewinding = true;
	}

    void colorChange()
    {
        if (Input.GetKeyDown(KeyCode.B) && timeForm){
            renderer.color = new Color(0f, 0f, 0f, 1f);
            timeForm = false;
        }
        else if (Input.GetKeyDown(KeyCode.B) && !timeForm)
        {
            renderer.color = new Color(255f, 255f, 255f, 1f);
            timeForm = true;
        }
    }

}

	