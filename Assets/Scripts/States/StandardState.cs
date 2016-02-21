using UnityEngine;
using System.Collections;

public class StandardState : IPlayerState {


	private readonly MovementStateController player;

	private Rigidbody rigidbody;

	private bool jump;

	private float speed = 0;
	private Vector3 speedV;
	private float jumpXVel;
	
	public StandardState(MovementStateController stateController, Rigidbody rigidComp)
    {
		player = stateController;
		rigidbody = rigidComp;
	}

	public void StartState(Vector3 vel)
	{
		rigidbody.velocity = Vector3.zero;
        speedV = Vector3.zero;
	}

	public void UpdateState(Vector3 inputDir)
	{

		if(player.stateChange)
		{
            //ToFloatingState();
            ChangeState(player.floatingState);
		}
		if(player.jump)
		{
			JumpInit ();
		}
//		else
//		{
//			if(player.GroundedCheck())
//			{
//				jumpXVel = 0;
//			}
//		}
//		Debug.Log(jumpXVel + " " + player.GroundedCheck());
//		Debug.Log(player.SideCheckRight() + " " + player.SideCheckLeft());

	}

	private void JumpInit()
	{
		if(player.JumpCheck())
		{
//			jumpXVel = player.inputDir.normalized.x *2;
			rigidbody.velocity += new Vector3(0, player.standardJumpVelocity, 0); //speedV.x*10
		}
	}

	public void FixedUpdateState(Vector3 inputDir) 
	{
        //Debug.Log()
		if(!player.GroundedCheck())
		{
			rigidbody.velocity -= new Vector3(0, player.standardGravity ,0);
//			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y,
		}
        //		else
        //		{
        //			jumpXVel = 0;
        //		}

        //Debug.Log(player.GroundedCheck());
        speed = speed + player.standardAccel * player.inputDir.magnitude *  Time.deltaTime;
		speed = Mathf.Clamp(speed, 0f, player.standardMovementMax);
		speed = speed - speed * Mathf.Clamp01(player.standardDrag * Time.deltaTime);

		speedV = new Vector3(speedV.x + player.standardAccel  * player.inputDir.x * Time.deltaTime, 0, 0); //speedV.z + player.standardAccel * player.inputDir.z * Time.deltaTime
		speedV = speedV - speedV * Mathf.Clamp01(player.standardDrag * Time.deltaTime);
		speedV = new Vector3(Mathf.Clamp(speedV.x, -player.standardMovementMax, player.standardMovementMax),speedV.y, 0);
		if((player.SideCheckLeft() || player.SideCheckRight()) && !player.GroundedCheck())
		{
			speedV = Vector3.zero;
		}
        
//		if(player.GroundedCheck())
//		{
//		if(!player.SideCheckLeft() && !player.SideCheckRight())
//		{
		if(player.inputDir.magnitude > 0.1f)
		{
			rigidbody.velocity = new Vector3(speedV.x + jumpXVel, rigidbody.velocity.y, 0);
		}
		else if(!player.GroundedCheck() && player.inputDir.magnitude < 0.2f)
		{
			rigidbody.velocity = new Vector3(rigidbody.velocity.x * 0.99f, rigidbody.velocity.y, 0);
		}
		else if(player.GroundedCheck())
		{
			rigidbody.velocity = new Vector3(speedV.x + jumpXVel, rigidbody.velocity.y, 0);
		}
//		}
//		else if(player.GroundedCheck())
//		{
//			rigidbody.velocity = new Vector3(speedV.x + jumpXVel, rigidbody.velocity.y, 0);
//		}
//		}
//		else
//		{
//			rigidbody.velocity = new Vector3(rigidbody.velocity.x * speedV.x/10, rigidbody.velocity.y, 0);
//		}

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
    public void OnCollisionEnter(Collision col)
    {

    }


}
