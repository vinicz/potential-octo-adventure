using UnityEngine;
using System.Collections;

public class UICenterOnNext : MonoBehaviour {

    public UICenterHelper centerHandlerObject;

    void Start()
    {
        centerHandlerObject.CenteredGameObjectChanged += onCenteredObjectChanged;
    }

    public void OnClick()
    {
       centerHandlerObject.centerOnNextItem();
    }

    void onCenteredObjectChanged()
    {
        if (centerHandlerObject.isTheLastItemSelected())
        {
            this.gameObject.SetActive(false);
        } else
        {
            this.gameObject.SetActive(true);
        }
    }

    void OnDestroy()
    {
        centerHandlerObject.CenteredGameObjectChanged -= onCenteredObjectChanged;
    }
}
