using UnityEngine;
using System.Collections;

public class MultiplayerNameLabel : MonoBehaviour {

	
    private GameObject targetPlayer;
    private Quaternion initialOrientation;

	void Start () {

        targetPlayer = this.transform.parent.gameObject;
        initialOrientation = this.transform.rotation;
	
	}
	
	void Update () {
	
        this.transform.position = targetPlayer.transform.position + new Vector3(-0.5f, 1, 0);
        this.transform.rotation = initialOrientation;
	}
}
