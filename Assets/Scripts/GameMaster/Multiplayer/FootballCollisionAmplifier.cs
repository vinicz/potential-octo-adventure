using UnityEngine;
using System.Collections;

public class FootballCollisionAmplifier : MonoBehaviour {

    void OnCollisionEnter(Collision collision) {

        if (collision.rigidbody!=null && collision.gameObject.networkView != null && collision.gameObject.networkView.isMine && !Network.isServer)
        {
            Vector3 collisionVelocity = collision.rigidbody.velocity;
            float forceStrength = collisionVelocity.magnitude;
            Vector3 forceDirection = (this.gameObject.transform.position - collision.gameObject.transform.position).normalized;
            Vector3 collisionForce = forceDirection * forceStrength*10;

            networkView.RPC("amplifyCollision", RPCMode.Server, collisionForce);
        } 
    }

    [RPC]
    void amplifyCollision(Vector3 force)
    {
        this.rigidbody.AddForce(force);
    }
}
