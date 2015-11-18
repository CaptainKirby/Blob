using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    private Vector3 curVel;
    public float smoothTime;

    void Start () {
	
	}
	
	
	void FixedUpdate ()
    {
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref curVel.x, smoothTime), Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref curVel.y, smoothTime), transform.position.z);
    }

    void LateUpdate()
    {
        //transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref curVel.x, smoothTime), Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref curVel.y, smoothTime), transform.position.z);

    }
}
