using UnityEngine;
using System.Collections;

public class GameServiceLayer : MonoBehaviour {


    public static GameServiceLayer serviceLayer;

    public GameHandlerScript gameMaster;
    public LevelService levelService;
    public ItemService itemService;
    public OptionsService optionsService;
    public BackgroundMusicPlayer backgroundMusicPlayer;

	void Awake () {

        if (serviceLayer == null)
        {
            DontDestroyOnLoad(gameObject);
            serviceLayer = this;
        } else if (serviceLayer != this)
        {
            Destroy(gameObject);
        }
	
	}
}
