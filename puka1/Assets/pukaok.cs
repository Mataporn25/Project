using UnityEngine;
using System.Collections;

public class pukaok : MonoBehaviour {
	
	Vector3 velocity = Vector3.zero;
	public Vector3 gravity;
	public Vector3 flapVelocity;
	public float maxSpeed = 5f;
	public float forwardSpeed = 1f;
	
	bool didFlap = false;
	Animator animator;

	bool dead = false;
	
	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator> ();
		if (animator == null) {
		 Debug.LogError ("Didn't find animator!");
		}
	}
	// Do Graphic &Input updates here
	void Update(){
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
			didFlap = true;

		}
	}
	
	// Do physics engine updates here
	void FixedUpdate () {

		if (dead)
			return;

		velocity.x = forwardSpeed;
		velocity += gravity * Time.deltaTime;
		
		if (didFlap) {
			animator.SetTrigger ("DoFlap");
			didFlap = false;
			if(velocity.y < 0)
				velocity.y = 0;
			
			velocity += flapVelocity;
		}
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		
		//transform.position += velocity * Time.deltaTime;
		rigidbody2D.velocity = new Vector2(velocity.x, velocity.y);
		
		float angle = 0;
		if (velocity.y < 0) {
			angle = Mathf.Lerp (0, -20, -velocity.y / maxSpeed);
		}
		transform.rotation = Quaternion.Euler (0, 0, angle);
		
	}
	void OnCollisionEnter2D(Collision2D collision){
		animator.SetTrigger ("Deat");
		dead = true;
	}
}
