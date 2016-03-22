using UnityEngine;
using System.Collections;

public class PlayerMoving : MonoBehaviour {

	public Collider2D wall;



	private Vector3 screenPoint;
	private Vector3 offset;

	private Vector3 prevPos;


	void OnMouseDrag(){
		prevPos = transform.position;
		var curScreenPoint = new Vector3 (
			Input.mousePosition.x, 
			Input.mousePosition.y,
			screenPoint.z);

		var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
		var col = GetComponent<Collider2D> ();
		if (col.bounds.Intersects (wall.bounds))
			transform.position = prevPos;
		
	}



	void OnMouseDown(){
		screenPoint = Camera.main.ScreenToWorldPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void Update(){
		prevPos = transform.position;
		transform.Translate (Vector2.left*Time.deltaTime);
		var col = GetComponent<Collider2D> ();
		if (col.bounds.Intersects (wall.bounds))
			transform.position = prevPos;
	}

	void OnCollisionStay2D(Collision2D col){
		Debug.Log ("Stay");
	}

	void OnCollisionExit2D(Collision2D col){
		Debug.Log ("exit");
	}

	void OnCollisionEnter2D (Collision2D col){
			Debug.Log ("enter");
	}




}
