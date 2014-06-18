using UnityEngine;
using System.Collections;

public class CameraAccelometerController : MonoBehaviour
{

    public float maxXRotationAngle = 10f;
    public float maxZRotationAngle = 20f;
    private Vector3 initialOrientation;
    private Quaternion initialCameraOrientation;
	private bool zRotationAdjuster;
	private float zRotationAdjusterChangedTimer=0;

	private float xRotation;
	private float zRotation;


    void Start()
    {
        initialOrientation = GameServiceLayer.serviceLayer.optionsService.getInitialOrientation();
        initialCameraOrientation = this.transform.rotation;
    }

    void Update()
    {
        if (GameServiceLayer.serviceLayer.gameMaster.getGameState() == GameHandlerScript.GameState.GAME)
        {

//			bool xIn90Degrees = (Mathf.Abs(Input.acceleration.x) >= 0.95 && Mathf.Abs(Input.acceleration.x) <= 1.05);
//			bool yIn90Degrees = (Mathf.Abs(Input.acceleration.y) >= 0.95 && Mathf.Abs(Input.acceleration.y) <= 1.05);
//
//			if(xIn90Degrees || yIn90Degrees)
//			{
//				return;
//			}

            Quaternion fullRotation = Quaternion.FromToRotation(initialOrientation, Input.acceleration);


			bool zRotationAdjuster = Input.acceleration.y *Input.acceleration.z <0;
//			if(newzRotationAdjuster!=zRotationAdjuster)
//			{
//                zRotationAdjusterChangedTimer +=  Time.deltaTime;
//
//                if(zRotationAdjusterChangedTimer> 0.5f)
//				{
//					zRotationAdjuster = newzRotationAdjuster;
//				}
//			}else
//			{
//                zRotationAdjusterChangedTimer=0;
//			}




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
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, fixedYRotation * initialCameraOrientation, Time.deltaTime*5);
            //this.transform.rotation = fixedYRotation * initialCameraOrientation;
            //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, fixedYRotation * initialCameraOrientation, Time.deltaTime*10);
        } 
    }
}
