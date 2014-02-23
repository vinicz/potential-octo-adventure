using UnityEngine;
using System.Collections;

public class SoccerGameHandler : MonoBehaviour
{

    private int team1GoalCount;
    private int team2GoalCount;
    public GameObject ball;


    // Use this for initialization
    void Start()
    {
        team1GoalCount = 0;
        team2GoalCount = 0;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void OnGUI()
    {

        GUI.Box (new Rect (200, 10, 300, 30),  team1GoalCount + ":" + team2GoalCount);


    }

   

    
    // Update is called once per frame
    void Update()
    {
    
    }

    public void addGoalForTeam(int i)
    {
        if (i == 0)
        {
            team1GoalCount++;
        } else if (i == 1)
        {
            team2GoalCount++;
        }

        ball.transform.position = transform.position;
        ball.rigidbody.velocity = new Vector3(0, 0, 0);
        ball.rigidbody.angularVelocity=new Vector3(0, 0, 0);
    }
}
