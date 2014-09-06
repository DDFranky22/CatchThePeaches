using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

	public bool check;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D c){
		check = true;
	}

	void OnTriggerEnter2D(Collider2D c){
		check = true;
	}

	void OnTriggerExit2D(Collider2D c){
		check = false;
	}
}
