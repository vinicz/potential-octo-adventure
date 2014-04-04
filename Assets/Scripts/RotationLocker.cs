using UnityEngine;
using System.Collections;

public class RotationLocker : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles=new Vector3(transform.eulerAngles.x,0,transform.eulerAngles.z);
	}
}
