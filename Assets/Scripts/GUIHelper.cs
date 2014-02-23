using UnityEngine;
using System.Collections;

public class GUIHelper : MonoBehaviour
{


    public float buttonWidth;
    public float buttonHeight;
    public float bigWindowWidht;
    public float bigWindowHeight;
    public float smallWindowWidht;
    public float smallWindowHeight;

    public float originalWidth = 800.0f; // define here the original resolution
    public float originalHeight = 480.0f; // you used to create the GUI contents
    private Vector3 scale;
    private Matrix4x4 originalGUIMatrix;
    
    public float getButtonWidth()
    {
        return Screen.width * buttonWidth;
    }

    public float getButtonHeight()
    {
        return Screen.height * buttonHeight;
    }

    public float getBigWindowWidht()
    {
        return Screen.width * bigWindowWidht;
    }

    public float getBigWindowHeight()
    {
        return Screen.height * bigWindowHeight;
    }

    public float getSmallWindowWidht()
    {
        return Screen.width * smallWindowWidht;
    }

    public float getSmallWindowHeight()
    {
        return Screen.height * smallWindowHeight;
    }

    public float getLineSize()
    {
        return getButtonHeight() * 1.1f;
    }

    public Rect getRectInTheMiddle(float width, float height)
    {
        return new Rect(Screen.width / 2.0f - width / 2.0f, Screen.height / 2.0f - height / 2.0f, width, height);
    }

    public Rect getRectInTeTopMiddle(float width, float height, float margin)
    {
        return new Rect(Screen.width / 2.0f - width / 2.0f, margin, width, height);

    }

    public Rect getRectInTeBottomMiddle(float width, float height, float margin)
    {
        return new Rect(Screen.width / 2.0f - width / 2.0f, Screen.height - margin, width, height);
        
    }

    public void adjustGUIMatrix()
    {

        scale.x = Screen.width/originalWidth; // calculate hor scale
        scale.y = Screen.height/originalHeight; // calculate vert scale
        scale.z = 1;
        originalGUIMatrix = GUI.matrix; // save current matrix
        // substitute matrix - only scale is altered from standard
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
    }

    public void restoreGUIMatrix()
    {

        GUI.matrix = originalGUIMatrix; 
    }
    

}
