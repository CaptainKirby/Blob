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
    public float floatingGravityFallIncriment = 0.25f;
    public float floatExtraHeight = 10;

	[Header("Rock State")]
	public float rockGravity = 1;

    [Space(10)]
    public GameObject heelLeft;
    public GameObject heelRight;
	public float heelRaycastDist = 1;
    public float raycastDist = 1;


	[HideInInspector] public IPlayerState currentState;
	[HideInInspector] public StandardState standardState;
	[HideInInspector] public FloatingState floatingState;
	[HideInInspector] public RockState rockState;

	public void Awake()
	{
		standardState = new StandardState(this, GetComponent<CharacterController>());
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
        //Debug.Log(GetComponent<Rigidbody>().velocity);
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

    void OnCollisionEnter(Collision col)
    {
        currentState.OnCollisionEnter(col);
    }


    public bool GroundedCheck() // need to fix so it doesnt return false in intersections
    {
        //Debug.DrawRay(transform.position, new Vector3(-Vector3.up.x - 0.2f, -Vector3.up.y, 0) * raycastDist);
        if (Physics.Raycast(transform.position, -Vector3.up, raycastDist))
        {
            return true;
        }
        else if (Physics.Raycast(transform.position, new Vector3(-Vector3.up.x - 0.1f, -Vector3.up.y, 0), raycastDist))
        {
            return true;
        }
        else if (Physics.Raycast(transform.position, new Vector3(-Vector3.up.x + 0.1f, -Vector3.up.y, 0), raycastDist))
        {
            return true;
        }
        else
            return false;
        //return Physics.Raycast(transform.position, -Vector3.up, raycastDist);
        //RaycastHit hit2;
        //return Physics.SphereCast(transform.position, 0.2f, -transform.up, out hit2, transform.localScale.y-0.2f);

    }

    public bool HeelCheckL()
    {
        var down = transform.TransformDirection(Vector3.down);
        bool allowFall = false;
        // cast a ray down from our heel, if it hits anything, we shouldn't allow the capsule to fall
        allowFall = !(Physics.Raycast(heelLeft.transform.position, down, heelRaycastDist));

        return allowFall;
    }

    public bool HeelCheckR()
    {
        var down = transform.TransformDirection(Vector3.down);
        bool allowFall = false;
        // cast a ray down from our heel, if it hits anything, we shouldn't allow the capsule to fall
        allowFall = !(Physics.Raycast(heelRight.transform.position, down, heelRaycastDist));

        return allowFall;
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
