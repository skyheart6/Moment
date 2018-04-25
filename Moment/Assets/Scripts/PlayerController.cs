using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	

	List<Vector2> playerPositions;
	public float maxSpeed = 10f;
	bool facingRight = true;
	Animator anim;
	public float jumpForce;
	public bool grounded = false;
	bool doubleJump = false;
	public bool stasis = false;
	bool timerTrigger = true;
	public bool isRewinding = false;
    bool shifted = false;
    public bool rewound = false;
	Rigidbody2D rb;
    new SpriteRenderer renderer;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
    public bool timeForm = false;
    public float stasisTimer;
    public int keyframe = 5;
    private int frameCounter = 0;
    private int reverseCounter = 0;
    private Vector2 currentPosition;
    private Vector2 previousPosition;
    private bool firstRun = true;
    public int count;
    public Text power1Text, power2Text, formShiftText, jumpText;
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public string testText;
    KeyCode power1;
    KeyCode power2;
    KeyCode formShift;
    KeyCode jump;
    private GameObject currentKey;
    bool paused = false;


    // Use this for initialization	
    void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D> ();
        renderer = GetComponent<SpriteRenderer>();
		playerPositions = new List<Vector2> ();
        keys.Add("Power 1", KeyCode.Z);
        keys.Add("Power 2", KeyCode.X);
        keys.Add("Form Shift", KeyCode.C);
        keys.Add("Jump", KeyCode.Space);

        power1Text.text = keys["Power 1"].ToString();
        testText = keys["Power 1"].ToString();
        power2Text.text = keys["Power 2"].ToString();
        formShiftText.text = keys["Form Shift"].ToString();
        jumpText.text = keys["Jump"].ToString();

        power1 = keys["Power 1"];
        power2 = keys["Power 2"];
        formShift = keys["Form Shift"];
        jump = keys["Jump"];

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

        if (grounded)
        {
            rewound = false;
            rb.gravityScale = 1;
        }
        Power1();
        Power2();
        //if (playerPositions.Count > 128)
        //{
        //    playerPositions.RemoveRange(0,20);
        //}

    }

	void Update(){


        
        if (grounded && Input.GetKeyDown (jump)){
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));

		}
      
        count = playerPositions.Count;

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
            if (Input.GetKey(power2) && !rewound)
            {
                isRewinding = true;
                
            }
                
            if (Input.GetKeyUp(power2) || grounded)
            {
                isRewinding = false;
              
            }
            if (!isRewinding && Input.GetKeyUp(power2))
            {
                rewound = true;
            }

            if (isRewinding && !grounded)
               
            {
                //gameObject.transform.GetComponent<Rigidbody2D>().simulated = false;
                Rewind();
                //gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                

            }
            else {
                //gameObject.transform.GetComponent<Rigidbody2D>().simulated = true;
                Record();
                transform.tag = "Player";
                
                /*gameObject.transform.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic*/
                ;
            }
        }
        else {
            Record();
            Fall();
        }
        ChangeKey();
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
			//gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.transform.GetComponent<Rigidbody2D>().simulated = false;
            stasis = true;
			StartCoroutine(StasisTimer ());
		}

		if (stasis && timerTrigger) {
			
			//gameObject.transform.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
            //gameObject.transform.GetComponent<Rigidbody2D>().simulated = true;
			gameObject.transform.GetComponent<Rigidbody2D>().simulated = true;
        }

        if ((grounded && stasisTimer == 0) || !timeForm)
			stasis = false;
	}

	public IEnumerator StasisTimer(){
		timerTrigger = false;
        stasisTimer = 1.8f;
		renderer.color = new Color(100f, 0f, 0f, 1f);
        yield return new WaitForSeconds(stasisTimer);
		renderer.color = new Color(255f, 255f, 255f, 1f);
        stasisTimer = 0f;
        timerTrigger = true;
	}

	void Fall(){
		if (!grounded && Input.GetKey (power2))
			rb.gravityScale = 10;
		
	}

	void Record(){
        //if (playerPositions.Count > 128)
        //{
        //    playerPositions.RemoveAt(0);
        //    playerPositions.RemoveAt(1);
        //    playerPositions.RemoveAt(2);
        //    playerPositions.RemoveAt(3);
        //}
        if (!isRewinding)
        {
            if (frameCounter < keyframe)
            {
                frameCounter += 1;
            }
            //playerPositions.Insert (0, transform.position);
            else
            {
                frameCounter = 0;
                playerPositions.Insert(0, transform.position);
                
            }
        }
        else {
            if (reverseCounter > 0)
            {
                reverseCounter -=1;
            }
            else
            {
                //transform.position = playerPositions[playerPositions.Count -1];
                //playerPositions.RemoveAt(playerPositions.Count - 1);
                reverseCounter = keyframe;
                RestorePositions();
            }
            if (firstRun)
            {
                firstRun = false;
                RestorePositions();
            }
            float interpolation = (float)reverseCounter / (float)keyframe;
            transform.position = Vector2.Lerp(previousPosition, currentPosition, interpolation);

        }
    }

	void Rewind(){
        if (playerPositions.Count != 0)
        {
            transform.position = playerPositions[0];
            playerPositions.RemoveAt(0);
            transform.tag = "Untagged";
        }
        
	}

    void colorChange()
    {
        if (Input.GetKey(formShift) && timeForm && !shifted && timerTrigger){
            renderer.color = new Color(0f, 0f, 0f, 1f);
            timeForm = false;
            shifted = true;
            gameObject.transform.GetComponent<Rigidbody2D>().simulated = true;
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

    void RestorePositions()
    {
        int lastIndex = playerPositions.Count - 1;
        int secondToLastIndex = playerPositions.Count - 2;
        if (secondToLastIndex >= 0)
        {
            currentPosition = (Vector2)playerPositions[lastIndex];
            previousPosition = (Vector2)playerPositions[secondToLastIndex];
            playerPositions.RemoveAt(lastIndex);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Death" || other.transform.tag == "Respawn")
        {
            playerPositions.Clear();
        }
    }

    public void ChangeKey()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }


}

