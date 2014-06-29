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
        ownSkillStorageObject.transform.position = this.transform.position;
        ownSkillStorageObject.transform.rotation = this.transform.rotation;

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
         string requestedSkillId = GameServiceLayer.serviceLayer.characterService.getSelectedSkill(skillIndex);;

        if (requestedSkillId.Equals(""))
        {
            requestedSkillId = null;
        }

        return requestedSkillId;
    }
}
