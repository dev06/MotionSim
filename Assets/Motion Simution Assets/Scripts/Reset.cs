using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {

	private Vector3 position; 
	void Start () 
	{
		position = transform.position; 	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetTransform()
	{
		transform.position = position; 
	}
}
