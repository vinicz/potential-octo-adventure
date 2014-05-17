using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalibrationWindow : MonoBehaviour
{

    public UILabel calibrationLabel;
    public GameObject previousWindow;
    private bool beforeCalibration = true;
    private bool calibrationFinished = false;
    private List<GameObject> activeWindows = new List<GameObject>();

    void Start()
    {
        disableOtherWindows();

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
            }

        }
    }

    void onCalibrationCompleted()
    {
        calibrationFinished = true;
        calibrationLabel.text = "Calibration finished,\n tap to exit";
      
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
