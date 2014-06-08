using UnityEngine;
using System.Collections;

public class NetworkPositionUpdater : MonoBehaviour {

 
	public float interpolationSpeed=50f;

    private Vector3 targetPosition;
	private Quaternion targetOrientation;


    void Start()
    {
        targetPosition = transform.position;
		targetOrientation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (networkView.isMine)
        {
             networkView.RPC("updatePosition", RPCMode.AllBuffered, transform.position);
			 networkView.RPC("updateRotation", RPCMode.AllBuffered, transform.rotation);
				
        } else
        {
			//rigidbody.isKinematic = true;
			Vector3 distanceVector = targetPosition - this.transform.position;

			if(distanceVector.magnitude > 0.01)
			{
				this.transform.position = Vector3.Lerp(this.transform.position,targetPosition, Time.deltaTime *interpolationSpeed);
			}

			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetOrientation, Time.deltaTime *interpolationSpeed );

			//rigidbody.isKinematic = false;
        }
        
        
    }

    
    [RPC]
    public void updatePosition(Vector3 position)
    {
        targetPosition = position;
        
    }

	[RPC]
	public void updateRotation(Quaternion rotation)
	{
		targetOrientation = rotation;
		
	}
}
