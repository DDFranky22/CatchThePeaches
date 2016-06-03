using UnityEngine;
using System.Collections;

public class CameraSizeAdapting : MonoBehaviour
{
	
		public float F54;
		public float F43;
		public float F32;
		public float F1610;
		public float F169;
	
	
		// Use this for initialization
		void Start ()
		{
				Adapt ();
		}

		void Adapt ()
		{
				float width = Screen.width;
				float height = Screen.height;
				float div = width / height;
				if (div <= 1.25f) {
						Camera.main.orthographicSize = F54;
						return;
				}
				if (div > 1.25f && div < 1.4f) {
						Camera.main.orthographicSize = F43;
						return;
				}
				if (div > 1.4f && div <= 1.5f) {
						Camera.main.orthographicSize = F32;
						return;
				}
				if (div > 1.5f && div <= 1.6f) {
						Camera.main.orthographicSize = F1610;
						return;
				}
				if (div > 1.6 && div < 1.8f) {
						Camera.main.orthographicSize = F169;
						return;
				}
		}
}
