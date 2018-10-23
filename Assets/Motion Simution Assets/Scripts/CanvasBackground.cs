using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class CanvasBackground : MonoBehaviour 
{
	
	void Start () 
	{
		TogglePanel(0); 
	}
	
	public void TogglePanel(int index)
	{
		foreach(Transform t in transform)
		{
			t.GetComponent<CanvasGroup>().alpha = 0; 
			t.GetComponent<CanvasGroup>().blocksRaycasts = false; 
		}	


		transform.GetChild(index).GetComponent<CanvasGroup>().alpha = 1f; 

		transform.GetChild(index).GetComponent<CanvasGroup>().blocksRaycasts = true; 
	}
}
