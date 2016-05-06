using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface TriggerDvig{
	IEnumerator LeftDvig ();
	IEnumerator RightDvig ();
}
	
public class TriggerScript : MonoBehaviour {
	
	GameObject golovka;

	Vector2 FirstPoint;
	Vector2 SecondPoint;

	bool isFP;
	bool isSP;

	Vector2 prevPos;

	private Vector2 offset;
	private Vector2 fromOffsetToSP;
	private Vector2 fromOffsetToFP;

	private Bounds fpBounds;
	private Bounds spBounds;

	Vector2 distances;
	float distBetweenPoints;

	private bool isDvig;

	public float rotating; //0-3

	void Awake () {
		var srenderer = GetComponent<SpriteRenderer> ();
		var extents = srenderer.bounds.extents;
		distances = new Vector2 ((extents.x * 3f) / 4f, (extents.y * 3f) / 4f);
		FirstPoint = new Vector2 (-distances.x, distances.y);
		SecondPoint = new Vector2 (distances.x, -distances.y);
		offset = new Vector2 (-distances.x, -distances.y);
		fromOffsetToFP = FirstPoint - offset;
		fromOffsetToSP = SecondPoint - offset;
		distBetweenPoints = Mathf.Abs (FirstPoint.x - SecondPoint.x);
		fpBounds = new Bounds (FirstPoint, new Vector2 (.1f, .1f));
		spBounds = new Bounds (SecondPoint, new Vector2 (.1f, .1f));
		transform.rotation = Quaternion.AngleAxis (rotating * 90f, Vector3.forward);
		golovka = GameObject.Find ("Head");
		golovka.SetActive (false);

		Debug.Log (Vector2.Angle (fromOffsetToFP, fromOffsetToSP));
	}

	void OnMouseDown(){
		Vector2 mpos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 triggerPos = this.transform.InverseTransformPoint (mpos);
		if (fpBounds.Contains (triggerPos)) {
			golovka.SetActive (true);
			golovka.transform.localPosition = FirstPoint;
			isFP = true;
			prevPos = mpos;
			return;
		}
		if (spBounds.Contains (triggerPos)) {
			golovka.SetActive (true);
			golovka.transform.localPosition = SecondPoint;
			isSP = true;
			prevPos = mpos;
			return;
		}
	}



	private class TrigTest:TriggerDvig{
		 public IEnumerator LeftDvig (){
			Debug.Log ("Left Suka");
			yield return null;
		}
		 public IEnumerator RightDvig (){
			Debug.Log ("Right suka");
			yield return null;
		}
	}
		
	void OnMouseUp(){
		golovka.SetActive (false);
		isFP = false;
		isSP = false;
	}

	Vector2 golovkaPos;

	void OnMouseDrag(){
		TrigTest asd = new TrigTest ();
		Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
		Vector2 moveVec = newPos - prevPos;
		prevPos = newPos;
		var len = moveVec.magnitude;
		var dir = this.transform.InverseTransformDirection (moveVec.normalized);
		golovkaPos = golovka.transform.localPosition;
		var alpha = Vector2.Angle (golovkaPos - offset, fromOffsetToSP);
		Vector2 newVector;
		if (alpha >= 45f) {
			var newX = golovka.transform.localPosition.x + (len * dir.x);
			var newY = Mathf.Sqrt (Mathf.Pow (distBetweenPoints, 2f) - Mathf.Pow (newX - offset.x, 2f)) + offset.y;
			newVector = new Vector2 (newX, newY);
		} else {
			var	newY = golovka.transform.localPosition.y + (len * dir.y);
			var newX = Mathf.Sqrt (Mathf.Pow (distBetweenPoints, 2f) - Mathf.Pow (newY - offset.y, 2f)) + offset.x;
			newVector = new Vector2 (newX, newY);
		}
		if (newVector.x <= FirstPoint.x || newVector.y >= FirstPoint.y) {
			golovka.transform.localPosition = FirstPoint;
			if (isFP)
				return;
			Debug.Log ("Azaza from 2 to 1");
			StartCoroutine (asd.LeftDvig());
			return;
		}
		if (newVector.x >= SecondPoint.x || newVector.y <= SecondPoint.y) {
			golovka.transform.localPosition = SecondPoint;
			if (isSP)
				return;
			Debug.Log ("azaza from 1 to 2");
			StartCoroutine (asd.RightDvig());
			return;
		}
		golovka.transform.localPosition = newVector;
	}



	void OnDrawGizmos(){
		var fp = this.transform.TransformPoint (new Vector3 (FirstPoint.x, FirstPoint.y, 0f));
		var sp = this.transform.TransformPoint (new Vector3 (SecondPoint.x, SecondPoint.y, 0f));
		var fps = new Vector3 (fpBounds.size.x, fpBounds.size.y, 0f);
		var sps = new Vector3 (spBounds.size.x, spBounds.size.y, 0f);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube (fp, fps);
		Gizmos.DrawWireCube (sp, sps);
	}

	// Update is called once per frame
	void Update () {
	

	}
}
