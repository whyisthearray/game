using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {


	
	// Update is called once per frame



	void OnCollisionEnter2D (Collision2D coll){
		if (coll.gameObject.tag == "wall")
			Debug.Log ("asd");
	}
	void OnCollisionExit2D(Collision2D coll){}
}
