using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMoveAndRotate: MonoBehaviour {

	public List<GameObject> targetList;
	public bool loop = true;
    public float moveSpeed = 0.5f;
	public float rotationSpeed = 0.5f;
	public Vector3 rotationAxis;

	private int targetIndex=0;
	private Vector3 targetPosition;

	void Start () {
        if(targetList.Count==0)
        {
            enabled=false;
        }
	}

	void Update () {
		if (GameServiceLayer.serviceLayer.gameMaster.getGameState () == GameHandlerScript.GameState.GAME) {
			targetPosition = targetList [targetIndex].transform.position;

			transform.position += (targetPosition - transform.position).normalized * Time.deltaTime * moveSpeed;
			transform.Rotate(rotationAxis * Time.deltaTime * rotationSpeed);

			if ((transform.position - targetPosition).magnitude < 0.2) {
				targetIndex++;
				rotationSpeed *= -1f;
				if (targetIndex >= targetList.Count) {
					targetIndex = 0;
					if (!loop)
						enabled = false;
				}
			}
		}
	}
}
