using UnityEngine;
using System.Collections;

public class PlayerMoving : MonoBehaviour {

	private Rigidbody2D rb;

	private Vector2 accessX; //x - right  y - left
	private Vector2 accessY; //x - up  y - down

	private bool isCanMoving;
	private Vector2 prevPosition;

	void Start () {	
		rb = GetComponent<Rigidbody2D> ();
		accessX = Vector2.one;
		accessY = Vector2.zero;
	}

	public void SetNewAccessMoves(Vector2 accX,Vector2 accY){
		this.accessX = accX;
		this.accessY = accY;
	}

	private bool IsTwoDirectional(Vector2 vec){
		return vec.x != 0f && vec.y != 0f;
	}
		
	void OnMouseDown(){
		isCanMoving = true;
		prevPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}

	void OnMouseUp(){
		isCanMoving = false;
	}
		
	void Update () {
		if (!isCanMoving)
			return;
		Vector2 newPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		var moveVec = newPosition - prevPosition;
		var direction = moveVec.normalized;
		var len = moveVec.magnitude;
		var velocity = direction * len;
		if (velocity.x > 0)
			velocity.x *= accessX.x;
		else
			velocity.x *= accessX.y;
		if (velocity.y > 0)
			velocity.y *= accessY.x;
		else
			velocity.y *= accessY.y;
		if (IsTwoDirectional (velocity)) {
			if (Mathf.Abs (velocity.x) > Mathf.Abs (velocity.y))
				velocity.y = 0f;
			else
				velocity.x = 0f;			
		}
		transform.position = Vector2.Lerp (rb.position, rb.position + velocity, 60f);
		prevPosition = newPosition;
	}
}
