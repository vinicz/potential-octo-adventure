using UnityEngine;
using System.Collections;

public class AccelometerController : MonoBehaviour
{

    public bool printAccelometerInfo;
    public float speed = 30;

    // Update is called once per frame
    void Update()
    {

        if (GameHandlerScript.gameState == GameHandlerScript.GameState.GAME)
        {
            Vector3 mulitpliedAcceleration = Input.acceleration * speed;
            rigidbody.AddForce(mulitpliedAcceleration.x, mulitpliedAcceleration.z, mulitpliedAcceleration.y);
        }
                

    }

    void OnGUI()
    {
        if (printAccelometerInfo)
        {
            GUI.Box(new Rect(10, 10, 500, 30), Input.acceleration.x + " " + Input.acceleration.y + " " + Input.acceleration.z);
        }

    }
}
