using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float velocity = 10f;
	public float jumpVelocity = 10f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

    private Rigidbody2D rgbd2D;
    private GameObject planet;
	private bool isGrounded;
	private float groundCheckRadius = 0.2f;
	private bool jumping;

    // Use this for initialization
    void Start ()
    {
		this.rgbd2D = GetComponent<Rigidbody2D>();
		this.isGrounded = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetAxis("Jump") > 0) {
			this.jumping = true;
		}	
	}

    void FixedUpdate ()
    {      
        if (this.planet)
        {
			
            Vector2 position = this.planet.transform.position - this.transform.position;
			float angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
			this.rgbd2D.rotation = angle + 90;

			this.isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundCheckRadius, this.whatIsGround);
			if (this.isGrounded && !this.jumping) {
				float move = Input.GetAxis("Horizontal") * this.velocity;
				float verticalVelocity = Vector2.Dot(this.transform.up, this.rgbd2D.velocity);
				this.rgbd2D.velocity = this.transform.right * move + this.transform.up * verticalVelocity;
			}
			if (this.jumping) {
				this.jumping = false;
				if (this.isGrounded) {
					float horizontalVelocity = Vector2.Dot(this.transform.right, this.rgbd2D.velocity);
					this.rgbd2D.velocity = this.transform.right * horizontalVelocity + this.transform.up * jumpVelocity;
				}
			}
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
		Debug.Log (other);
        this.planet = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
		Debug.Log (other);
        this.planet = null;
    }
}
