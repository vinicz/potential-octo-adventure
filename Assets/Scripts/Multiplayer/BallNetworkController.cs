using UnityEngine;
using System.Collections;

public class BallNetworkController : MonoBehaviour
{

    public bool printAccelometerInfo;
    public GameObject playerNameObject;
    public ShockWaveScript shockWave;
    

    void Start()
    {
        if (networkView.isMine)
        {
            
            TextMesh playerName = playerNameObject.GetComponent<TextMesh>();
            playerName.color = Color.black;

        } 

    }


    // Update is called once per frame
    void Update()
    {
        if (networkView.isMine)
        {
            Vector3 mulitpliedAcceleration = Input.acceleration * 3500;
            rigidbody.AddForce(new Vector3(mulitpliedAcceleration.x, mulitpliedAcceleration.z, mulitpliedAcceleration.y));
            //networkView.RPC("updateForce", RPCMode.AllBuffered, new Vector3(mulitpliedAcceleration.x, mulitpliedAcceleration.z, mulitpliedAcceleration.y));

            if (Input.anyKeyDown && shockWave.ShockWaveCooldownRemaining==0)
            {
                networkView.RPC("invokeShockWave", RPCMode.AllBuffered);
            }

        } 

       
        
    }
    
    void OnGUI()
    {
        if (printAccelometerInfo)
        {
            GUI.Box(new Rect(10, 10, 500, 30), Input.acceleration.x + " " + Input.acceleration.y + " " + Input.acceleration.z);
        }
        if(networkView.isMine)
            GUI.Box(new Rect(600,10,200,30),"Shockwave cooldown: "+((int)shockWave.ShockWaveCooldownRemaining));

    }

    public void initializeTeam(int i)
    {       
        networkView.RPC("setTeam", RPCMode.AllBuffered, i);
       
    }

    [RPC]
    public void setTeam(int team)
    {
        TextMesh playerName = playerNameObject.GetComponent<TextMesh>();
        playerName.text = team.ToString();


        if (team % 2 == 0)
        {
            renderer.material.color = Color.red;    
        } else
        {
            renderer.material.color = Color.blue;   
        }
    }

    [RPC]
    public void updateForce(Vector3 force)
    {

        rigidbody.AddForce(force);

    }

    [RPC]
    public void invokeShockWave()
    {
        
        shockWave.createShockWave();
        
    }









}
