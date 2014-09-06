using UnityEngine;
using System.Collections;

public class CatchMe : MonoBehaviour {

	private bool added;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DropMe(Vector3 position){
		this.gameObject.transform.position = Vector3.zero;
		this.gameObject.SetActive (true);
	}

	void OnTriggerEnter2D(Collider2D c){
		HaveThePeach script = c.gameObject.GetComponent<HaveThePeach> ();
		if (script != null) {
			script.haveItem = true;
			if(!added){
			script.Peaches++;
				added = true;
			}
			Destroy(this.gameObject);
		}
	}
}
