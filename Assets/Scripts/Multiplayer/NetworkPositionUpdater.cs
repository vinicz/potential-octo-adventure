using UnityEngine;
using System.Collections;

public class NetworkPositionUpdater : MonoBehaviour {

 
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (networkView.isMine)
        {
             networkView.RPC("updatePosition", RPCMode.AllBuffered, transform.position);
        } else
        {
            Vector3 dif = targetPosition - transform.position;
            if(dif.magnitude>2.5)
            {
                transform.position=targetPosition;
            }else
            {
                rigidbody.AddForce(dif*2);
            }

           
        }
        
        
    }

    
    [RPC]
    public void updatePosition(Vector3 position)
    {
        targetPosition = position;
        
    }
}
