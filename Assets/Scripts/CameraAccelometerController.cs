using UnityEngine;
using System.Collections;

public class CameraAccelometerController : MonoBehaviour
{

    public float maxXRotationAngle = 10f;
    public float maxZRotationAngle = 20f;
    private Vector3 initialOrientation;
    private Quaternion initialCameraOrientation;

    void Start()
    {
        initialOrientation = GameServiceLayer.serviceLayer.optionsService.getInitialOrientation();
        initialCameraOrientation = this.transform.rotation;
    }

    void Update()
    {
        if (GameServiceLayer.serviceLayer.gameMaster.getGameState() == GameHandlerScript.GameState.GAME)
        {

//            Vector3 fixedAcceleration = new Vector3(
//                Input.acceleration.x, 
//                Input.acceleration.z, 
//                Input.acceleration.y);



            Quaternion fullRotation = Quaternion.FromToRotation(initialOrientation, Input.acceleration);
            bool zRotationAdjuster = Input.acceleration.y *Input.acceleration.z <0;

            float xRotation = 360-fullRotation.eulerAngles.x;
            float zRotation =  zRotationAdjuster?  360-fullRotation.eulerAngles.z : fullRotation.eulerAngles.z;

            if (xRotation > maxXRotationAngle && xRotation < 180)
            {
                xRotation = maxXRotationAngle;
            }
            if (xRotation < 360 - maxXRotationAngle && xRotation > 180)
            {
                xRotation = 360 - maxXRotationAngle;
            }
            if (zRotation > maxZRotationAngle && zRotation < 180)
            {
                zRotation = maxZRotationAngle;
            }
            if (zRotation < 360 - maxZRotationAngle && zRotation > 180)
            {
                zRotation = 360 - maxZRotationAngle;
            }
            Quaternion fixedYRotation = Quaternion.Euler(xRotation, 0, zRotation);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, fixedYRotation * initialCameraOrientation, Time.deltaTime * 5);
            //this.transform.rotation = fixedYRotation * initialCameraOrientation;
            //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, fixedYRotation * initialCameraOrientation, Time.deltaTime*10);
        } 
    }
}
