using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.UI.Extensions; 
public class GraphCreator : MonoBehaviour {

	public GameObject pointPrefab, pointContainer; 

	public AxisLineScaler xAxisScaler, yAxisScaler; 

	public UILineRenderer lineRenderer; 

	public Color xAxisColor, yAxisColor, zAxisColor; 

	private InformationPanel panel; 

	void OnEnable()
	{
		EventManager.OnButtonClick+=OnButtonClick; 
	}

	void OnDisable()
	{
		EventManager.OnButtonClick-=OnButtonClick; 	
	}

	void Start()
	{
		panel = FindObjectOfType<InformationPanel>(); 
	}

	void OnButtonClick(ButtonID id)
	{
		if(id == ButtonID.CreateGraph)
		{
			CreateGraph(); 
		}
	}

	public void CreateGraph()
	{

		lineRenderer.Points = new Vector2[0]; 
		
		List<Point> points = panel.GetGraphPoints(); 

		if(points == null) return; 

		if(points.Count == 0)
		{
			return; 
		}

		float xMax = 0; 

		float yMax = 0; 

		xAxisScaler.ScaleXAxis(points[points.Count-1].x, out xMax); 

		yAxisScaler.ScaleYAxis(GetYAxisMax(points), out yMax); 

		Vector2[] array = new Vector2[points.Count]; 


		lineRenderer.color = GetLineColor(panel.selectedAxis); 

		for(int i = 0;i < points.Count; i++)
		{
			Vector2 pointPosition = new Vector2((points[i].x * 160) / xMax, (points[i].y * 100) / yMax); 

			array[i] = new Vector2(pointPosition.x, pointPosition.y); 
		}

		lineRenderer.Points = array; 

	}

	private Color GetLineColor(InformationPanel.SelectedAxis selectedAxis)
	{

		switch(selectedAxis)
		{
			case InformationPanel.SelectedAxis.xAxis: 
			{
				return xAxisColor;
			}

			case InformationPanel.SelectedAxis.yAxis:
			{
				return yAxisColor; 
			}

			case InformationPanel.SelectedAxis.zAxis:
			{
				return zAxisColor;
			}
		}

		return new Color(); 
	}

	private float GetYAxisMax(List<Point> points)
	{
		float max = points[0].y; 

		for(int i = 0;i < points.Count; i++)
		{
			if(Mathf.Abs(points[i].y) > max)
			{
				max = Mathf.Abs(points[i].y); 
			}
		}

		return max; 
	}

}
