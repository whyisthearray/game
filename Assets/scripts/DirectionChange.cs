using UnityEngine;
using System.Collections;

public class DirectionChange : MonoBehaviour {

	public GameObject player;

	public Vector2 accessX; //x - right  y - left
	public Vector2 accessY; //x - up  y - down

	private Vector2 triggerPosition;
	private float prevDir;
	private bool inTrigger;


	void Start () {		
		this.triggerPosition = this.transform.position;
		this.enabled = false;
	}

	private float SignVector(Vector2 vec){
		return Mathf.Sign (vec.x + vec.y);
	}
		
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Enter SUKA");
		Vector2 plPos = player.transform.position;
		var normalVec = triggerPosition - plPos;
		prevDir = SignVector (normalVec.normalized);
		this.enabled = true;
	}
		
	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Exit");
		this.enabled = false;
	}

	void Update(){		
		Vector2 plPos = player.transform.position;
		var normalVec = triggerPosition - plPos;
		if (inTrigger) {
			var dir = normalVec.normalized;
			if (dir == Vector2.zero)
				return;
			if (dir.x != 0f)
				player.GetComponent<PlayerMoving> ().SetNewAccessMoves (Vector2.one, Vector2.zero);
			else
				player.GetComponent<PlayerMoving> ().SetNewAccessMoves (Vector2.zero, Vector2.one);
			inTrigger = false;
			return;
		}
		var currDir = SignVector (normalVec.normalized);
		if (currDir == prevDir ) {
			prevDir = currDir;
			return;
		}
		if (prevDir == 0f) {
			prevDir = currDir;
			return;
		}			
		player.transform.position = triggerPosition;
		player.GetComponent<PlayerMoving> ().SetNewAccessMoves (this.accessX, this.accessY);
		prevDir = 0f;
		inTrigger = true;
	}
}
