using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerSkillStorage : MonoBehaviour {

    [Serializable]
	public class PlayerSkillStruct{
        public string skillId;
        public Skill skill;
    }

    public List<PlayerSkillStruct> playerSkillList;


    public void useSkill(string skillName, GameObject targetObject)
    {
        PlayerSkillStruct currentPlayerSkill = null;

        currentPlayerSkill = findSkillByName(skillName);

        currentPlayerSkill.skill.useSkill(targetObject);
    }

    public float getCooldownReamining(string skillId)
    {
        PlayerSkillStruct currentPlayerSkill = null;
        
        currentPlayerSkill = findSkillByName(skillId);

        return currentPlayerSkill.skill.getCoolDownRemaining();
    }

    public List<string> getPlayerSkillList()
    {
        List<string> playerSkillStrings = new List<string>();

        foreach (PlayerSkillStruct playerSkill in playerSkillList)
        {
            playerSkillStrings.Add(playerSkill.skillId);
        }

        return playerSkillStrings;
    }

    PlayerSkillStruct findSkillByName(string skillName)
    {
        PlayerSkillStruct currentPlayerSkill = null;

        foreach (PlayerSkillStruct playerSkill in playerSkillList)
        {
            if (playerSkill.skillId.Equals(skillName))
            {
                currentPlayerSkill = playerSkill;
                break;
            }
        }

        return currentPlayerSkill;
    }

}
