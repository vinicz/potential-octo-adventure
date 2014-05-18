using UnityEngine;
using System.Collections;

public class LevelBackgroundMusicHandler : MonoBehaviour {

	
    public AudioClip backgroundMusic;

	void Start () {
        GameServiceLayer.serviceLayer.backgroundMusicPlayer.setupBackgroundMusicPlayer(backgroundMusic);
	}
	

}
