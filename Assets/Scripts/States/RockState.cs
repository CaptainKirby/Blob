using UnityEngine;
using System.Collections;
using System;

public class RockState : IPlayerState {

	private readonly MovementStateController player;

	private Rigidbody rigidbody;
	public RockState(MovementStateController stateController, Rigidbody rigidComp)
	{
		player = stateController;
		rigidbody = rigidComp;
	}

	public void StartState(Vector3 vel)
	{
		rigidbody.velocity = Vector3.zero;
	}

	public void UpdateState(Vector3 inputDir)
	{
		if(player.stateChange)
		{
            //ToStandardState();
            ChangeState(player.standardState);
		}
	}
	 
	public void FixedUpdateState(Vector3 inputDir)
	{
		if(!player.GroundedCheck())
		{
			rigidbody.velocity -= new Vector3(0, player.rockGravity ,0);
			//			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y,
		}
	}

	public void LateUpdateState()
	{

	}

    public void ChangeState(IPlayerState state)
    {
        player.currentState = state;
        player.currentState.StartState(rigidbody.velocity);
    }



	public void OnTriggerEnter(Collider col)
	{
		
	}

   
}
