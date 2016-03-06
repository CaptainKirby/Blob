using UnityEngine;
using System.Collections;

public class StandardState : IPlayerState {


	private readonly MovementStateController player;

	private Rigidbody rigidbody;

	private bool jump;

	private float speed = 0;
	private Vector3 speedV;
	private float jumpXVel;
    private float jumpSpeed;

    private bool grounded;
    private CharacterController controller;

	public StandardState(MovementStateController stateController, CharacterController charController)
    {
		player = stateController;
        controller = charController;
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
		
        speed = speed + player.standardAccel * player.inputDir.magnitude * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0f, player.standardMovementMax);
        speed = speed - speed * Mathf.Clamp01(player.standardDrag * Time.deltaTime);

        speedV = new Vector3(speedV.x + player.standardAccel * player.inputDir.x * Time.deltaTime, 0, 0); //speedV.z + player.standardAccel * player.inputDir.z * Time.deltaTime
        speedV = speedV - speedV * Mathf.Clamp01(player.standardDrag * Time.deltaTime);
        speedV = new Vector3(Mathf.Clamp(speedV.x, -player.standardMovementMax, player.standardMovementMax), speedV.y, 0);
        if((player.HeelCheckL() || player.HeelCheckR()) && controller.isGrounded)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if (player.jump && grounded)
        {
            JumpInit();
        }

        //if((player.SideCheckLeft() || player.SideCheckRight()) && !player.GroundedCheck())
        //{
        //	speedV = Vector3.zero;
        //}

        //if(controller.isGrounded)
        //{
        //    controller.SimpleMove(new Vector3(speedV.x + jumpXVel, controller.velocity.y, 0));
        //}
        //		if(player.GroundedCheck())
        //		{
        //		if(!player.SideCheckLeft() && !player.SideCheckRight())
        //		{
        //if (!controller.isGrounded)
        //{
            jumpSpeed -= player.standardGravity * Time.deltaTime;
        //}
        if(player.HeelCheckL() && player.HeelCheckR())
        { 
            speedV.y = jumpSpeed;
        }
        else
        {
            speedV.y = 0;
            jumpSpeed = 0;
        }
        if (speedV.y < 0 && grounded)
        {
            jumpSpeed = 0;
            //speedV.y = 0;

        }

        //}
        //else
        //{
        //    speedV.y -= player.standardGravity;
        //}
        //speedV.y  -= player.standardGravity * Time.deltaTime;
        //}
        if (player.inputDir.magnitude > 0.1f)
        {
            speedV = new Vector3(speedV.x + jumpXVel, speedV.y, 0);
        }
        else if (!grounded && player.inputDir.magnitude < 0.2f)
        {
            speedV = new Vector3(speedV.x * 0.99f, speedV.y, 0);
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
        //if(controller.isGrounded)
        //{
        //controller.SimpleMove(new Vector3(0, player.standardJumpVelocity, 0));
        //jumpSpeed = -1;
        jumpSpeed = player.standardJumpVelocity;
        Debug.Log("JUMP");
        //			jumpXVel = player.inputDir.normalized.x *2;
        //rigidbody.velocity += new Vector3(0, player.standardJumpVelocity, 0); //speedV.x*10
        //}
    }

	public void FixedUpdateState(Vector3 inputDir) 
	{
        controller.Move(speedV);
        Debug.Log(grounded + " " + speedV.y + " " + jumpSpeed);
        //Debug.Log(player.HeelCheckL() + " " + player.HeelCheckR());
        //Debug.Log()
        //if(!controller.isGrounded)
        //{
        //rigidbody.velocity -= new Vector3(0, player.standardGravity ,0);
        //controller.SimpleMove(new Vector3(0, -player.standardGravity, 0));
        //			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y,
        //}
        //		else
        //		{
        //			jumpXVel = 0;
        //		}

        //Debug.Log(controller.isGrounded);
        //      speed = speed + player.standardAccel * player.inputDir.magnitude *  Time.deltaTime;
        //speed = Mathf.Clamp(speed, 0f, player.standardMovementMax);
        //speed = speed - speed * Mathf.Clamp01(player.standardDrag * Time.deltaTime);

        //speedV = new Vector3(speedV.x + player.standardAccel  * player.inputDir.x * Time.deltaTime, 0, 0); //speedV.z + player.standardAccel * player.inputDir.z * Time.deltaTime
        //speedV = speedV - speedV * Mathf.Clamp01(player.standardDrag * Time.deltaTime);
        //speedV = new Vector3(Mathf.Clamp(speedV.x, -player.standardMovementMax, player.standardMovementMax),speedV.y, 0);
        //      //if((player.SideCheckLeft() || player.SideCheckRight()) && !player.GroundedCheck())
        //      //{
        //      //	speedV = Vector3.zero;
        //      //}

        //      //if(controller.isGrounded)
        //      //{
        //      //    controller.SimpleMove(new Vector3(speedV.x + jumpXVel, controller.velocity.y, 0));
        //      //}
        //      //		if(player.GroundedCheck())
        //      //		{
        //      //		if(!player.SideCheckLeft() && !player.SideCheckRight())
        //      //		{
        //      //if (!controller.isGrounded)
        //      //{
        //          speedV = new Vector3(speedV.x + jumpXVel, speedV.y - player.standardGravity, 0);
        //      //}
        //      if (player.inputDir.magnitude > 0.1f)
        //      {
        //          speedV = new Vector3(speedV.x + jumpXVel, speedV.y, 0);
        //      }
        //      else if (!controller.isGrounded && player.inputDir.magnitude < 0.2f)
        //      {
        //          speedV = new Vector3(speedV.x * 0.99f, speedV.y, 0);
        //      }
        //else if (controller.isGrounded )
        //{
        //    speedV = new Vector3(speedV.x + jumpXVel, speedV.y, 0);
        //}
        //Debug.Log(speedV);
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
        player.currentState.StartState(controller.velocity);
    }


	public void OnTriggerEnter(Collider col)
	{
        
    }
    public void OnCollisionEnter(Collision col)
    {

    }


}
