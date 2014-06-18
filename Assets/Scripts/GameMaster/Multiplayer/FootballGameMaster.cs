using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FootballGameMaster : MultiGameMaster {

	protected override int determineRandomTeam ()
	{
		return 1;
	}
	
	public override List<int> getPossibleTeams ()
	{

		return new List<int> { 1,2 };
	}

	protected override GameModeLogic createGameModeLogic ()
	{
		return new NormalFootballGameModeLogic();
		
	}
}
