using UnityEngine;
using System.Collections;

public class FallAtTime : MonoBehaviour {

	private Rigidbody2D body;
	public float secondFromStar;
	// Use this for initialization
	void Start () {
		body = rigidbody2D;
		StartCoroutine(StartFall());
	}

	IEnumerator StartFall(){
		body.gravityScale = 0.0f;
		yield return new WaitForSeconds(secondFromStar);
		body.gravityScale = 1.0f;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
