using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float speed=1.0f;
    public Vector3 axis;
	
	// Update is called once per frame
	void Update () {

        transform.Rotate (axis* Time.deltaTime*speed  );
	
	}
}
