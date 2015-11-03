using UnityEngine;
using System.Collections;

public class StandardState : IPlayerState {


	private readonly MovementStateController player;

	private Rigidbody rigidbody;

	public StandardState(MovementStateController stateController, Rigidbody rigidComp)
    {
		player = stateController;
		rigidbody = rigidComp;
	}


	public void UpdateState(Vector3 inputDir)
	{


		if(player.stateChange)
		{
			ToFloatingState();
		}
	}

	public void FixedUpdateState(Vector3 inputDir) 
	{
		if(!player.GroundedCheck())
		{
			rigidbody.velocity -= new Vector3(0, 0.2f ,0);
//			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y,
		}
    }

	public void ToStandardState()
	{

	}

	public void ToFloatingState()
	{
		player.currentState = player.floatingState;
		Debug.Log ("To float state");
	}

	public void ToRockState()
	{

	}

	public void OnTriggerEnter(Collider col)
	{
        
    }


}
