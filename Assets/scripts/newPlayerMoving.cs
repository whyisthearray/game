using UnityEngine;
using System.Collections;

public class newPlayerMoving : MonoBehaviour {

	private Vector3 prevPos;

	public float actionRadius;

	private SpriteRenderer playerRenderer;

	private bool canMoveLeft;
	private bool canMoveRight;

	void Start(){
		playerRenderer = this.GetComponent<SpriteRenderer> ();
	}

	void OnMouseDown(){
		var screenPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		prevPos = screenPoint;
		Debug.Log ("called");
	}


	void OnMouseDrag(){
		//Debug.Log("drag called");
		var newScreenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = (newScreenPoint - prevPos).normalized;
		var distance = (newScreenPoint - prevPos).magnitude;
		var angle = Vector2.Angle (direction, Vector2.right);
		if (direction.x >= .8 ) {

			transform.Translate (distance, 0, 0);
			prevPos = newScreenPoint;
			return;
		}
		if (direction.x <= -.8) {
			transform.Translate (-distance, 0, 0);
			prevPos = newScreenPoint;
			return;
		}
	}
}
