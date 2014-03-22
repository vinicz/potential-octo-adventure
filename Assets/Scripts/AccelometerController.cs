using UnityEngine;
using System.Collections;

public class AccelometerController : MonoBehaviour
{

    public bool printAccelometerInfo;
    public float speed = 30;
    public float rotateAngle = 20;
    public bool addRelativeChange = false;
    private Vector3 lastAcceleration;

    void Start()
    {
        lastAcceleration = Input.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mulitpliedAcceleration = Vector3.zero;

        if (GameHandlerScript.gameState == GameHandlerScript.GameState.GAME)
        {


            mulitpliedAcceleration = Input.acceleration * speed;
            Quaternion rotateQuaternion = Quaternion.AngleAxis(rotateAngle, Vector3.right);
            mulitpliedAcceleration = rotateQuaternion * mulitpliedAcceleration;

            if (addRelativeChange)
            {
                mulitpliedAcceleration += (Input.acceleration - lastAcceleration) * speed;

            }  
           
        }

        rigidbody.AddForce(mulitpliedAcceleration.x, mulitpliedAcceleration.z, mulitpliedAcceleration.y);
                

    }

    void OnGUI()
    {
        if (printAccelometerInfo)
        {
            GUI.Box(new Rect(10, 10, 500, 30), Input.acceleration.x + " " + Input.acceleration.y + " " + Input.acceleration.z);
        }

    }
}
