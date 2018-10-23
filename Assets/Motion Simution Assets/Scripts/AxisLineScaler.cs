using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class AxisLineScaler : MonoBehaviour {


	public void ScaleXAxis(float max, out float xMax)
	{

		max = Mathf.Clamp(max, 5, max); 

		float increment = max / 5f; 

		increment = Mathf.Ceil(increment); 

	//	increment = Mathf.Round(increment / 5f) * 5f; 

		//increment = Mathf.Clamp(increment, 1, increment); 

		xMax = 0; 

		for(int i = 0;  i  < transform.childCount; i++)
		{
			Text t = transform.GetChild(i).GetComponent<Text>(); 

			float offset = (i + 1) * increment; 

			if(i == transform.childCount - 1)
			{
				xMax = offset; 
			}

			t.text = offset.ToString(); 
		}

	}

	public void ScaleYAxis(float max, out float yMax)
	{
		max = Mathf.Clamp(max, 5, max); 

		float increment = max / 5f; 

		increment = Mathf.Ceil(increment); 

		//increment = Mathf.Round(increment / 5f) * 5f; 

		//increment = Mathf.Clamp(increment, 1, increment); 

		yMax = 0; 

		float index = (-transform.childCount / 2f) * increment; 

		for(int i = 0; i < transform.childCount; i++)
		{
			Text t = transform.GetChild(i).GetComponent<Text>(); 

			if(index >= 0)
			{
				index+=(increment); 
				t.text = index.ToString(); 
				if(i == transform.childCount - 1)
				{
					yMax = index; 
				}
				continue; 
			}


			if(i == transform.childCount - 1)
			{
				yMax = index; 
			}

			t.text = index.ToString(); 

			index+=increment; 
		}
	}
}
