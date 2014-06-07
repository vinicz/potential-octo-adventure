﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialSkillButton : SkillButton
{

		private List<PlayerSpawner> playerSpawners;

		protected override Skill getCurrentSkill ()
		{
				playerSpawners = GameServiceLayer.serviceLayer.playerSpawnerList;
				GameObject onePlayerObject = playerSpawners [0].getPlayerObject ();
				Skill currentSkill = onePlayerObject.GetComponent<Skill> ();

				return currentSkill;
		}

		protected override void useSkill ()
		{
				foreach (PlayerSpawner spawner in playerSpawners) {
						GameObject onePlayerObject = spawner.getPlayerObject ();
						Skill currentSkill = onePlayerObject.GetComponent<Skill> ();

						if (currentSkill != null) {
								currentSkill.useSkill ();
						}
				}

		}
}
