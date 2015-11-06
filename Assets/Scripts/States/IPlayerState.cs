using UnityEngine;
using System.Collections;


public interface IPlayerState
{
	void StartState(Vector3 vel);

	void UpdateState(Vector3 inputDir);

	void FixedUpdateState(Vector3 inputDir);

	void LateUpdateState();

	void ToStandardState();

	void ToFloatingState();

	void ToRockState();

	void OnTriggerEnter(Collider col); 
	
}

