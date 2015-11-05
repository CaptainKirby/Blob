using UnityEngine;
using System.Collections;
using Rewired;

public class MovementStateController : MonoBehaviour {

	[HideInInspector]public Vector3 inputDir;
	[HideInInspector]public bool stateChange;
	[HideInInspector] public bool jump;
	Player player;

	[Header("Standard State")]
	public float standardAccel = 5;
	public float standardDrag = 2;
	public float standardMovementMax = 10;
	public float standardGravity = 0.2f;
	public float standardJumpVelocity = 12f;

	[Header("Floatig State")]
	public float floatingAccel = 5;
	public float floatingDrag = 2;
	public float floatingMovementMax = 10;
	public float floatingGravity = 0.1f;

	[Header("Rock State")]
	public float rockGravity = 1;

	[Space(10)]
	public float raycastDist = 1;


	[HideInInspector] public IPlayerState currentState;
	[HideInInspector] public StandardState standardState;
	[HideInInspector] public FloatingState floatingState;
	[HideInInspector] public RockState rockState;

	public void Awake()
	{
		standardState = new StandardState(this, GetComponent<Rigidbody>());
		floatingState = new FloatingState(this, GetComponent<Rigidbody>());
		rockState = new RockState(this, GetComponent<Rigidbody>());
	}

	public void Start()
	{
		currentState = standardState;
		player = ReInput.players.GetPlayer(0);
	}

	void Update()
	{
		inputDir.x = player.GetAxis("Move Horizontal");
		inputDir.y = player.GetAxis("Move Vertical");
		stateChange = player.GetButtonDown("Change State");
		jump = player.GetButtonDown("Jump");

		currentState.UpdateState(inputDir); 
	}

	void FixedUpdate()
	{
		currentState.FixedUpdateState(inputDir);
	}

	void LateUpdate()
	{
		currentState.LateUpdateState();
	}
	void OnTriggerEnter(Collider col)
	{
		currentState.OnTriggerEnter(col);
	}

	public bool GroundedCheck()
	{
//		return Physics.Raycast(transform.position, -Vector3.up,raycastDist);
		RaycastHit hit2;
		return Physics.SphereCast(transform.position, 0.4f, -transform.up, out hit2, 0.15f);

	}

	public bool JumpCheck()
	{
//		return Physics.Raycast(transform.position, -Vector3.up,raycastDist + 0.2f);
		RaycastHit hit;
		return Physics.SphereCast(transform.position, 0.4f, -transform.up, out hit, 0.3f);
	}
	public bool SideCheckRight()
	{
//		RaycastHit hit;
		Vector3 p1 = new Vector3(transform.position.x, transform.position.y - GetComponent<CapsuleCollider>().height/4, transform.position.z);
		Vector3 p2 = new Vector3(transform.position.x, transform.position.y + GetComponent<CapsuleCollider>().height/4, transform.position.z);

		return  Physics.CapsuleCast(p1, p2, 0, transform.right,  0.55f);
	}
	public bool SideCheckLeft()
	{
		//		RaycastHit hit;
		Vector3 p1 = new Vector3(transform.position.x, transform.position.y - GetComponent<CapsuleCollider>().height/4, transform.position.z);
		Vector3 p2 = new Vector3(transform.position.x, transform.position.y + GetComponent<CapsuleCollider>().height/4, transform.position.z);
		
		return  Physics.CapsuleCast(p1, p2, 0, -transform.right,  0.55f);
	}


}
