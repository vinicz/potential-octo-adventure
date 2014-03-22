using UnityEngine;
using System.Collections;

public class BackKeyHandler : MonoBehaviour
{
    public GameObject targetWindow;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (targetWindow!=null)
            {
                this.gameObject.SetActive(false);
                targetWindow.SetActive(true);
            } else
            {
            
                Application.Quit(); 
            }
        
        
        }
    
    
    }
}
