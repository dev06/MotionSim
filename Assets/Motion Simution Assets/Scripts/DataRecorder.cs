using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorder : MonoBehaviour {

	public List<Point> velocity = new List<Point>(); 

	public List<Point> velocity_zAxis = new List<Point>(); 
	public List<Point> velocity_xAxis = new List<Point>(); 
	public List<Point> velocity_yAxis = new List<Point>(); 


	public List<Point> acc_zAxis = new List<Point>(); 
	public List<Point> acc_xAxis = new List<Point>(); 
	public List<Point> acc_yAxis = new List<Point>(); 

	public List<Point> dis_zAxis = new List<Point>(); 
	public List<Point> dis_xAxis = new List<Point>(); 
	public List<Point> dis_yAxis = new List<Point>(); 




	void OnEnable()
	{
		EventManager.OnSimulationStatus+=OnSimulationStatus; 
	}


	void OnDisable()
	{
		EventManager.OnSimulationStatus-=OnSimulationStatus; 
	}

	void OnSimulationStatus(bool b)
	{
		if(b)
		{
			ClearAllData(); 
		}
	}
	void Start () 
	{
		// int index = -25; 
		
		// for(int i = 0;i <= 50; i++)
		// {

		// 	index++; 
		// }		
		// velocity.Add(new Point(1, .25f)); 
	}
	
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			if(EventManager.OnButtonClick != null)
			{
				EventManager.OnButtonClick(ButtonID.CreateGraph); 
			}				
		}		
	}

	public void RecordVelocity(Vector3 v)
	{
		velocity_zAxis.Add(new Point(MainController.SIMULATION_TIME, v.z)); 
		velocity_xAxis.Add(new Point(MainController.SIMULATION_TIME, v.x)); 
		velocity_yAxis.Add(new Point(MainController.SIMULATION_TIME, v.y)); 
	}

	public void RecordAcceleration(Vector3 v)
	{
		acc_zAxis.Add(new Point(MainController.SIMULATION_TIME, v.z)); 
		acc_xAxis.Add(new Point(MainController.SIMULATION_TIME, v.x)); 
		acc_yAxis.Add(new Point(MainController.SIMULATION_TIME, v.y)); 
	}


	public void RecordDisplacement(Vector3 v)
	{
		dis_zAxis.Add(new Point(MainController.SIMULATION_TIME, v.z)); 
		dis_xAxis.Add(new Point(MainController.SIMULATION_TIME, v.x)); 
		dis_yAxis.Add(new Point(MainController.SIMULATION_TIME, v.y)); 
	}

	public void ClearAllData()
	{
		velocity_zAxis.Clear(); 
		velocity_xAxis.Clear(); 
		velocity_yAxis.Clear(); 


		acc_zAxis.Clear(); 
		acc_xAxis.Clear(); 
		acc_yAxis.Clear(); 


		dis_zAxis.Clear(); 
		dis_xAxis.Clear(); 
		dis_yAxis.Clear(); 
	}
}

public class Point
{
	public float x; 
	public float y;
	public Vector2 position;  

	public Point(float x, float y)
	{
		this.x = x; 
		this.y = y; 
		position = new Vector2(x, y); 
	}
}
