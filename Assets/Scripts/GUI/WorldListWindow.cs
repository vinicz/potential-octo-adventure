using UnityEngine;
using System.Collections;

public class WorldListWindow : MonoBehaviour
{

    public UIGrid parentGrid;
    public WorldItem worldItem;
    
    // Use this for initialization
    void Start()
    {
        
        foreach (string world in GameServiceLayer.serviceLayer.levelService.getSinglePlayerWorldNames())
        {
            worldItem.createWorldItem(parentGrid.gameObject, world, this.gameObject);
        }
        parentGrid.Reposition();
        
    }

}
