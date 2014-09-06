using UnityEngine;
using System.Collections;

public class DifficultySelector : MonoBehaviour {

	public bool begin;
	public GUIText countdown;

	public GUISkin skin;
	public IndieQuiltCommunicator communicator;
	private int difficulty;
	public GameObject[] Levels;
	private GameObject activeLevel;
	private bool hide;

	public float XSDifficulty;
	public float YSDifficulty;
	public float XDDifficulty;
	public float YDDifficulty;
	public float XSPlus;
	public float YSPlus;
	public float XDPlus;
	public float YDPlus;
	public float XSMinus;
	public float YSMinus;
	public float XDMinus;
	public float YDMinus;
	public float XSStart;
	public float YSStart;
	public float XDStart;
	public float YDStart;
	// Use this for initialization
	void Start () {
		difficulty = communicator.difficulty;
		if(difficulty==0){
			difficulty = 1;
			activeLevel = Levels[difficulty-1];
			activeLevel.SetActive(true);
		}
		else{
			activeLevel = Levels[difficulty-1];
			activeLevel.SetActive(true);
		}
		countdown.enabled = false;

		if(PlayerPrefs.GetInt("CTPInstGiv")==null)
			PlayerPrefs.SetInt("CTPInstGiv", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StartCountDown(){
		countdown.enabled = true;
		countdown.text = "";
		if(PlayerPrefs.GetInt("CTPInstGiv")==0){
			countdown.text = "Take ALL 5 peaches";
			yield return new WaitForSeconds(2.0f);
			countdown.text = "30 seconds";
			yield return new WaitForSeconds(2.0f);
			countdown.text = "Do NOT get caught";
			yield return new WaitForSeconds(2.0f);
			countdown.text = "Run & Walljump";
			PlayerPrefs.SetInt("CTPInstGiv", 1);
		}
		for(int i = 0;i<=3;i++){
			yield return new WaitForSeconds(1.0f);
			countdown.text = ""+(3-(i));
		}
		begin = true;
	}

	void OnGUI(){
		//delete
		float width = Screen.width;
		float height = Screen.height;
		GUI.skin = skin;
		if(!hide){
			if(GUI.Button(new Rect(width/XSPlus,height/YSPlus,width/XDPlus,height/YDPlus),"+")&&difficulty<10){
				activeLevel.SetActive(false);
				difficulty++;
				activeLevel = Levels[difficulty-1];
				activeLevel.SetActive(true);
				}
			if(GUI.Button(new Rect(width/XSMinus,height/YSMinus,width/XDMinus,height/YDMinus),"-")&&difficulty>1){
				activeLevel.SetActive(false);
				difficulty--;
				activeLevel = Levels[difficulty-1];
				activeLevel.SetActive(true);
			}
			GUI.Label(new Rect(width/XSDifficulty,height/YSDifficulty,width/XDDifficulty,height/YDDifficulty),""+difficulty);
			if(GUI.Button(new Rect(width/XSStart,height/YSStart,width/XDStart,height/YDStart),"Start Game")){
				Time.timeScale = 1.0f;
				hide = true;
				StartCoroutine(StartCountDown());
			}
		}
		//delete
	}
}
