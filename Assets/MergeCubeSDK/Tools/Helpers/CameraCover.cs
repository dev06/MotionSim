using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCover : MonoBehaviour {
	public static CameraCover instance;
	void Awake(){
		instance = this;
	}
	public void Die(){
		Invoke ("SelfDestroy", .5f);
	}
	void SelfDestroy(){
		Destroy (gameObject);
	}
}
