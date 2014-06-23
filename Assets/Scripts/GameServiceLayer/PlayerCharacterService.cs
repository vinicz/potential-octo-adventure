using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacterService : MonoBehaviour {

    public static string PLAYER_CHARACTER_NAME = "player_character";
    public static string PLAYER_SKIIL1_NAME = "player_skill1";
    public static string PLAYER_SKIIL2_NAME = "player_skill2";

    public delegate void SelectedCharacterChangedHandler();
    public event SelectedCharacterChangedHandler SelectedCharacterChanged;
    
    public delegate void PreviewCharacterChangedHandler();
    public event PreviewCharacterChangedHandler PreviewCharacterChanged;

    public PlayerCharacterStorage playerCharacterStorage;
    public PlayerSkillStorage playerSkillStoragePrefab;

    private PlayerSkillStorage playerSkillStorage;
    private string playerCharacterName;
    private PlayerCharacter playerCharacter;
    private PlayerCharacter previewCharacter;
    private List<string> selectedSkills;

    void Awake()
    {
        initPlayerCharacter ();     

        initSkillStorage();
    }


    public PlayerCharacter getSelectedPlayerCharacter()
    {
        return playerCharacter;
    }
    
    public void setSelectedPlayerCharacter(PlayerCharacter character)
    {
        playerCharacter = character;
        PlayerPrefs.SetString (PLAYER_CHARACTER_NAME, playerCharacter.productId);
        
        if (SelectedCharacterChanged != null) 
        {
            SelectedCharacterChanged();
        }
    }
    
    public PlayerCharacter getPreviewPlayerCharacter()
    {
        return previewCharacter;
    }
    
    public void setPreviewPlayerCharacter(PlayerCharacter character)
    {
        previewCharacter = character;
        
        if (PreviewCharacterChanged != null) 
        {
            PreviewCharacterChanged();
        }
    }
    
    public List<PlayerCharacter> getPossiblePlayerCharacters()
    {
        return playerCharacterStorage.getPlayerCharacters ();
    }

    public void setSelectedSkills(List<string> selectedSkills)
    {
        this.selectedSkills = selectedSkills;
    }
 
    public List<string> getSelectedSkills()
    {
        //return selectedSkills;
        return new List<string>{"skill_jump","skill_dash"};
    }

    public List<string> getPossibleSkills()
    {
        return playerSkillStorage.getPlayerSkillList();
    }

    void initPlayerCharacter ()
    {
        List<PlayerCharacter> characters = playerCharacterStorage.getPlayerCharacters ();
        string playerCharacterName = PlayerPrefs.GetString (PLAYER_CHARACTER_NAME, "");
        foreach (PlayerCharacter character in characters) {
            if (character.productId.Equals (playerCharacterName)) {
                playerCharacter = character;
                break;
            }
        }
    }

    void initSkillStorage()
    {
        GameObject ownSkillStorageObject = (GameObject)Instantiate(playerSkillStoragePrefab.gameObject);
        ownSkillStorageObject.transform.parent = this.transform;
        playerSkillStorage = ownSkillStorageObject.GetComponent<PlayerSkillStorage>();
    }
}
