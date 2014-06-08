using UnityEngine;
using System.Collections;

public class DiamondMasterScript : GameHandlerScript
{

		public delegate void CollectedDiamondCountChangedHandler ();

		public event CollectedDiamondCountChangedHandler CollectedDiamondCountChanged;
	
		public delegate void KilledEnemyCountChangedHandler ();

		public event KilledEnemyCountChangedHandler KilledEnemyCountChanged;
	
		public int enemyCount;
		const string DIAMOND_TAG_NAME = "Diamond";
		protected int requiredDiamondCount;
		protected int collectedDiamondCount;
	
		protected override void Start ()
		{
				base.Start ();
				requiredDiamondCount = GameObject.FindGameObjectsWithTag (DIAMOND_TAG_NAME).Length;	
				collectedDiamondCount = 0;

		}

		public virtual void killOneEnemy (GameObject enemy)
		{
				enemy.SetActive (false);
				enemyCount--;
		
				if (KilledEnemyCountChanged != null) {
						KilledEnemyCountChanged ();
				}
		}
	
		public virtual void collectOneDiamond (GameObject diamond)
		{
		
				collectedDiamondCount++;
				diamond.GetComponent<DiamondActivator> ().DeactivateDiamond ();
		
				if (CollectedDiamondCountChanged != null) {
						CollectedDiamondCountChanged ();
				}

		}

		protected override GameModeLogic createGameModeLogic ()
		{
				return DiamondModeLogicFactory.createGameModeLogic (this, levelRecord);
		}

		public int getCollectedDiamonds ()
		{
				return collectedDiamondCount;
		}
	
		public int getRequiredDiamondCount ()
		{
				return requiredDiamondCount;
		}

		public int getEnemyCount ()
		{
				return enemyCount;
		}
	
    
  
}
