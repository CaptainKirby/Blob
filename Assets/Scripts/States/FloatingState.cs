using UnityEngine;
using System.Collections;

public class FloatingState : IPlayerState {

	private readonly MovementStateController player;

	private Rigidbody rigidbody;
	public FloatingState(MovementStateController stateController, Rigidbody rigidComp)
	{
		player = stateController;
		rigidbody = rigidComp;
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
		if(!player.GroundedCheck())
		{
			rigidbody.velocity = new Vector3(0, Mathf.Clamp(rigidbody.velocity.y, -0.5f, 0.5f) ,0);
			rigidbody.velocity -= new Vector3(0, 0.01f ,0);
			//			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y,
		}
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
		Debug.Log ("To rock state");
	}

	public void OnTriggerEnter(Collider col)
	{

	}


}
