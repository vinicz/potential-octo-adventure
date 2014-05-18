using UnityEngine;
using System.Collections;

public class BackgroundMusicPlayer : MonoBehaviour
{

    private AudioSource backgroudMusicAudioSource;

    void Awake()
    {
        backgroudMusicAudioSource = GetComponent<AudioSource>();
    }


    public void setupBackgroundMusicPlayer(AudioClip newbackgroudMusic, float volume)
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

            changeMusicPlayerToNew(newbackgroudMusic, volume);
        }

    }

    void OnDestroy()
    {
        GameServiceLayer.serviceLayer.optionsService.MusicEnabledOptionChanged -= onMusicEnabledOptionChanged;
    }


    void changeMusicPlayerToNew(AudioClip newbackgroudMusic,float volume)
    {
        backgroudMusicAudioSource.Stop();

        backgroudMusicAudioSource.clip = newbackgroudMusic;

        if (GameServiceLayer.serviceLayer.optionsService.isMusicEnabled())
        {
            backgroudMusicAudioSource.Play();
            backgroudMusicAudioSource.volume = volume;
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
