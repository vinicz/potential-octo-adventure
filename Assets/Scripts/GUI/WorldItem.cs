using UnityEngine;
using System.Collections;

public class WorldItem : MonoBehaviour {

    public WorldWindow worldWindow;
    public string worldName;
    public WorldButtonTool worldButton;
    public UILabel worldNameLabel;

	public GameObject createWorldItem(GameObject parent, string name, GameObject currentWindow)
    {
        worldName = name;
        worldNameLabel.text = name;

        GameObject newWindow = createWorldWindow(currentWindow);
        setUpButtonForWorld(currentWindow, newWindow);
        GameObject newWorldItem = createWorldItem(parent);

        return newWorldItem;
    }

    GameObject createWorldWindow(GameObject currentWindow)
    {
        GameObject newWindow = worldWindow.createWorldWindow(currentWindow.transform.parent.gameObject, worldName,currentWindow);
        newWindow.SetActive(false);
        return newWindow;
    }

    void setUpButtonForWorld(GameObject currentWindow, GameObject newWindow)
    {
        worldButton.worldWindow = newWindow;
        worldButton.currentWindow = currentWindow;
    }

    GameObject createWorldItem(GameObject parent)
    {
        GameObject newWorldItem = NGUITools.AddChild(parent, this.gameObject);
        newWorldItem.transform.localPosition = this.transform.position;
        newWorldItem.transform.localRotation = this.transform.rotation;
        newWorldItem.transform.localScale = this.transform.localScale;
        return newWorldItem;
    }
}
