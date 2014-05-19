using UnityEngine;
using System.Collections;

public class LevelBackgroundMusicHandler : MonoBehaviour {

	
    public AudioClip backgroundMusic;
    public float volume = 0.5f;

	void Start () {
        GameServiceLayer.serviceLayer.backgroundMusicPlayer.setupBackgroundMusicPlayer(backgroundMusic,volume);
	}
	

}
