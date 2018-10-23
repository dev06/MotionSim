using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InformationPanel : MonoBehaviour {

	public enum SelectedGraph
	{
		None, 
		VelocityGraph, 
		PositionGraph, 
		AccelerationGraph, 
	}

	public enum SelectedAxis
	{
		None, 
		xAxis, 
		yAxis, 
		zAxis,
	}

	public Text titleText, unitsText; 

	public Text velocityText, displacementText, accelerationText; 

	public SelectedGraph selectedGraph; 

	public SelectedAxis selectedAxis; 

	public List<Point> activePoints; //points that will be displayed on the graph

	private ShipMovement ship; 

	private GraphCreator graphCreator; 

	void Start()
	{
		TogglePanel(0); 

		ship = FindObjectOfType<ShipMovement>(); 

		graphCreator = FindObjectOfType<GraphCreator>(); 
	}

	void OnEnable () 
	{
		EventManager.OnButtonClick+=OnButtonClick; 
	}
	
	void OnDisable () 
	{
		EventManager.OnButtonClick-=OnButtonClick; 
	}

	void OnButtonClick(ButtonID id)
	{
		
		switch(id)
		{
			case ButtonID.VelocityGraph:
			{
				TogglePanel(1); 
				selectedGraph = SelectedGraph.VelocityGraph; 
				titleText.text = "Velocity"; 
				unitsText.text = "(m/s)";
				graphCreator.CreateGraph(); 
				break; 
			}
			case ButtonID.AccelerationGraph:
			{
				TogglePanel(1); 
				selectedGraph = SelectedGraph.AccelerationGraph; 
				titleText.text = "Acceleration"; 
				unitsText.text = "(m/s^2)";
				graphCreator.CreateGraph(); 
				break; 
			}
			case ButtonID.PositionGraph:
			{
				TogglePanel(1); 
				selectedGraph = SelectedGraph.PositionGraph; 
				titleText.text = "Displacement"; 
				unitsText.text = "(m)";
				graphCreator.CreateGraph(); 
				break; 
			}

			case ButtonID.InfoBack:
			{
				TogglePanel(0); 
				break; 
			}
		}
	}

	void Update()
	{
		velocityText.text = "V:" + ship.shipVelocity.ToString(); 
		accelerationText.text = "A:" + ship.shipAcceleration.ToString(); 
		displacementText.text = "D:" + ship.shipDisplacement.ToString(); 
		
	}

	public void xAxisSelected()
	{
		selectedAxis = SelectedAxis.xAxis; 

		GetGraphPoints(); 

		graphCreator.CreateGraph(); 

	}

	public void yAxisSelected()
	{
		selectedAxis = SelectedAxis.yAxis; 

		GetGraphPoints(); 

		graphCreator.CreateGraph(); 

	}

	public void zAxisSelected()
	{
		selectedAxis = SelectedAxis.zAxis; 

		GetGraphPoints(); 

		graphCreator.CreateGraph(); 


	}

	public List<Point> GetGraphPoints()
	{

		switch(selectedGraph)
		{
			case SelectedGraph.VelocityGraph:
			{
				switch(selectedAxis)
				{
					case SelectedAxis.xAxis:
					{
						activePoints = MainController.Instance.dataRecorder.velocity_xAxis; 
						break; 
					}
					case SelectedAxis.yAxis:
					{
						activePoints = MainController.Instance.dataRecorder.velocity_yAxis; 
						break; 	
					}
					case SelectedAxis.zAxis:
					{
						activePoints = MainController.Instance.dataRecorder.velocity_zAxis; 
						break; 	
					}
				}

				break; 
			}

			case SelectedGraph.AccelerationGraph:
			{
				switch(selectedAxis)
				{
					case SelectedAxis.xAxis:
					{
						activePoints = MainController.Instance.dataRecorder.acc_xAxis; 
						break; 
					}
					case SelectedAxis.yAxis:
					{
						activePoints = MainController.Instance.dataRecorder.acc_yAxis; 
						break; 	
					}
					case SelectedAxis.zAxis:
					{
						activePoints = MainController.Instance.dataRecorder.acc_zAxis; 
						break; 	
					}
				}

				break; 
			}

			case SelectedGraph.PositionGraph:
			{
				switch(selectedAxis)
				{
					case SelectedAxis.xAxis:
					{
						activePoints = MainController.Instance.dataRecorder.dis_xAxis; 
						break; 
					}
					case SelectedAxis.yAxis:
					{
						activePoints = MainController.Instance.dataRecorder.dis_yAxis; 
						break; 	
					}
					case SelectedAxis.zAxis:
					{
						activePoints = MainController.Instance.dataRecorder.dis_zAxis; 
						break; 	
					}
				}

				break; 
			}

		}

		return activePoints; 
	}

	void TogglePanel(int index)
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
