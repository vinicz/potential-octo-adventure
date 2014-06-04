using UnityEngine;
using System.Collections;

public class AccelometerController : MonoBehaviour
{

    public bool printAccelometerInfo;
    public Vector3 speed = new Vector3(30, 30, 30);
    public Vector3 initialAcceleration = new Vector3(0, 0, -1);
    public float accelerationStrength = 2.0f;
    private Vector3 mulitpliedAcceleration = Vector3.zero;

    void Start()
    {
        initialAcceleration = GameServiceLayer.serviceLayer.optionsService.getInitialOrientation();
    }


    // Update is called once per frame
    void Update()
    {
		if (GameServiceLayer.serviceLayer.gameMaster.getGameState() == GameHandlerScript.GameState.GAME)
        {

            Vector3 rotationAxis = Vector3.Cross(initialAcceleration, Input.acceleration);
            float angleBetweenDirections = Vector3.Angle(initialAcceleration, Input.acceleration);
            Quaternion rotationAmplifierQuaternion = Quaternion.AngleAxis(angleBetweenDirections * accelerationStrength, rotationAxis);
            mulitpliedAcceleration = rotationAmplifierQuaternion * new Vector3(0f,0f,-1f);

            
            float xHelperForceValue =  Mathf.Abs(Input.acceleration.y)*2;
            float xHelperForce = xHelperForceValue>=1 ? xHelperForceValue:1;
            float zHelperForceValue =  Mathf.Abs(Input.acceleration.x)*2;
            float zHelperForce = zHelperForceValue>=1 ? zHelperForceValue:1;

            mulitpliedAcceleration = new Vector3(
                mulitpliedAcceleration.x * speed.x * xHelperForce, 
                mulitpliedAcceleration.y * speed.z, 
                mulitpliedAcceleration.z * speed.y * zHelperForce);

            rigidbody.AddForce(
                mulitpliedAcceleration.x * Time.deltaTime, 
                mulitpliedAcceleration.z * Time.deltaTime,
                mulitpliedAcceleration.y * Time.deltaTime);
        }       
    }

    void OnGUI()
    {
        if (printAccelometerInfo)
        {
            GUI.Box(new Rect(10, 10, 500, 30), Input.acceleration.x + " " + Input.acceleration.y + " " + Input.acceleration.z);
            GUI.Box(new Rect(10, 40, 500, 30), mulitpliedAcceleration.x + " " + mulitpliedAcceleration.y + " " + mulitpliedAcceleration.z);
        }
       

    }
}
