using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMoverLinear : MonoBehaviour {

    public List<GameObject> targetList;
    public float speed=0.5f;
    private int targetIndex=0;
	public bool loop = true;

	// Use this for initialization
	void Start () {
        if(targetList.Count==0)
        {
            enabled=false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (GameServiceLayer.serviceLayer.gameMaster.getGameState () == GameHandlerScript.GameState.GAME) {
			//transform.position = Vector3.Lerp (transform.position, targetList [targetIndex].transform.position, Time.deltaTime * speed);
			//transform.position = Vector3.Lerp (transform.position, targetList [targetIndex].transform.position, Time.deltaTime * speed);
			transform.position += (targetList [targetIndex].transform.position - transform.position).normalized * Time.deltaTime * speed;

			if ((transform.position - targetList [targetIndex].transform.position).magnitude < 0.2) {
				targetIndex++;
				if (targetIndex >= targetList.Count) {
					targetIndex = 0;
					if (!loop)
						enabled = false;
				}
			}
		}
	}
}
