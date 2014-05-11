using UnityEngine;
using System.Collections;

public class UICenterOnPrevious : MonoBehaviour {

    public UICenterHelper centerHandlerObject;

    void Start()
    {
        centerHandlerObject.CenteredGameObjectChanged += onCenteredObjectChanged;
        this.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        centerHandlerObject.centerOnPreviousItem();
    }

    void onCenteredObjectChanged()
    {
        if (centerHandlerObject.isTheFirstItemSelected())
        {
            this.gameObject.SetActive(false);
        } else
        {
            this.gameObject.SetActive(true);
        }
    }
}
