using UnityEngine;
using System.Collections;

public class BowlingCameraScript : MonoBehaviour {

    public GameObject targetObject;
    public int distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       // transform.LookAt(targetObject.transform.position);

        Vector3 newPosition = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + distance, targetObject.transform.position.z);
       

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 1.5f);
	
	}
}
