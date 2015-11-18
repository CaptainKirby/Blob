using UnityEngine;
using System.Collections;


public interface IPlayerState
{
	void StartState(Vector3 vel);

	void UpdateState(Vector3 inputDir);

	void FixedUpdateState(Vector3 inputDir);

	void LateUpdateState();

    void ChangeState(IPlayerState state);

	void OnTriggerEnter(Collider col);

    void OnCollisionEnter(Collision col); 

}

