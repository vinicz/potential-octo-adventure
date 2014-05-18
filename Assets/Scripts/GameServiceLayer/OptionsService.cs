using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionsService : MonoBehaviour
{

    public static string SOUND_ENABLED_OPTION = "sound_enabled";
    public static string MUSIC_ENABLED_OPTION = "music_enabled";
    public static string VIBRATION_ENABLED_OPTION = "vibration_enabled";

    public delegate void CalibrationCompletedHandler();
    public event CalibrationCompletedHandler CalibrationCompleted;

    public delegate void MusicEnabledOptionChangedHandler();
    public event MusicEnabledOptionChangedHandler MusicEnabledOptionChanged;

    public OrientationCalibrationService orientationCalibrationService;
    private bool soundEnabled;
    private bool musicEnabled;
    private bool vibrationEnabled;
    private Vector3 initialOrientation = new Vector3(0, 0, -1);
    private float previousVolume;
   

    void Awake()
    {
        initSound();
        initMusic();
        initVibration();
    }

   

    public bool isSoundEnabled()
    {
        return soundEnabled;
    }

    public void enabledSound()
    {
        if (previousVolume != 0)
        {
            AudioListener.volume = previousVolume;
        } else
        {
            AudioListener.volume = 1;
        }

        soundEnabled = true;
        PlayerPrefs.SetInt(SOUND_ENABLED_OPTION, 1);
        PlayerPrefs.Save();
    }

    public void disableSound()
    {
        previousVolume = AudioListener.volume;
        AudioListener.volume = 0;

        soundEnabled = false;
        PlayerPrefs.SetInt(SOUND_ENABLED_OPTION, 0);
        PlayerPrefs.Save();   
    }

    public bool isMusicEnabled()
    {
        return musicEnabled;
    }
    
    public void setMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        int musicEnabledInt = enabled ? 1 : 0;
        
        PlayerPrefs.SetInt(MUSIC_ENABLED_OPTION, musicEnabledInt);
        PlayerPrefs.Save();

        if (MusicEnabledOptionChanged != null)
        {
            MusicEnabledOptionChanged();
        }
    }


    public bool isVibrationEnabled()
    {
        return vibrationEnabled;
    }

    public void setVibrationEnabled(bool enabled)
    {
        vibrationEnabled = enabled;
        int vibrationEnabledInt = enabled ? 1 : 0;

        PlayerPrefs.SetInt(VIBRATION_ENABLED_OPTION, vibrationEnabledInt);
        PlayerPrefs.Save();
    }

    public Vector3 getInitialOrientation()
    {
        return initialOrientation;
    }

    public void calibrateInitialOrientation()
    {
        orientationCalibrationService.CalibrationCompleted += onCalibrationCompleted;
        orientationCalibrationService.calibrateOrientation();
    }

    void onCalibrationCompleted()
    {
        orientationCalibrationService.CalibrationCompleted -= onCalibrationCompleted;
        initialOrientation = orientationCalibrationService.getOrientation();

        if (CalibrationCompleted != null)
        {
            CalibrationCompleted();
        }
    }

    void getSelectedPlayerSkin()
    {

    }

    void setSelectedPlayerSkin()
    {

    }

    void getPossiblePlayerSkins()
    {

    }

    void initSound()
    {
        int soundEnabledInt = PlayerPrefs.GetInt(SOUND_ENABLED_OPTION, 1);
        soundEnabled = (soundEnabledInt == 1) ? true : false;
        if (soundEnabled)
        {
            enabledSound();
        } else
        {
            disableSound();
        }
    }

    void initMusic()
    {
        int musicEnabledInt = PlayerPrefs.GetInt(MUSIC_ENABLED_OPTION, 1);
        musicEnabled = (musicEnabledInt == 1) ? true : false;
    }

    void initVibration()
    {
        int vibrationEnabledInt = PlayerPrefs.GetInt(VIBRATION_ENABLED_OPTION, 1);
        vibrationEnabled = (vibrationEnabledInt == 1) ? true : false;
    }


}
