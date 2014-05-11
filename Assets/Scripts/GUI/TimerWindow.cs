using UnityEngine;
using System.Collections;
using System;

public class TimerWindow : MonoBehaviour {

    public UILabel timeLabel;

	void Update () {
	
        float gameTimeLeft = GameServiceLayer.serviceLayer.gameMaster.getGameTimeLeft();
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTimeLeft);
        string timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        timeLabel.text = timeText;
	}
}
