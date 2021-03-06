﻿using UnityEngine;
using System.Collections;

public class VictoryChecker : MonoBehaviour {

	public Timer timerscript;
	public bool Timeout;
	public bool Busted;
	public HaveThePeach catched;
	public CreatePeach created;
	public GUIText textOnScreen;
	public bool endGame;

	private bool oneShot; //one opporinity

	public IndieQuiltCommunicator Communicator;

	public AudioSource[] musics;

	// Use this for initialization
	void Start () {
		timerscript = GameObject.Find("Timer").GetComponent<Timer>();
	}
	
	// Update is called once per frame
	void Update () {
		checkVictory();
	}

	void checkVictory(){
		if(Timeout&&catched.Peaches!=created.NumberOfPeaches){
			textOnScreen.text = "Game Over";
			timerscript.stop = true;
			Communicator.success = false;
			endGame= true;
			musics[2].Stop();
			if(!musics[0].isPlaying&&!oneShot){
				musics[0].Play();
				oneShot = true;
			}
			StartCoroutine(GameOver());
			return;
		}
		if(catched.Peaches==created.NumberOfPeaches&&!Timeout){
			textOnScreen.text = "You Win";
			timerscript.stop = true;
			Communicator.success = true;
			endGame= true;
			musics[2].Stop();
			if(!musics[1].isPlaying&&!oneShot){
				musics[1].Play();
				oneShot = true;
			}
			StartCoroutine(GameOver());
			return;
		}
		if(Busted){
			textOnScreen.text = "Game Over";
			timerscript.stop = true;
			Communicator.success = false;
			endGame= true;
			musics[2].Stop();
			if(!musics[0].isPlaying&&!oneShot){
				musics[0].Play();
				oneShot = true;
			}
			StartCoroutine(GameOver());
			return;
		}
	}

	IEnumerator GameOver(){
		yield return new WaitForSeconds(2.0f);
		Communicator.finished = true;
	}

}
