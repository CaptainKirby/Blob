using UnityEngine;
using System.Collections;

public class FloatingState : IPlayerState {

	private readonly MovementStateController player;

	private Rigidbody rigidbody;
	private Vector3 speedV;
    private float startHeight;
	public FloatingState(MovementStateController stateController, Rigidbody rigidComp)
	{
		player = stateController;
		rigidbody = rigidComp;
	}

	public void StartState(Vector3 vel)
	{
        //		rigidbody.velocity = rigidbody.velocity/2;
        //		StartCoroutine(
        //		player.StartCoroutine(EntryVelocity(vel));
        //		Debug.Log (rigidbody.velocity);
        speedV = rigidbody.velocity;
        startHeight = player.transform.position.y;
        Debug.Log(startHeight);
	}

	public void UpdateState(Vector3 inputDir)
	{
		if(player.stateChange)
		{
            //ToRockState();
            ChangeState(player.rockState);
		}
	}

	public void FixedUpdateState(Vector3 inputDir)
	{
		speedV = new Vector3(speedV.x + player.floatingAccel  * player.inputDir.x * Time.deltaTime, speedV.y + player.floatingAccel  * player.inputDir.y * Time.deltaTime, 0);
		speedV = speedV - speedV * Mathf.Clamp01(player.floatingDrag * Time.deltaTime);
		speedV = new Vector3(Mathf.Clamp(speedV.x, -player.floatingMovementMax, player.floatingMovementMax),Mathf.Clamp(speedV.y, -player.floatingMovementMax, player.floatingMovementMax), 0);
//		Debug.Log (speedV);

//		if(player > 0.1f)
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

        //if(player.transform.position.y > startHeight + player.floatExtraHeight)
        //{
        //    rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y * 0.9f, 0);

        //}

//		rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -player.floatingMovementMax, player.floatingMovementMax), Mathf.Clamp(rigidbody.velocity.y, -player.floatingMovementMax, player.floatingMovementMax), 0);

//		if(rigidbody.velocity.magnitude < 0.3f)
//		{
//		rigidbody.velocity = new Vector3(rigidbody.velocity.x * 0.98f, rigidbody.velocity.y, 0);
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

//	private IEnumerator EntryVelocity(Vector3 vel)
//	{
//		bool onOff = true;
//		float mTime = 0;
//		Vector3 velTemp = vel;
//		while(onOff)
//		{
//			if(mTime < 1)
//			{
//				velTemp  = velTemp*0.99f;
//				rigidbody.velocity = new Vector3(rigidbody.velocity.x + velTemp.x, rigidbody.velocity.y+ velTemp.y, 0);
//			}
//			else
//			{
//				onOff = false;
//				yield break;
//			}
//			yield return null;
//		}
//
//	}


}
