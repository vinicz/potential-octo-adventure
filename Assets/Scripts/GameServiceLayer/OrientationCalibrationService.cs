using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrientationCalibrationService : MonoBehaviour
{
    public delegate void CalibrationCompletedHandler();
    public event CalibrationCompletedHandler CalibrationCompleted;

    private Vector3 orientation;
    private bool calibratingOrientation = false;
    private List<Vector3> calibrationData = new List<Vector3>();
    private float timeSinsceLastDataCollection = 0;
    private float dataCollectionInterval = 0.01f;

    void Update()
    {
        if (calibratingOrientation)
        {
            timeSinsceLastDataCollection += Time.deltaTime;
            
            collectCalibrationData();
            
            if (calibrationData.Count >= 100)
            {
                calculateNewOrientation();
                finalizeCalibration();
            }
        }   
    }

    void collectCalibrationData()
    {
        if (timeSinsceLastDataCollection >= dataCollectionInterval)
        {
            calibrationData.Add(Input.acceleration);
        }
    }

    void calculateNewOrientation()
    {
        Vector3 sumOfData = Vector3.zero;
        foreach (Vector3 orientationData in calibrationData)
        {
            sumOfData += orientationData;
        }
        orientation = sumOfData / calibrationData.Count;
    }

    void finalizeCalibration()
    {
        calibratingOrientation = false;
        calibrationData.Clear();

        if (CalibrationCompleted != null)
        {
            CalibrationCompleted();
        }
    }

    public Vector3 getOrientation()
    {
        return orientation;
    }
    
    public void calibrateOrientation()
    {
        calibratingOrientation = true;
    }
}
