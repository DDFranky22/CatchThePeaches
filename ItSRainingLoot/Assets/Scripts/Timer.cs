using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
		public float secondsRemaning;
		private VictoryChecker scriptVictory;
		public Text textOnScreen;
		public bool stop;
		private DifficultySelector start;
		private bool started;

		// Use this for initialization
		void Start ()
		{
				scriptVictory = GameObject.Find ("CheckVictory").GetComponent<VictoryChecker> ();
				start = Camera.main.GetComponent<DifficultySelector> ();
		}

		IEnumerator CountDown ()
		{
				textOnScreen.text = "" + secondsRemaning;
				while (secondsRemaning > 0 && !stop) {
						yield return new WaitForSeconds (1.0f);
						secondsRemaning--;
						textOnScreen.text = "" + secondsRemaning;
				}
				if (secondsRemaning == 0) {
						scriptVictory.Timeout = true;
				}
		}

		// Update is called once per frame
		void Update ()
		{
				if (!started && start.begin) {
						StartCoroutine (CountDown ());
						started = true;
				}
		}
}
