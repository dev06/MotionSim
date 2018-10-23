using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceObjects : MonoBehaviour {

	public GameObject prefab; 

	private List<Vector3> chosen; 

	void Start () 
	{
		for(int x = 0;x < 5; x++)
		{
			for(int y = 0; y < 5; y++)
			{
				for(int z = 0; z < 5; z++)
				{
					GameObject clone = Instantiate(prefab) as GameObject; 

					clone.transform.SetParent(transform); 

				// float xx = (float)(x * 6); 
				// float zz = (float)(z * 6); 

					float xx = Random.Range(-.5f, .5f);

					float yy = Random.Range(-.5f, .5f); 

					float zz = Random.Range(-.5f, .5f); 

				//clone.transform.localPosition = new Vector3((x - 1) * .5f, 0, (z - 1) * .5f); 

					clone.transform.localPosition = new Vector3(xx, yy, zz); 


				}

			}

		}	
	}


	// Vector3 GetPosition()
	// {
	// 	float xx = Random.Range(-.5f, .5f);

	// 	float zz = Random.Range(-.5f, .5f); 

	// 	Vector3 ret = Vector3.zero; 

	// 	if(chosen.Count == 0)
	// 	{
	// 		ret = new Vector3(xx, 0, zz); 

	// 		return ret; 
	// 	}

	// }
}
