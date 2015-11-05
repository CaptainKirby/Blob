﻿using UnityEngine;
using System.Collections;

public class FloatingState : IPlayerState {

	private readonly MovementStateController player;

	private Rigidbody rigidbody;
	private Vector3 speedV;

	public FloatingState(MovementStateController stateController, Rigidbody rigidComp)
	{
		player = stateController;
		rigidbody = rigidComp;
	}

	public void StartState()
	{
//		rigidbody.velocity = rigidbody.velocity/2;
//		Debug.Log (rigidbody.velocity);
	}

	public void UpdateState(Vector3 inputDir)
	{
		if(player.stateChange)
		{
			ToRockState();
		}
	}

	public void FixedUpdateState(Vector3 inputDir)
	{
		speedV = new Vector3(speedV.x + player.floatingAccel  * player.inputDir.x * Time.deltaTime, speedV.y + player.floatingAccel  * player.inputDir.y * Time.deltaTime, 0);
		speedV = speedV - speedV * Mathf.Clamp01(player.floatingDrag * Time.deltaTime);
		speedV = new Vector3(Mathf.Clamp(speedV.x, -player.floatingMovementMax, player.floatingMovementMax),Mathf.Clamp(speedV.y, -player.floatingMovementMax, player.floatingMovementMax), 0);
//		Debug.Log (speedV);

//		if(speedV.magnitude > 0.1f)
//		{
			rigidbody.velocity = new Vector3(speedV.x, speedV.y , 0); // keep starting velocity HOW
//		}
//		else
//		{
//			rigidbody.velocity = rigidbody.velocity * 0.98f; 
//		}
		if(!player.GroundedCheck())
		{
			//			rigidbody.velocity = new Vector3(0, Mathf.Clamp(rigidbody.velocity.y, -0.5f, 0.5f) ,0);
			rigidbody.velocity -= new Vector3(0, player.floatingGravity ,0);
			//			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y,
		}

//		rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -player.floatingMovementMax, player.floatingMovementMax), Mathf.Clamp(rigidbody.velocity.y, -player.floatingMovementMax, player.floatingMovementMax), 0);

//		if(rigidbody.velocity.magnitude < 0.3f)
//		{
//		rigidbody.velocity = new Vector3(rigidbody.velocity.x * 0.98f, rigidbody.velocity.y, 0);
//		}
    }
	public void LateUpdateState()
	{
		
	}
	public void ToStandardState()
	{
		Debug.Log ("Wrong State Change");
	}

	public void ToFloatingState()
	{
		Debug.Log ("Wrong State Change");
	}

	public void ToRockState()
	{
		player.currentState = player.rockState;		
		player.currentState.StartState();
		Debug.Log ("To rock state");
	}

	public void OnTriggerEnter(Collider col)
	{

	}


}
