using UnityEngine;
using System.Collections;
using Rewired;

public class MovementStateController : MonoBehaviour {

	[HideInInspector]public Vector3 inputDir;
	[HideInInspector]public bool stateChange;
	Player player;

//	public maxGravity
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
		inputDir.z = player.GetAxis("Move Vertical");
		stateChange = player.GetButtonDown("Change State");

		currentState.UpdateState(inputDir); 
	}

	void FixedUpdate()
	{
		currentState.FixedUpdateState(inputDir);
	}

	void OnTriggerEnter(Collider col)
	{
		currentState.OnTriggerEnter(col);
	}

	public bool GroundedCheck()
	{
		return Physics.Raycast(transform.position, -Vector3.up,raycastDist);
	}
}
