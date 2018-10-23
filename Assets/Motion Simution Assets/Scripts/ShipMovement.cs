using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class ShipMovement : MonoBehaviour {

	public Transform referenceObjects; 

	public LineRenderer zLine, yLine, xLine; 
	public LineRenderer velocityLine; 

	private Vector3 defaultPos; 
	
	private Quaternion defaultRot; 

	private Rigidbody rb; 

	[HideInInspector]
	public Vector3 forward, backward, up, down, left, right; 

	public ForceSide forwardForce, backwardForce, upwardForce, downwardForce, leftForce, rightForce; 

	private bool startCoroutines; 

	public Vector3 shipAcceleration; 

	public Vector3 shipDisplacement; 

	public Vector3 shipVelocity; 

	private Ship ship; 

	void OnEnable()
	{
		EventManager.OnSimulationStatus+=OnSimulationStatus; 
	}

	void OnDisable()
	{
		EventManager.OnSimulationStatus-=OnSimulationStatus; 
	}

	void Start () 
	{
		rb = GetComponent<Rigidbody>(); 

		defaultPos = transform.localPosition; 

		defaultRot = transform.localRotation; 

		forwardForce = new ForceSide("Forward"); 

		backwardForce = new ForceSide("Backward"); 

		upwardForce = new ForceSide("Upward"); 

		downwardForce = new ForceSide("Downward"); 

		leftForce = new ForceSide("Left"); 

		rightForce = new ForceSide("Right"); 

		ship = FindObjectOfType<Ship>(); 

		ToggleLines(); 
		
	}

	float recordingTimer = 0; 


	private bool initRecord; 

	void Update () 
	{

		if(MainController.SIMULATION_STATUS == false) 
		{		

			return; 
		}

		if(!startCoroutines)
		{
			StartCoroutine("IForward"); 

			StartCoroutine("IBackward"); 

			StartCoroutine("IUpward"); 

			StartCoroutine("IDownward"); 

			StartCoroutine("ILeftward"); 

			StartCoroutine("IRightward"); 

			startCoroutines = true; 
		}



		forward = (transform.forward * forwardForce.appliedForce) + (transform.forward * forwardForce.initialVelocity); 

		backward = (-transform.forward * backwardForce.appliedForce) + (-transform.forward * backwardForce.initialVelocity); 

		up = (transform.up * upwardForce.appliedForce) + (transform.up * upwardForce.initialVelocity);

		down = (-transform.up * downwardForce.appliedForce) + (-transform.up * downwardForce.initialVelocity);

		left = (-transform.right * leftForce.appliedForce) + (-transform.right * leftForce.initialVelocity);

		right = (transform.right * rightForce.appliedForce) + (transform.right * rightForce.initialVelocity);


		Vector3 movement =  -(forward + backward + left + right + up+ down) * Time.fixedDeltaTime; 

		Vector3 raw = (forward + backward + left + right + up+ down); 


		raw = transform.InverseTransformDirection(raw); 

		this.shipVelocity = raw; 

		Vector3 a = new Vector3(rightForce.acceleration - leftForce.acceleration, upwardForce.acceleration - downwardForce.acceleration, forwardForce.acceleration - backwardForce.acceleration); 

		shipAcceleration = a; 
		
		shipDisplacement+=raw * Time.deltaTime; 

		recordingTimer+=Time.deltaTime; 

		if(!initRecord)
		{

			MainController.Instance.dataRecorder.ClearAllData(); 

			MainController.Instance.dataRecorder.RecordVelocity(raw); 
			MainController.Instance.dataRecorder.RecordAcceleration(shipAcceleration); 
			MainController.Instance.dataRecorder.RecordDisplacement(shipDisplacement); 


			initRecord = true; 
		}

		if(recordingTimer > .1f)
		{
			MainController.Instance.dataRecorder.RecordVelocity(raw); 
			MainController.Instance.dataRecorder.RecordAcceleration(shipAcceleration); 
			MainController.Instance.dataRecorder.RecordDisplacement(shipDisplacement); 
			FindObjectOfType<GraphCreator>().CreateGraph(); 
			recordingTimer = 0; 
		}



		
		for(int i = 0;i < referenceObjects.childCount; i++)
		{
			referenceObjects.GetChild(i).GetComponent<Rigidbody>().velocity = movement; 
		}

		Vector3 d = transform.InverseTransformDirection(-movement);

		Vector3 net = (forward + backward + up + down + left + right); 

		velocityLine.endWidth = .01f; 

		velocityLine.SetPosition(1,  (d.normalized * Vector3.ClampMagnitude(net, 2).magnitude)); 

	}

	IEnumerator IForward()
	{

		forwardForce.acceleration = 0; 

		ship.ToggleBackward(false); 
		
		yield return new WaitForSeconds(forwardForce.forceDelay); 

		float currentForwardForce = 0; 

		float timer = 0; 

		ship.ToggleBackward(forwardForce.force > 0); 

		while(timer < forwardForce.forceDuration)
		{

			currentForwardForce += (Time.deltaTime * forwardForce.force); 

			forwardForce.acceleration = forwardForce.force;

			currentForwardForce = Mathf.Clamp(currentForwardForce, 0, forwardForce.force * forwardForce.forceDuration); 

			forwardForce.appliedForce = currentForwardForce; 

			if(forwardForce.appliedForce > 0)
			{
				forwardForce.multiplier = 1; 
			}

			timer+=Time.deltaTime; 

			yield return null; 
		}

		forwardForce.multiplier = 0; 

		forwardForce.acceleration = 0; 

		ship.ToggleBackward(false); 

		StopCoroutine("IForward"); 
	}

	IEnumerator IBackward()
	{
		backwardForce.acceleration = 0; 
		
		ship.ToggleForward(false);

		yield return new WaitForSeconds(backwardForce.forceDelay); 

		ship.ToggleForward(backwardForce.force > 0);

		float currentBackForce = 0; 

		float timer = 0; 

		while(timer < backwardForce.forceDuration)
		{
			currentBackForce += (Time.deltaTime * backwardForce.force); 

			backwardForce.acceleration = backwardForce.force;

			currentBackForce = Mathf.Clamp(currentBackForce, 0, backwardForce.force * backwardForce.forceDuration); 

			backwardForce.appliedForce = currentBackForce; 

			if(backwardForce.appliedForce > 0)
			{
				backwardForce.multiplier = 1; 
			}

			timer+= Time.deltaTime; 

			yield return null; 
		}

		backwardForce.multiplier = 0; 

		backwardForce.acceleration = 0; 

		ship.ToggleForward(false);

		StopCoroutine("IBackward"); 
	}

	IEnumerator IUpward()
	{
		upwardForce.acceleration = 0; 

		ship.ToggleDownward(false); 
		
		yield return new WaitForSeconds(upwardForce.forceDelay); 

		float currentUpwardForce = 0; 

		float timer = 0; 
		ship.ToggleDownward(upwardForce.force > 0); 


		while(timer < upwardForce.forceDuration)
		{
			currentUpwardForce += (Time.deltaTime * upwardForce.force); 

			upwardForce.acceleration = upwardForce.force; 

			currentUpwardForce = Mathf.Clamp(currentUpwardForce, 0, upwardForce.force * upwardForce.forceDuration); 

			upwardForce.appliedForce = currentUpwardForce; 

			if(upwardForce.appliedForce > 0)
			{
				upwardForce.multiplier = 1; 
			}

			timer+= Time.deltaTime; 

			yield return null; 
		}

		upwardForce.multiplier = 0; 

		upwardForce.acceleration = 0; 

		ship.ToggleDownward(false); 



		StopCoroutine("IUpward"); 
	}

	IEnumerator IDownward()
	{
		downwardForce.acceleration = 0; 

		ship.ToggleUpward(false); 
		
		yield return new WaitForSeconds(downwardForce.forceDelay); 

		float currentDownwardForce = 0; 

		float timer = 0; 

		ship.ToggleUpward(downwardForce.force > 0); 

		while(timer < downwardForce.forceDuration)
		{
			currentDownwardForce += (Time.deltaTime * downwardForce.force); 

			downwardForce.acceleration = downwardForce.force; 

			currentDownwardForce = Mathf.Clamp(currentDownwardForce, 0, downwardForce.force * downwardForce.forceDuration); 

			downwardForce.appliedForce = currentDownwardForce; 

			if(downwardForce.appliedForce > 0)
			{
				downwardForce.multiplier = 1; 
			}

			timer+= Time.deltaTime; 

			yield return null; 
		}

		downwardForce.acceleration = 0; 

		downwardForce.multiplier = 0; 

		ship.ToggleUpward(false); 

		StopCoroutine("IDownward"); 
	}

	IEnumerator ILeftward()
	{
		leftForce.acceleration = 0; 

		ship.ToggleRightward(false);
		
		yield return new WaitForSeconds(leftForce.forceDelay); 

		float currentLeftForce = 0; 

		float timer = 0; 

		ship.ToggleRightward(leftForce.force > 0);

		while(timer < leftForce.forceDuration)
		{
			currentLeftForce += (Time.deltaTime * leftForce.force); 

			leftForce.acceleration = leftForce.force; 

			currentLeftForce = Mathf.Clamp(currentLeftForce, 0, leftForce.force * leftForce.forceDuration); 

			leftForce.appliedForce = currentLeftForce; 

			if(leftForce.appliedForce > 0)
			{
				leftForce.multiplier = 1; 
			}

			timer+= Time.deltaTime; 

			yield return null; 
		}

		leftForce.multiplier = 0; 

		leftForce.acceleration = 0; 

		ship.ToggleRightward(false);

		StopCoroutine("ILeftward"); 
	}

	IEnumerator IRightward()
	{
		rightForce.acceleration = 0; 

		ship.ToggleLeftward(false);
		
		yield return new WaitForSeconds(rightForce.forceDelay); 

		float currentRightForce = 0; 

		float timer = 0; 
		ship.ToggleLeftward(rightForce.force > 0);

		while(timer < rightForce.forceDuration)
		{
			currentRightForce += (Time.deltaTime * rightForce.force); 

			rightForce.acceleration = rightForce.force; 

			currentRightForce = Mathf.Clamp(currentRightForce, 0, rightForce.force * rightForce.forceDuration); 

			rightForce.appliedForce = currentRightForce; 

			if(rightForce.appliedForce > 0)
			{
				rightForce.multiplier = 1; 
			}	

			timer+= Time.deltaTime; 

			yield return null; 
		}

		rightForce.multiplier = 0; 

		rightForce.acceleration = 0; 
		ship.ToggleLeftward(false);

		StopCoroutine("IRightward"); 
	}

	public void Reset()
	{

		rb.Sleep(); 

		rb.velocity = Vector3.zero; 

		transform.localPosition = defaultPos; 

		transform.localRotation = defaultRot; 

		forward = backward = left = right = up = down = Vector3.zero; 

		startCoroutines = false; 

		forwardForce.Clear(); 

		backwardForce.Clear(); 

		upwardForce.Clear(); 

		rightForce.Clear(); 

		leftForce.Clear(); 

		downwardForce.Clear();

		ship.ToggleForward(false);

		ship.ToggleLeftward(false); 
		ship.ToggleRightward(false); 
		ship.ToggleBackward(false); 
		ship.ToggleUpward(false); 
		ship.ToggleDownward(false); 

		shipAcceleration = Vector3.zero; 

		shipDisplacement = Vector3.zero; 

		StopAllCoroutines(); 

		recordingTimer = 0; 

	}


	public ForceSide GetSelectedForceSide(string side)
	{
		switch(side)
		{
			case "ForwardPanel": return forwardForce; 
			case "BackwardPanel": return backwardForce; 
			case "LeftPanel": return leftForce; 
			case "RightPanel": return rightForce; 
			case "UpPanel": return upwardForce; 
			case "DownPanel": return downwardForce; 
		}

		return null; 
	}

	private void ToggleLines()
	{
		xLine.enabled = yLine.enabled = zLine.enabled = !MainController.SIMULATION_STATUS; 

		velocityLine.enabled = MainController.SIMULATION_STATUS; 
	}


	private void OnSimulationStatus(bool b)
	{
		ToggleLines(); 

		if(!b)
		{
			initRecord = false; 
		}
	}
}
