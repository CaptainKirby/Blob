using UnityEngine;
using System.Collections;

public class StandardState : IPlayerState {


	private readonly MovementStateController player;

	private Rigidbody rigidbody;

	private bool jump;

	private float speed = 0;
	private Vector3 speedV;
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
		if(player.jump)
		{
			JumpInit ();
		}
	}

	private void JumpInit()
	{
		if(player.JumpCheck())
		{
			rigidbody.velocity += new Vector3(0, 10, 0);
		}
	}


	public void FixedUpdateState(Vector3 inputDir) 
	{
		if(!player.GroundedCheck())
		{
			rigidbody.velocity -= new Vector3(0, 0.2f ,0);
//			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y,
		}

		speed = speed + player.standardAccel * player.inputDir.magnitude *  Time.deltaTime;
		speed = Mathf.Clamp(speed, 0f, player.standardMovementMax);
		speed = speed - speed * Mathf.Clamp01(player.standardDrag * Time.deltaTime);

		speedV = new Vector3(speedV.x + player.standardAccel  * inputDir.x * Time.deltaTime, 0,speedV.z + player.standardAccel * inputDir.z * Time.deltaTime);
		speedV = speedV - speedV * Mathf.Clamp01(player.standardDrag * Time.deltaTime);


		rigidbody.velocity = new Vector3(speedV.x, rigidbody.velocity.y, speedV.z);

    }

	public void ToStandardState()
	{

	}

	public void ToFloatingState()
	{
		player.currentState = player.floatingState;
		jump = false;
		Debug.Log ("To float state");
	}

	public void ToRockState()
	{

	}

	public void OnTriggerEnter(Collider col)
	{
        
    }


}
