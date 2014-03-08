using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMover : MonoBehaviour {

    public List<GameObject> targetList;
    public float speed=0.5f;
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



        transform.position = Vector3.Lerp(transform.position, targetList[targetIndex].transform.position, Time.deltaTime*speed);

        if ((transform.position - targetList [targetIndex].transform.position).magnitude < 0.2)
        {
            targetIndex++;
            if(targetIndex>=targetList.Count)
            {
                targetIndex=0;
            }
        }
	
	}
}
