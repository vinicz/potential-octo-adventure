using UnityEngine;
using System.Collections;

public class GoalHandler : MonoBehaviour
{


    public int teamIndex;
    private FootballGameMaster gameMaster;

    void Start()
    {
        gameMaster = (FootballGameMaster)GameServiceLayer.serviceLayer.gameMaster;

    }
   

    void OnTriggerEnter(Collider other)
    {
        if (networkView.isMine && other.gameObject.tag == "SoccerBall")
        {
            gameMaster.addGoalToTeam(teamIndex,other.gameObject);
        }

    }


   


}
