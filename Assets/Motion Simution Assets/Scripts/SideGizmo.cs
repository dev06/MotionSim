using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class ForceSide
{

	public string title; 

	public float force; 

	public float forceDelay; 

	public float forceDuration; 

	public float initialVelocity; 

	public float appliedForce; 

	public int multiplier = 0; 

	public float acceleration; 

	public ForceSide(string title)
	{

		this.title = title; 

		multiplier = 0; 

		force = 0; 

		forceDelay = 0; 

		forceDuration = 1; 
	}

	public void ModifyForce(float addition)
	{
		force+=addition; 

		force = Mathf.Clamp(force, 0, 10); 

		if(EventManager.OnUpdateUI != null)
		{
			EventManager.OnUpdateUI(); 
		}
	}

	public void ModifyForceDelay(float addition)
	{
		forceDelay+=addition; 

		forceDelay = Mathf.Clamp(forceDelay, 0, 15); 

		if(EventManager.OnUpdateUI != null)
		{
			EventManager.OnUpdateUI(); 
		}
	}


	public void ModifyForceDuration(float addition)
	{
		forceDuration+=addition; 

		forceDuration = Mathf.Clamp(forceDuration, 1, 10); 

		if(EventManager.OnUpdateUI != null)
		{
			EventManager.OnUpdateUI(); 
		}
	}

	public void ModifyInitialVelocity(float addition)
	{
		initialVelocity+=addition; 

		initialVelocity = Mathf.Clamp(initialVelocity, 0, 5); 

		if(EventManager.OnUpdateUI != null)
		{
			EventManager.OnUpdateUI(); 
		}
	}

	public void ResetValues()
	{
		force = 0; 

		forceDuration = 1f; 

		initialVelocity = 0; 

		forceDelay = 0; 


		if(EventManager.OnUpdateUI != null)
		{
			EventManager.OnUpdateUI(); 
		}
	}

	public void Clear()
	{
		appliedForce = 0;

		multiplier = 0;  
	}

}

public class SideGizmo : MonoBehaviour 
{

	CanvasBackground cb; 

	ShipMovement ship; 

	Sidepanel sidePanel; 

	public Color activeForceColor; 

	public Image[] forceImages; 

	public Text title; 

	void OnEnable()
	{
		EventManager.OnSimulationStatus+=OnSimulationStatus; 
	}

	void OnDisable()
	{
		EventManager.OnSimulationStatus-=OnSimulationStatus; 
	}

	void Start()
	{
		cb = FindObjectOfType<CanvasBackground>(); 
		ship = FindObjectOfType<ShipMovement>(); 
		sidePanel = FindObjectOfType<Sidepanel>(); 
	}

	void Update()
	{
		if(!MainController.SIMULATION_STATUS) return; 
		UpdateForceImages(); 		
	}

	private void UpdateForceImages()
	{
		forceImages[0].enabled = ship.leftForce.multiplier == 1; 
		forceImages[1].enabled = ship.downwardForce.multiplier == 1; 
		forceImages[2].enabled = ship.forwardForce.multiplier == 1; 
		forceImages[3].enabled = ship.upwardForce.multiplier == 1; 
		forceImages[4].enabled = ship.backwardForce.multiplier == 1; 
		forceImages[5].enabled = ship.rightForce.multiplier == 1; 
		
	}

	public void ForwardPanel()
	{
		cb.TogglePanel(1); 
		MainController.SELECTED_FORCE_SIDE = ship.GetSelectedForceSide("ForwardPanel");
		sidePanel.UpdatePanel(MainController.SELECTED_FORCE_SIDE); 
	}

	public void BackwardPanel()
	{
		cb.TogglePanel(1); 
		MainController.SELECTED_FORCE_SIDE = ship.GetSelectedForceSide("BackwardPanel");
		sidePanel.UpdatePanel(MainController.SELECTED_FORCE_SIDE); 
	}

	public void LeftPanel()
	{
		cb.TogglePanel(1); 
		MainController.SELECTED_FORCE_SIDE = ship.GetSelectedForceSide("LeftPanel");
		sidePanel.UpdatePanel(MainController.SELECTED_FORCE_SIDE); 
	}

	public void RightPanel()
	{
		cb.TogglePanel(1); 
		MainController.SELECTED_FORCE_SIDE = ship.GetSelectedForceSide("RightPanel");
		sidePanel.UpdatePanel(MainController.SELECTED_FORCE_SIDE); 
	}

	public void UpPanel()
	{
		cb.TogglePanel(1); 
		MainController.SELECTED_FORCE_SIDE = ship.GetSelectedForceSide("UpPanel");
		sidePanel.UpdatePanel(MainController.SELECTED_FORCE_SIDE); 
	}

	public void DownPanel()
	{
		cb.TogglePanel(1); 
		MainController.SELECTED_FORCE_SIDE = ship.GetSelectedForceSide("DownPanel");
		sidePanel.UpdatePanel(MainController.SELECTED_FORCE_SIDE); 
	}


	private void OnSimulationStatus(bool b)
	{
		if(!b)
		{
			title.text = "Select a side"; 
			
			for(int i = 0;i < forceImages.Length; i++)
			{
				forceImages[i].enabled = true; 
				forceImages[i].color = forceImages[i].GetComponent<ForceImageSprite>().defaultColor; 
			}			
		}
		else
		{
			title.text = "Active Forces"; 
			
			for(int i = 0; i < forceImages.Length; i++)
			{
				forceImages[i].color = activeForceColor; 
			}
		}
	}	
}
