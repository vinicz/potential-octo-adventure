using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMover : MonoBehaviour {

    public List<GameObject> targetList;
    public int speed;
    private int targetIndex=0;
   

	// Use this for initialization
	void Start () {
	
        if(targetList.Count==0)
        {
            enabled=false;
        }

	}
	
	// Update is called once per frame
	void Update () {



        //Vector3.Lerp(transform.position, 
	
	}
}
