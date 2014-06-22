using UnityEngine;
using System.Collections;

public class FootballGameScoreWindow : MonoBehaviour {

    public UILabel gameScoreLabel;
    private FootballGameMaster gameMaster;

	
    void Start()
    {
        gameMaster = (FootballGameMaster)GameServiceLayer.serviceLayer.gameMaster;
        gameMaster.GameScoreChanged += onGameScoreChanged;
        onGameScoreChanged();
    }

    void onGameScoreChanged()
    {
        Vector2 gameScore = gameMaster.getGameScore();
        gameScoreLabel.text = gameScore [1] + " : " + gameScore [0];
    }
}
