using UnityEngine;
using System.Collections;

public class GoalHandler : MonoBehaviour
{


    public int teamIndex;
    public SoccerGameHandler soccerGameHandler;

    void OnTriggerEnter(Collider other)
    {
        if (networkView.isMine && other.gameObject.tag == "SoccerBall")
        {
            networkView.RPC("publishGoal", RPCMode.AllBuffered);
        }

    }


    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    [RPC]
    void publishGoal()
    {
       
        soccerGameHandler.addGoalForTeam(teamIndex);
    
    }
}
