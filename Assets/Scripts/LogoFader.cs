using UnityEngine;
using System.Collections;

public class LogoFader : MonoBehaviour
{

    public float fadeSpeed = 1f;
    private bool sceneStarting = true;
    private bool sceneEnding = false;

    void Start()
    {

        guiTexture.color = new Color(guiTexture.color.r, guiTexture.color.g, guiTexture.color.b, 0);
    }
    
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (sceneStarting)
        {
            StartScene();
        }

        if (sceneEnding)
        {
            EndScene();
        }
    }
    
    void fadeToTransparent()
    {
     
        guiTexture.color = Color.Lerp(guiTexture.color, new Color(guiTexture.color.r, guiTexture.color.g, guiTexture.color.b, 0), Time.deltaTime * 3);
    }
    
    void fadeToNormal()
    {
       
        guiTexture.color = Color.Lerp(guiTexture.color, new Color(guiTexture.color.r, guiTexture.color.g, guiTexture.color.b, 1), Time.deltaTime * 0.5f);
    }
    
    void StartScene()
    {
       
       
        fadeToNormal();
       
        if (guiTexture.color.a >= 0.5f)
        {
           
           
            sceneStarting = false;
            Application.LoadLevel(1);
            //sceneEnding = true;
        }
    }
    
    public void EndScene()
    {

        fadeToTransparent();

        if (guiTexture.color.a <= 0.005f)
        {
            guiTexture.color = new Color(guiTexture.color.r, guiTexture.color.g, guiTexture.color.b, 0);
            Application.LoadLevel(1);

        }
    }
}
