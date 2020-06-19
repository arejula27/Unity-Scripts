using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	
	
	public int maxJumps = 1;
	public float jumpForce = 500;
	public float moveSpeed = 50.0f;
	public float maxMoveSpeed = 10.0f;
	public LayerMask whatIsGround;
	public Transform footPos;
	public float slowdowmjump = 2;

	private float horizontal;
	private Rigidbody2D rb;
	private bool grounded;
	private int currentJumps;
	private SpriteRenderer sprite;
	private float inveslowdowmjump;
	private bool jumping = false;
	// Start is called before the first frame update
	void Start()
	{
		inveslowdowmjump = 1 / slowdowmjump;
		currentJumps = maxJumps;
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		
		horizontal = Input.GetAxis("Horizontal");
		Move(horizontal);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}

	}

	//salto
	protected void Jump()
	{
		
		if (currentJumps < 1) return;

		jumping = true;
		currentJumps--;
		rb.velocity = new Vector2(rb.velocity.x, 0);
		rb.AddForce(Vector2.up * jumpForce);
		
	}

    private void LateUpdate()
    {
		//CheckGrounded();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if ((col.gameObject.tag == "Floor")&& jumping)
		{
			jumping = false;
			currentJumps = maxJumps;
		}
	}



	protected void Move(float dir)
	{
		//flip player
		FlipActor(dir);
		float realspeed = moveSpeed;
		if (jumping) realspeed *= inveslowdowmjump;
        
		if (Mathf.Abs(rb.velocity.x) < maxMoveSpeed)
        {
			rb.velocity = new Vector2(dir* realspeed, rb.velocity.y);

		}
        else
        {
			rb.velocity = new Vector2(0, rb.velocity.y);


		}
        
			

	}

	private void FlipActor(float dir)
	{
		if ( dir < 0) 
		{
			sprite.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

		}
		else if(dir > 0)
        {

			sprite.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		}
	}
}