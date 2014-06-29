using UnityEngine;
using System.Collections;

public class SkillIconsViewFactory : MonoBehaviour {

    public GameObject skillIconsView;
    private  SkillIconsView newSkillIconsView;

    void Start()
    {
        initSkillIconsView();

    }

    public SkillIconsView getSkillIconsView()
    {
        initSkillIconsView();
        return newSkillIconsView;
    }


    void initSkillIconsView()
    {
        if (newSkillIconsView == null)
        {

            GameObject newSkillIconsViewObject = (GameObject)Instantiate(skillIconsView);
            newSkillIconsViewObject.transform.parent = this.transform;
            newSkillIconsViewObject.transform.localPosition = Vector3.zero;
            newSkillIconsViewObject.transform.localRotation = Quaternion.identity;
            newSkillIconsViewObject.transform.localScale = new Vector3(1, 1, 1);
            newSkillIconsView = newSkillIconsViewObject.GetComponent<SkillIconsView>();
        }
    }
}
