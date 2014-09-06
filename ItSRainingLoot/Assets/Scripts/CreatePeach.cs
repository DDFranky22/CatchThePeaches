using UnityEngine;
using System.Collections;

public class CreatePeach : MonoBehaviour {
	public float NumberOfPeaches;
	public GameObject peachPrefab;
	private float seconds;
	private DifficultySelector start;
	private bool started;

	void Start(){
		start = Camera.main.GetComponent<DifficultySelector>();
	}

	// Use this for initialization
	void CreatePeaches () {
		Collider2D collider = collider2D;
		for(int i = 0;i<NumberOfPeaches;i++){
			GameObject peach = Instantiate(peachPrefab,new Vector3(Random.Range(this.transform.position.x-collider.bounds.size.x/2,this.transform.position.x+collider.bounds.size.x/2),this.transform.position.y-collider.bounds.size.y),Quaternion.identity) as GameObject;
			FallAtTime script = peach.GetComponent<FallAtTime>();
			script.secondFromStar = seconds;
			seconds +=5.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!started&&start.begin){
			started = true;
			CreatePeaches();
		}
	}
}
