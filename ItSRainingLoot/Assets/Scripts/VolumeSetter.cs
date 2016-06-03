using UnityEngine;
using System.Collections;

public class VolumeSetter : MonoBehaviour
{

		public float volume;
		public AudioSource song;

		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
				song.volume = volume;
		}
}
