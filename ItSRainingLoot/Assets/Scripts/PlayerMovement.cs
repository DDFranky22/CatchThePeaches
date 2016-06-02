using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{

	private DifficultySelector start;

	public VictoryChecker check;

	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
	public float afterJumpUpForce = 2.0f;
	public float afterJumpDownforce = 3.5f;
	public float WallJumpHorizontalForce = 3.0f;

	public bool WallJumpReady;
	public int WallPosition;
	
	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;
	
	private CharacterController2D _controller;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	
	private bool jumped;

	
	void Awake()
	{
		start = Camera.main.GetComponent<DifficultySelector> ();
		_controller = GetComponent<CharacterController2D>();
		
		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}
	
	
	#region Event Listeners
	
	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;
		
		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}
	
	
	void onTriggerEnterEvent( Collider2D col )
	{
		if (col.gameObject.layer.Equals(11)) {
			WallJumpReady = true;
			if(col.gameObject.transform.position.x>this.gameObject.transform.position.x){
				WallPosition = 1;
			}
			else{
				WallPosition = -1;
			}
		}
	}
	
	
	void onTriggerExitEvent( Collider2D col )
	{
		WallPosition = 0;
		WallJumpReady = false;
	}
	
	#endregion

	private void WallJump(int x){
		_velocity.x = x*WallJumpHorizontalForce;
	}


	private void Movement(Vector2 _velocity){
		
		if( _controller.isGrounded )
			_velocity.y = 0;
		
		float horizontalMovement = Input.GetAxis ("Horizontal");
		
		if( Mathf.Abs(horizontalMovement)>=0.19f )
		{
			normalizedHorizontalSpeed = horizontalMovement;
			if(normalizedHorizontalSpeed>0)
				if( transform.localScale.x > 0f )
					transform.localScale = new Vector3( -1*transform.localScale.x, transform.localScale.y, transform.localScale.z );
			if(normalizedHorizontalSpeed<0)
				if( transform.localScale.x < 0f )
					transform.localScale = new Vector3( -1*transform.localScale.x, transform.localScale.y, transform.localScale.z );
		}
		else
		{
			normalizedHorizontalSpeed = 0;
		}
	}

	private void Jump(){
			if ((_controller.isGrounded||WallJumpReady) && Input.GetAxis ("Jump") > 0.0f&&!jumped) {
			if(WallJumpReady){
				WallJump(-WallPosition);
			}
			_velocity.y = Mathf.Sqrt (afterJumpUpForce * jumpHeight * -gravity);
			jumped = true;
		} 
		else if(Input.GetAxis("Jump")<=0&&jumped){
			_velocity.y -= Mathf.Sqrt (afterJumpDownforce * jumpHeight);
			jumped = false;
		}
	}

	
	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		if(!check.endGame&&start.begin){
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
		
		Movement(_velocity);

		Jump ();
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;
		
		_controller.move( _velocity * Time.deltaTime );
		}
	}
	
}
