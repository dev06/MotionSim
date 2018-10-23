using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {


	public static MainController Instance; 

	public DataRecorder dataRecorder; 

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this; 
		}
	}

	void Start()
	{
		dataRecorder = GetComponent<DataRecorder>(); 
	}

	public static bool SIMULATION_STATUS; 


	public static ForceSide SELECTED_FORCE_SIDE; 


	public static float SIMULATION_TIME = 0; 


	public void ToggleSimulation()
	{
		SIMULATION_STATUS = !SIMULATION_STATUS; 

		if(EventManager.OnSimulationStatus != null)
		{
			EventManager.OnSimulationStatus(SIMULATION_STATUS); 
		}

		if(SIMULATION_STATUS)
		{
			SIMULATION_TIME = 0; 
		}
	}

	void Update()
	{
		if(!SIMULATION_STATUS) return; 

		SIMULATION_TIME+=Time.deltaTime; 
	}
}
