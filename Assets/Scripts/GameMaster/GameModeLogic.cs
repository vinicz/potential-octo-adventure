

public interface GameModeLogic
{
    void update();
    void onDiamondCollected();
    void onEnemyKilled();
    int calculateReward();

}


