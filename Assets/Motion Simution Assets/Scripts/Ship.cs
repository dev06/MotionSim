using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	public GameObject left, right, up, down, forward, back; 

	ShipMovement shipMovement; 

	void Start () 
	{
		shipMovement = FindObjectOfType<ShipMovement>();
	}
	
	public void ToggleForward(bool b)
	{
		for(int i = 0;i < forward.transform.childCount; i++)
		{
			ParticleSystem p = forward.transform.GetChild(i).GetComponent<ParticleSystem>();
			if(b)
			{
				p.Play(); 
			}
			else
			{
				p.Stop(); 
			}
		}
	}

	public void ToggleBackward(bool b)
	{
		for(int i = 0;i < back.transform.childCount; i++)
		{
			ParticleSystem p = back.transform.GetChild(i).GetComponent<ParticleSystem>();

			if(b)
			{
				p.Play(); 
			}
			else
			{
				p.Stop(); 
			}
		}
	}

	public void ToggleUpward(bool b)
	{
		for(int i = 0;i < up.transform.childCount; i++)
		{
			ParticleSystem p = up.transform.GetChild(i).GetComponent<ParticleSystem>();

			if(b)
			{
				p.Play(); 
			}
			else
			{
				p.Stop(); 
			}
		}
	}

	public void ToggleDownward(bool b)
	{
		for(int i = 0;i < down.transform.childCount; i++)
		{
			ParticleSystem p = down.transform.GetChild(i).GetComponent<ParticleSystem>();

			if(b)
			{
				p.Play(); 
			}
			else
			{
				p.Stop(); 
			}
		}
	}

	public void ToggleLeftward(bool b)
	{
		for(int i = 0;i < left.transform.childCount; i++)
		{
			ParticleSystem p = left.transform.GetChild(i).GetComponent<ParticleSystem>();

			if(b)
			{
				p.Play(); 
			}
			else
			{
				p.Stop(); 
			}
		}
	}

	public void ToggleRightward(bool b)
	{
		for(int i = 0;i < right.transform.childCount; i++)
		{
			ParticleSystem p = right.transform.GetChild(i).GetComponent<ParticleSystem>();

			if(b)
			{
				p.Play(); 
			}
			else
			{
				p.Stop(); 
			}
		}
	}
}
