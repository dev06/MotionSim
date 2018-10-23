using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceCube : MonoBehaviour {

	private Vector3 position; 

	private Rigidbody rb; 

	private bool c; 

	private MeshRenderer mesh; 
	void Start () 
	{
		position = transform.localPosition; 	

		rb = GetComponent<Rigidbody>(); 

		mesh = GetComponent<MeshRenderer>(); 
	}

	
	void Update () 
	{
		if(MainController.SIMULATION_STATUS == false) return; 

		if(!IsInside() && !c)
		{
			transform.position = transform.position + (-rb.velocity.normalized); 
			c = true; 
		}

		mesh.enabled = IsInside(); 

		if(IsInside())
		{
			c = false; 
		}
	}

	void OnEnable()
	{
		EventManager.OnSimulationStatus+= OnSimulationStatus; 
	}

	void OnDisable()
	{
		EventManager.OnSimulationStatus-= OnSimulationStatus; 
	}

	bool IsInside()
	{
		float offset = .52f; 

		if((transform.localPosition.y < offset && transform.localPosition.y > -offset)
		&&( transform.localPosition.x < offset && transform.localPosition.x > -offset)
		&&( transform.localPosition.z < offset && transform.localPosition.z > -offset))
		{
			return true; 
		}


		return false; 
	}

	void OnSimulationStatus(bool b)
	{
		if(!b)
		{
			transform.localPosition = position; 

			c = false; 

			mesh.enabled = true; 

			rb.Sleep(); 

			rb.velocity = Vector3.zero; 
		}
	}
}
