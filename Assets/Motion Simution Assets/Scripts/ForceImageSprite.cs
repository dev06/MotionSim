using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class ForceImageSprite : MonoBehaviour {

	public Color defaultColor; 
	
	void Start () 
	{
		defaultColor = GetComponent<Image>().color; 	
	}

}
