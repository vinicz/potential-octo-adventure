using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UICenterHelper : MonoBehaviour
{

    public delegate void CenteredGameObjectChangedHandler();

    public event CenteredGameObjectChangedHandler CenteredGameObjectChanged;

    private bool inited = false;
    private UICenterOnChildImproved centerOnChild;
    private List<GameObject> childObjects;
    private GameObject centeredGameObject;

    void Start()
    {
        centerOnChild = this.GetComponent<UICenterOnChildImproved>(); 
    }

    void Update()
    {
        if (!inited)
        {
            childObjects = new List<GameObject>();
            
            foreach (Transform child in this.transform)
            {
                GameObject childObject = child.gameObject;
                childObjects.Add(childObject);
            }

            centerOnChild.CenterOn(childObjects [0].transform);
            inited = true;

            centeredGameObject = childObjects [0];
        }

        checkIfCenteredItemChanged();

    }

    public void centerOnNextItem()
    {
        int centeredItemIndex = getCenteredItemIndex();

        if (childObjects.Count > centeredItemIndex + 1)
        {
            centerOnChild.CenterOn(childObjects [centeredItemIndex + 1].transform);
        }

    }

    public void centerOnPreviousItem()
    {
    
        int centeredItemIndex = getCenteredItemIndex();

        if (0 <= centeredItemIndex - 1)
        {
            centerOnChild.CenterOn(childObjects [centeredItemIndex - 1].transform);
        }

    }

    public bool isTheLastItemSelected()
    {
        int centeredItemIndex = getCenteredItemIndex();
        bool lastItemSelected = (centeredItemIndex == childObjects.Count - 1);

        return lastItemSelected;
    }

    public bool isTheFirstItemSelected()
    {
        int centeredItemIndex = getCenteredItemIndex();
        bool firstItemSelected = (centeredItemIndex == 0);
        
        return firstItemSelected;
    }

    void checkIfCenteredItemChanged()
    {
        if (centerOnChild.centeredObject != centeredGameObject)
        {
            centeredGameObject = centerOnChild.centeredObject;
            if (CenteredGameObjectChanged != null)
            {
                CenteredGameObjectChanged();
            }
        }
    }

    int getCenteredItemIndex()
    {

        int index = 0;
        foreach (GameObject childObject in childObjects)
        {
            if (childObject == centerOnChild.centeredObject)
            {
                break;
            }
            
            index++;
        }

        return index;
    }
    
    
}
