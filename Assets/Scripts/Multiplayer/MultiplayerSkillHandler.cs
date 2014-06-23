using UnityEngine;
using System.Collections;

public class MultiplayerSkillHandler : PlayerSkillHandler {

    public override void useSkill(string skillId)
    {
        networkView.RPC("useSkillRemote", RPCMode.All, skillId);
    }

    [RPC]
    public void useSkillRemote(string skillId)
    {
        base.useSkill(skillId);
    }
}
