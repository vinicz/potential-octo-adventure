using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkillHandler : MonoBehaviour
{

    
    public PlayerSkillStorage skillStoragePrefab;
    private PlayerSkillStorage ownSkillStorage;

    void Start()
    {
        GameObject ownSkillStorageObject = (GameObject)Instantiate(skillStoragePrefab.gameObject);
        ownSkillStorageObject.transform.parent = this.transform;
        ownSkillStorage = ownSkillStorageObject.GetComponent<PlayerSkillStorage>();
    }

    public virtual void useSkill(string skillId)
    {
        ownSkillStorage.useSkill(skillId, this.gameObject);
    }

    public virtual float getCooldownReamining(string skillId)
    {
        return ownSkillStorage.getCooldownReamining(skillId);
    }

    public virtual string getSelectedSkillId(int skillIndex)
    {
        List<string> selectedSkillIds = GameServiceLayer.serviceLayer.characterService.getSelectedSkills();
        string requestedSkillId;


        if (selectedSkillIds.Count > skillIndex)
        {
            requestedSkillId = selectedSkillIds [skillIndex];
        } else
        {
            requestedSkillId = null;
        }

        return requestedSkillId;
    }
}
