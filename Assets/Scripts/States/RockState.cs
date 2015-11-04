﻿using UnityEngine;
using System.Collections;

public class RockState : IPlayerState {

	private readonly MovementStateController player;

	private Rigidbody rigidbody;
	public RockState(MovementStateController stateController, Rigidbody rigidComp)
	{
		player = stateController;
		rigidbody = rigidComp;
	}

	public void UpdateState(Vector3 inputDir)
	{
		if(player.stateChange)
		{
			ToStandardState();
		}
	}
	 
	public void FixedUpdateState(Vector3 inputDir)
	{
		if(!player.GroundedCheck())
		{
			rigidbody.velocity -= new Vector3(0, 0.8f ,0);
			//			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y,
		}
	}

	public void ToStandardState()
	{
		player.currentState = player.standardState;
		Debug.Log ("To standard state");
	}

	public void ToFloatingState()
	{

	}

	public void ToRockState()
	{

	}

	public void OnTriggerEnter(Collider col)
	{
		
	}


}