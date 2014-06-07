﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionsService : MonoBehaviour
{

    public static string SOUND_ENABLED_OPTION = "sound_enabled";
    public static string MUSIC_ENABLED_OPTION = "music_enabled";
    public static string VIBRATION_ENABLED_OPTION = "vibration_enabled";
    public static string INITIAL_ORIENTATION_X = "initial_orientation_x";
    public static string INITIAL_ORIENTATION_Y = "initial_orientation_y";
    public static string INITIAL_ORIENTATION_Z = "initial_orientation_z";
	public static string PLAYER_CHARACTER_NAME = "player_character";

    public delegate void CalibrationCompletedHandler();
    public event CalibrationCompletedHandler CalibrationCompleted;

    public delegate void CalibrationFailedHandler();
    public event CalibrationFailedHandler CalibrationFailed;

    public delegate void MusicEnabledOptionChangedHandler();
    public event MusicEnabledOptionChangedHandler MusicEnabledOptionChanged;

	public delegate void SelectedCharacterChangedHandler();
	public event SelectedCharacterChangedHandler SelectedCharacterChanged;

	public delegate void PreviewCharacterChangedHandler();
	public event PreviewCharacterChangedHandler PreviewCharacterChanged;
	
    public OrientationCalibrationService orientationCalibrationService;
	public PlayerCharacterStorage playerCharacterStorage;
    private bool soundEnabled;
    private bool musicEnabled;
    private bool vibrationEnabled;
    private Vector3 initialOrientation = new Vector3(0, 0, -1);
    private float previousVolume;
	private string playerCharacterName;
	private PlayerCharacter playerCharacter;
	private PlayerCharacter previewCharacter;

    void Awake()
    {
        initSound();
        initMusic();
        initVibration();
        initOrientation();
		initPlayerCharacter ();


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
       
        Vector3 calibratedOrientation = orientationCalibrationService.getOrientation();
        bool xIn90Degrees = (Mathf.Abs(calibratedOrientation.x) >= 0.9 && Mathf.Abs(calibratedOrientation.x) <= 1.1);
        bool yIn90Degrees = (Mathf.Abs(calibratedOrientation.y) >= 0.9 && Mathf.Abs(calibratedOrientation.y) <= 1.1);

        if (xIn90Degrees || yIn90Degrees)
        {
            if (CalibrationFailed != null)
            {
                CalibrationFailed();
            }
        } else
        {
            initialOrientation = calibratedOrientation;

            PlayerPrefs.SetFloat(INITIAL_ORIENTATION_X, initialOrientation.x);
            PlayerPrefs.SetFloat(INITIAL_ORIENTATION_Y, initialOrientation.y);
            PlayerPrefs.SetFloat(INITIAL_ORIENTATION_Z, initialOrientation.z);
            PlayerPrefs.Save();

            if (CalibrationCompleted != null)
            {
                CalibrationCompleted();
            }
        }
    }

	public PlayerCharacter getSelectedPlayerCharacter()
    {
		return playerCharacter;
    }

	public void setSelectedPlayerCharacter(PlayerCharacter character)
    {
		playerCharacter = character;
		PlayerPrefs.SetString (PLAYER_CHARACTER_NAME, playerCharacter.productId);

		if (SelectedCharacterChanged != null) 
		{
			SelectedCharacterChanged();
		}
    }

	public PlayerCharacter getPreviewPlayerCharacter()
	{
		return previewCharacter;
	}
	
	public void setPreviewPlayerCharacter(PlayerCharacter character)
	{
		previewCharacter = character;
		
		if (PreviewCharacterChanged != null) 
		{
			PreviewCharacterChanged();
		}
	}

	public List<PlayerCharacter> getPossiblePlayerCharacters()
    {
		return playerCharacterStorage.getPlayerCharacters ();
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

    void initOrientation()
    {
        float initialOrientationX = PlayerPrefs.GetFloat(INITIAL_ORIENTATION_X,0f);
        float initialOrientationY = PlayerPrefs.GetFloat(INITIAL_ORIENTATION_Y,0f);
        float initialOrientationZ = PlayerPrefs.GetFloat(INITIAL_ORIENTATION_Z,-1f);

        initialOrientation = new Vector3(initialOrientationX, initialOrientationY, initialOrientationZ);

    }

	void initPlayerCharacter ()
	{
		List<PlayerCharacter> characters = playerCharacterStorage.getPlayerCharacters ();
		string playerCharacterName = PlayerPrefs.GetString (PLAYER_CHARACTER_NAME, "");
		foreach (PlayerCharacter character in characters) {
			if (character.productId.Equals (playerCharacterName)) {
				playerCharacter = character;
				break;
			}
		}
	}

}
