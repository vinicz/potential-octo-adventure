using UnityEngine;
using System.Collections;

public class AccelometerController : MonoBehaviour
{

    public bool printAccelometerInfo;
    public Vector3 speed = new Vector3(30, 30, 30);
    public float rotateAngle = 20;
    private Vector3 lastAcceleration;

    void Start()
    {
        lastAcceleration = Input.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandlerScript.gameState == GameHandlerScript.GameState.GAME)
        {
            Vector3 mulitpliedAcceleration = Vector3.zero;

            mulitpliedAcceleration = new Vector3(Input.acceleration.x* speed.x, Input.acceleration.y* speed.z,Input.acceleration.z* speed.y);
            Quaternion rotateQuaternion = Quaternion.AngleAxis(rotateAngle, Vector3.right);
            mulitpliedAcceleration = rotateQuaternion * mulitpliedAcceleration;

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
