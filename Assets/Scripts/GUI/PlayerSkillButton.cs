using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkillButton : SkillButton
{
    public int skillIndex;

    private List<PlayerSpawner> playerSpawners;
    private PlayerSkillHandler currentSkillHandler;

    protected override string getCurrentSkill()
    {
        playerSpawners = GameServiceLayer.serviceLayer.playerSpawnerList;
        if (playerSpawners.Count > 0)
        {
            GameObject onePlayerObject = playerSpawners [0].getPlayerObject();
            currentSkillHandler = onePlayerObject.GetComponent<PlayerSkillHandler>();

            return currentSkillHandler.getSelectedSkillId(skillIndex);
        } else
        {
            return null;
        }
    }

    protected override float getCoolDownRemaining()
    {
        return currentSkillHandler.getCooldownReamining(currentSkillId);
    }

    protected override void useSkill()
    {
        foreach (PlayerSpawner spawner in playerSpawners)
        {
            GameObject onePlayerObject = spawner.getPlayerObject();
            PlayerSkillHandler playerSkillHandler = onePlayerObject.GetComponent<PlayerSkillHandler>();

            if (playerSkillHandler != null)
            {
                playerSkillHandler.useSkill(currentSkillId);
            }
        }

    }
}
