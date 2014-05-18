using UnityEngine;
using System.Collections;

public class BackgroundMusicPlayer : MonoBehaviour
{

    private AudioSource backgroudMusicAudioSource;

    void Awake()
    {
        backgroudMusicAudioSource = GetComponent<AudioSource>();
    }


    public void setupBackgroundMusicPlayer(AudioClip newbackgroudMusic)
    {
        string newbackgroudMusicName = newbackgroudMusic.name;
        string currentBackgroudMusicName = null;
        AudioClip currentBackgroudMusicAudioClip = backgroudMusicAudioSource.clip;

        if (currentBackgroudMusicAudioClip!=null)
        {
            currentBackgroudMusicName = currentBackgroudMusicAudioClip.name;
        }

        if (currentBackgroudMusicName != newbackgroudMusicName)
        {

            changeMusicPlayerToNew(newbackgroudMusic);
        }

    }

    void OnDestroy()
    {
        GameServiceLayer.serviceLayer.optionsService.MusicEnabledOptionChanged -= onMusicEnabledOptionChanged;
    }


    void changeMusicPlayerToNew(AudioClip newbackgroudMusic)
    {
        backgroudMusicAudioSource.Stop();

        backgroudMusicAudioSource.clip = newbackgroudMusic;

        if (GameServiceLayer.serviceLayer.optionsService.isMusicEnabled())
        {
            backgroudMusicAudioSource.Play();
        } 
        
        GameServiceLayer.serviceLayer.optionsService.MusicEnabledOptionChanged += onMusicEnabledOptionChanged;

    }

    void onMusicEnabledOptionChanged()
    {
        bool musicEnabled = GameServiceLayer.serviceLayer.optionsService.isMusicEnabled();

        backgroudMusicAudioSource.mute = !musicEnabled;

        if (!backgroudMusicAudioSource.isPlaying && musicEnabled)
        {
            backgroudMusicAudioSource.Play();
        }

    }



    
}
