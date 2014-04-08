using UnityEngine;
using System.Collections;

public class CameraAccelometerController : MonoBehaviour
{

    public Vector3 initialAcceleration = new Vector3(0, 0, -1);
    public float maxRotationAngle;
    private Vector3 initialCameraForward;
    private Quaternion initialCameraOrientation;

    void Start()
    {
        initialCameraForward = this.transform.forward;
        initialCameraOrientation = this.transform.rotation;
    }

    void Update()
    {
        if (GameHandlerScript.gameState == GameHandlerScript.GameState.GAME)
        {

            Vector3 fixedAcceleration = new Vector3(
                Input.acceleration.x, 
                Input.acceleration.z, 
                Input.acceleration.y);


            Quaternion fullRotation = Quaternion.FromToRotation(initialCameraForward, fixedAcceleration);
            float xRotation = fullRotation.eulerAngles.x;
            float zRotation = fullRotation.eulerAngles.z;

            if (xRotation > maxRotationAngle && xRotation < 180)
            {
                xRotation = maxRotationAngle;
            }
            if (xRotation < 360 - maxRotationAngle && xRotation > 180)
            {
                xRotation = 360 - maxRotationAngle;
            }
            if (zRotation > maxRotationAngle && zRotation < 180)
            {
                zRotation = maxRotationAngle;
            }
            if (zRotation < 360 - maxRotationAngle && zRotation > 180)
            {
                zRotation = 360 - maxRotationAngle;
            }
            Quaternion fixedYRotation = Quaternion.Euler(xRotation, 0, zRotation);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, fixedYRotation * initialCameraOrientation, Time.deltaTime * 5);
            //this.transform.rotation = fixedYRotation * initialCameraOrientation;
            //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, fixedYRotation * initialCameraOrientation, Time.deltaTime*10);
        } 
    }
}
