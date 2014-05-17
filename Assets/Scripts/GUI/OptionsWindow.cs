using UnityEngine;
using System.Collections;

public class OptionsWindow : MonoBehaviour {

    public GameObject calibrationWindow;
    public UICheckbox soundCheckBox;
    public UICheckbox vibrationCheckBox;


    void Awake()
    {
        soundCheckBox.startsChecked = GameServiceLayer.serviceLayer.optionsService.getSoundEnabled();
        vibrationCheckBox.startsChecked = GameServiceLayer.serviceLayer.optionsService.getVibrationEnabled();
    }


	
    public void onSoundCheckBoxClicked(bool check)
    {
        if(check)
        {
            GameServiceLayer.serviceLayer.optionsService.enabledSound();
        }else
        {
            GameServiceLayer.serviceLayer.optionsService.disableSound();
        }

    }

    public void onMusicCheckBoxClicked(bool check)
    {
       
    }

    public void onVibrationCheckBoxClicked(bool check)
    {
        GameServiceLayer.serviceLayer.optionsService.setVibrationEnabled(check);
    }

    public void onCalibartionButtonClicked()
    {
        this.gameObject.SetActive(false);
        calibrationWindow.SetActive(true);
    }

   
}
