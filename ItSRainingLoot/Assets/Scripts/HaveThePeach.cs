using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HaveThePeach : MonoBehaviour {

	public bool haveItem;
	public GameObject Item;

	public int Peaches;
	public Text peachesIndicator;
	// Use this for initialization
	void Start () {
	
	}

	public void LoseItem(){
		CatchMe scriptCatch = Item.GetComponent<CatchMe> ();
		scriptCatch.DropMe (this.transform.position);
		haveItem = false;
	}


	// Update is called once per frame
	void Update () {
		peachesIndicator.text = "X"+Peaches;
	}
}
