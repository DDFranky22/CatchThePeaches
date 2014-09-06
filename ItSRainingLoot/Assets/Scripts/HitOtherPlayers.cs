using UnityEngine;
using System.Collections;

public class HitOtherPlayers : MonoBehaviour {
	
	public PlayerMovement scriptMovement;
	public int NumberPlayer;
	public bool pressed;
	
	// Use this for initialization
	void Start () {
		scriptMovement = GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Hit_" + NumberPlayer) > 0 && !pressed) {
			pressed = true;
		}
		if (Input.GetAxis ("Hit_" + NumberPlayer) <= 0) {
			pressed = false;		
		}
	}
	
	void Hit(GameObject enemy){
		HaveThePeach havePeachScript = enemy.GetComponent<HaveThePeach> ();
		if(havePeachScript!=null)
		if (havePeachScript.haveItem) {
			havePeachScript.LoseItem();		
		}
	}
	
	void OnTriggerStay2D(Collider2D collider){
		if (pressed) {
			Hit (collider.gameObject);
		}
	}
	
}
