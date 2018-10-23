using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public enum StatType
{
	InitalVelocity, 
	ForceStartDelay, 
	ForceDuration,
	Force
}

public class StatBehaviour : MonoBehaviour 
{

	private ShipMovement ship;

	public StatType type; 

	public Text valueText; 

	private float addition = .5f;

	void OnEnable()
	{
		EventManager.OnUpdateUI += OnUpdateUI; 
	}

	void OnDisable()
	{
		EventManager.OnUpdateUI -= OnUpdateUI; 
	}

	void Start () 
	{
		ship = FindObjectOfType<ShipMovement>(); 	
	}
	
	void UpdateUI () 
	{
		if(type == StatType.InitalVelocity)
		{
			valueText.text = MainController.SELECTED_FORCE_SIDE.initialVelocity.ToString(); 
		}

		if(type == StatType.ForceStartDelay)
		{
			valueText.text = MainController.SELECTED_FORCE_SIDE.forceDelay.ToString(); 
		}

		if(type == StatType.Force)
		{
			valueText.text = MainController.SELECTED_FORCE_SIDE.force.ToString(); 
		}

		if(type == StatType.ForceDuration)
		{
			valueText.text = MainController.SELECTED_FORCE_SIDE.forceDuration.ToString(); 
		}
	}

	public void OnMinus()
	{

		switch(type)
		{
			case StatType.InitalVelocity: 
			{
				MainController.SELECTED_FORCE_SIDE.ModifyInitialVelocity(-addition); 
				break; 		
			}

			case StatType.ForceStartDelay:
			{
				MainController.SELECTED_FORCE_SIDE.ModifyForceDelay(-addition); 
				break; 
			}

			case StatType.ForceDuration:
			{
				MainController.SELECTED_FORCE_SIDE.ModifyForceDuration(-addition); 
				break; 
			}
			case StatType.Force:
			{
				MainController.SELECTED_FORCE_SIDE.ModifyForce(-addition); 
				break; 
			}
		}

	}

	public void OnPlus()
	{

		switch(type)
		{
			case StatType.InitalVelocity: 
			{
				MainController.SELECTED_FORCE_SIDE.ModifyInitialVelocity(addition); 
				break; 		
			}

			case StatType.ForceStartDelay:
			{
				MainController.SELECTED_FORCE_SIDE.ModifyForceDelay(addition); 
				break; 
			}

			case StatType.ForceDuration:
			{
				MainController.SELECTED_FORCE_SIDE.ModifyForceDuration(addition); 
				break; 
			}
			case StatType.Force:
			{
				MainController.SELECTED_FORCE_SIDE.ModifyForce(addition); 
				break; 
			}

		}
	}


	private void OnUpdateUI()
	{
		UpdateUI(); 	
	}
}

public class EventManager 
{
	public delegate void StateModified(StatType type, string op); 
	public static StateModified OnStateModified; 

	public delegate void Game(); 
	public static Game OnUpdateUI; 

	public delegate void SimuationStatus(bool b); 
	public static SimuationStatus OnSimulationStatus; 



	public delegate void ButtonClick(ButtonID id); 
	public static ButtonClick OnButtonClick; 
}
