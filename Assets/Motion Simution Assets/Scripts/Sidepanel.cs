using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Sidepanel : MonoBehaviour {

	public Text panelTitleText; 

	public Text forceText; 

	public Text forceDelayText; 

	public Text forceDurationText; 

	public Text initalVelocityText; 

	public void UpdatePanel(ForceSide panel)
	{
		panelTitleText.text = panel.title; 

		initalVelocityText.text = panel.initialVelocity.ToString(); 

		forceText.text = panel.force.ToString(); 

		forceDurationText.text = panel.forceDuration.ToString(); 

		forceDelayText.text = panel.forceDelay.ToString(); 
	}
}
