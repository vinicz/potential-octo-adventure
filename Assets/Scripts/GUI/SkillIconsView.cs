using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillIconsView : MonoBehaviour {

    public List<SkillIcon> skillIcons;
    public UISprite defaultIcon;
    private UISprite selectedSkillIcon;

    public void showSkillIcon(string skillId)
    {
        UISprite newSkillIcon = SkillIcon.findIconForSkill(skillIcons, skillId, defaultIcon);
       
        if (selectedSkillIcon != null)
        {
            selectedSkillIcon.gameObject.SetActive(false);
        }

        newSkillIcon.gameObject.SetActive(true);


        selectedSkillIcon = newSkillIcon;
    }
}
