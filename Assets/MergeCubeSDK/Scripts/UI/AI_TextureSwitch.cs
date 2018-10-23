using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_TextureSwitch : MonoBehaviour {
	public enum PLTEnum{
		android,ios
	}
	public PLTEnum plantform;
	// Use this for initialization
	void Awake () {
		if (Application.platform == RuntimePlatform.Android) {
			if (plantform == PLTEnum.ios) {
				Destroy (gameObject);
			}
		} else {
			if (plantform == PLTEnum.android) {
				Destroy (gameObject);
			}
		}
	}
}
