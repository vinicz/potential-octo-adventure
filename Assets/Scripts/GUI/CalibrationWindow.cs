using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalibrationWindow : MonoBehaviour
{
    const string INITIAL_CALIBRATION_TEXT = "Tap to set base\n orientation";

    public UILabel calibrationLabel;
    public GameObject previousWindow;
    private bool beforeCalibration = true;
    private bool calibrationFinished = false;
    private List<GameObject> activeWindows = new List<GameObject>();

    void Start()
    {
        disableOtherWindows();
        calibrationLabel.text = INITIAL_CALIBRATION_TEXT;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            if (beforeCalibration)
            {
                calibrationLabel.text = "Calibrating..";
                beforeCalibration = false;

                GameServiceLayer.serviceLayer.optionsService.CalibrationCompleted += onCalibrationCompleted;
                GameServiceLayer.serviceLayer.optionsService.CalibrationFailed += onCalibrationFailed;
                GameServiceLayer.serviceLayer.optionsService.calibrateInitialOrientation();
            }

            if (calibrationFinished)
            {
                calibrationFinished = false;
                this.gameObject.SetActive(false);
                previousWindow.SetActive(true);

                foreach (var window in activeWindows)
                {
                    window.SetActive(true);
                }

                beforeCalibration= true;
                calibrationLabel.text = INITIAL_CALIBRATION_TEXT;
            }

        }
    }

    void onCalibrationCompleted()
    {
        GameServiceLayer.serviceLayer.optionsService.CalibrationCompleted -= onCalibrationCompleted;
        GameServiceLayer.serviceLayer.optionsService.CalibrationFailed -= onCalibrationFailed;
        calibrationFinished = true;
        calibrationLabel.text = "Calibration finished,\n tap to exit";
      
    }

    void onCalibrationFailed()
    {
        GameServiceLayer.serviceLayer.optionsService.CalibrationCompleted -= onCalibrationCompleted;
        GameServiceLayer.serviceLayer.optionsService.CalibrationFailed -= onCalibrationFailed;
        beforeCalibration = true;
        calibrationLabel.text = "Calibration failed,\n please change the orientation\n Tap to retry";
        
    }

    void disableOtherWindows()
    {
        foreach (Transform window in this.transform.parent)
        {
            if (window != this.transform && window.gameObject.activeInHierarchy)
            {

                activeWindows.Add(window.gameObject);
                window.gameObject.SetActive(false);

            }
        }
    }
}
