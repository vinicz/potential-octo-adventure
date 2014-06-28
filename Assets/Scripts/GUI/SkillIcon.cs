using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class SkillIcon
{
    public UISprite icon;
    public string skillName;



    public static UISprite findIconForSkill(List<SkillIcon> skillIcons,string skillId, UISprite defaultIcon)
    {
        bool iconFoundForSkill = false;
        UISprite currentIcon = null;

        foreach (SkillIcon icon in skillIcons)
        {
            if (skillId.Equals(icon.skillName))
            {
                icon.icon.gameObject.SetActive(true);
                iconFoundForSkill = true;
                currentIcon = icon.icon;
                break;
            }
        }
        
        if (!iconFoundForSkill)
        {
            defaultIcon.gameObject.SetActive(true);
            currentIcon = defaultIcon;
        }

        return currentIcon;
    }
}