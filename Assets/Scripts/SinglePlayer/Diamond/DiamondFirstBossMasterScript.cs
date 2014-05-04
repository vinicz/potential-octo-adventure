using UnityEngine;
using System.Collections;

public class DiamondFirstBossMasterScript : DiamondMasterScript {

    public GameObject spawnItem;
    public GameObject spawnPosition;
    public float startGameTime = 5f;

    protected override void Start()
    {
        base.Start();

        setGameState(GameState.INTRO);
    }

    protected override void Update()
    {
        base.Update();

        if (gameState == GameState.INTRO)
        {
       
            if (startGameTime <= 0)
            {
                setGameState(GameState.PREGAME);
            } else
            {
                startGameTime -= Time.deltaTime;
            }
        }
    }

    public override void collectOneDiamond(GameObject diamond)
    {

        base.collectOneDiamond(diamond);
        GameObject newDiamond = (GameObject)Instantiate(spawnItem);
        newDiamond.transform.position = spawnPosition.transform.position;

        if (collectedDiamondCount==requiredDiamondCount)
        {
            newDiamond.rigidbody.mass=1000;
        }
    }

    
}
