using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class SkillButton : MonoBehaviour
{

    [Serializable]
    public class SkillIcon
    {
        public UISprite icon;
        public string skillName;
    }

    public UISprite defaultIcon;
    public List<SkillIcon> skillIcons;
    public UILabel coolDownTimer;
    public UIButton skillButton;
    protected string currentSkillId;
    private UISprite currentIcon;

    // Use this for initialization
    void Start()
    {
        currentSkillId = getCurrentSkill();
                    
        if (currentSkillId != null)
        {

            findIconForSkill();
        } else
        {
            this.gameObject.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        float coolDownRemaining = getCoolDownRemaining();
                
        if (coolDownRemaining > 0)
        {
            coolDownTimer.text = ((int)coolDownRemaining).ToString();
            skillButton.isEnabled = false;
        } else
        {
            skillButton.isEnabled = true;
            coolDownTimer.gameObject.SetActive(false);
            currentIcon.gameObject.SetActive(true);
        }
    }

    void OnClick()
    {
        useSkill();
        coolDownTimer.gameObject.SetActive(true);
        currentIcon.gameObject.SetActive(false);
    }

    protected abstract string getCurrentSkill();

    protected abstract float getCoolDownRemaining();

    protected abstract void useSkill();

    void findIconForSkill()
    {
        bool iconFoundForSkill = false;
        foreach (SkillIcon icon in skillIcons)
        {
            if (currentSkillId.Equals(icon.skillName))
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
    }
}
