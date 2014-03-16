using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeLauncherScript : MonoBehaviour {

    public GameObject projectile;
    public List<GameObject> launchers;
    public float fireReloadTime = 10f;
    public float velocity = 1f;
    public float launchOriginOffset =0f;

    private List<GameObject> projectilePool;
    private float reloadTimeRemaining =5f;

	// Use this for initialization
	void Start () {
	
        projectilePool = new List<GameObject>();

        for (int i=0; i<launchers.Count; i++)
        {
            GameObject newObject = (GameObject)Instantiate(projectile);
            newObject.SetActive(false);
            projectilePool.Add(newObject);
        }

	}
	
	// Update is called once per frame
	void Update () {
	
        if (reloadTimeRemaining <= 0)
        {
            for (int i=0; i<launchers.Count; i++)
            {
                projectilePool[i].SetActive(true);
                projectilePool[i].transform.position= launchers[i].transform.position;
                projectilePool[i].rigidbody.velocity= new Vector3(0f,0f,0f);
                Vector3 launchOrigin = new Vector3(this.transform.position.x,this.transform.position.y+launchOriginOffset,this.transform.position.z);
                Vector3 force= (launchers[i].transform.position- launchOrigin)*velocity;
                projectilePool[i].rigidbody.AddForce(force);
               

            }

            reloadTimeRemaining= fireReloadTime;
        }

        reloadTimeRemaining -= Time.deltaTime;

	}
}
