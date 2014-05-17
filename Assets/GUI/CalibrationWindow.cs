using UnityEngine;
using System.Collections;

public class CalibrationWindow : MonoBehaviour {

    public UILabel calibrationLabel;
    public GameObject previousWindow;

    private bool beforeCalibration = true;
    private bool calibrationFinished = false;

    void Start()
    {
        disableOtherWindows();
    }


	void Update () {
        if (Input.anyKey)
        {
            if(beforeCalibration)
            {
                calibrationLabel.text="Calibrating..";
                beforeCalibration = false;

                GameServiceLayer.serviceLayer.optionsService.CalibrationCompleted+= onCalibrationCompleted;
                GameServiceLayer.serviceLayer.optionsService.calibrateInitialOrientation();
            }

            if(calibrationFinished)
            {
                calibrationFinished = false;
                this.gameObject.SetActive(false);
                previousWindow.SetActive(true);
            }

        }
	}

    void onCalibrationCompleted()
    {
        calibrationFinished = true;
        calibrationLabel.text="Calibration finished,\n tap to exit";
      
    }

    void disableOtherWindows()
    {
        foreach (Transform window in this.transform.parent)
        {
            if (window != this.transform)
            {
                window.gameObject.SetActive(false);
            }
        }
    }
}
