using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.UI; 
public enum ButtonID
{
	None, 
	ToggleSimulation,
	Back,
	Clear,
	VelocityGraph, 
	AccelerationGraph, 
	PositionGraph,
	CreateGraph,
	InfoBack,
}
public class SimpleButtonHandler : MonoBehaviour, IPointerClickHandler {

	public ButtonID buttonID; 


	public void OnPointerClick(PointerEventData data)
	{
		if(EventManager.OnButtonClick != null)
		{
			EventManager.OnButtonClick(buttonID); 
		}

		if(buttonID == ButtonID.ToggleSimulation)
		{
			MainController.Instance.ToggleSimulation(); 

			GetComponentInChildren<Text>().text = MainController.SIMULATION_STATUS ? "Stop" : "Start"; 

			if(MainController.SIMULATION_STATUS == false)
			{
				FindObjectOfType<ShipMovement>().Reset(); 
			}
		}


		if(buttonID == ButtonID.Clear)
		{
			MainController.SELECTED_FORCE_SIDE.ResetValues(); 
		}

		if(buttonID == ButtonID.Back)
		{
			FindObjectOfType<CanvasBackground>().TogglePanel(0); 
		}
	}
}
