using UnityEngine;
using System.Collections;
using System;

public class TimerWindow : MonoBehaviour {

    public UILabel timeLabel;

	void Update () {
	
        float elapsedTime = GameServiceLayer.serviceLayer.gameMaster.getElapsedTime();
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        string timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        timeLabel.text = timeText;
	}
}
