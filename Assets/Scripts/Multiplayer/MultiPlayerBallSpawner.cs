using UnityEngine;
using System.Collections;

public class MultiPlayerBallSpawner : MonoBehaviour {

    public GameObject ballObject;
    private FootballGameMaster gameMaster;

    void Start()
    {
        gameMaster = (FootballGameMaster)GameServiceLayer.serviceLayer.gameMaster;
        gameMaster.setBallSpawner(this);

        gameMaster.GameStateChanged += onGameStateChanged;
      

    }

    public void spawnBall()
    {

        if (networkView.isMine)
        {
            Network.Instantiate(ballObject, this.transform.position, this.transform.rotation, 0);
        }
    }

    public void resetBall(GameObject ball)
    {
        ball.transform.position = transform.position;
        ball.rigidbody.velocity = new Vector3(0, 0, 0);
        ball.rigidbody.angularVelocity=new Vector3(0, 0, 0);
    }


    void onGameStateChanged()
    {
        if (gameMaster.getGameState() == GameHandlerScript.GameState.GAME)
        {
            spawnBall();
        }
    }
}
