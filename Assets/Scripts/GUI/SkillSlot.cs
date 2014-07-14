using UnityEngine;
using System.Collections;

public class SkillSlot : MonoBehaviour
{

    public SkillSelectWindow skillSelectWindow;
    public int skillIndex;
    public SkillIconsViewFactory skillIconsViewFactory;
    public UIButton skillSlotButton;

    void Start()
    {
        string selectedSkill = GameServiceLayer.serviceLayer.characterService.getSelectedSkill(skillIndex);

        if (!selectedSkill.Equals(""))
        {
            showSkill(selectedSkill);
        }

        skillSlotButton.isEnabled = false;

        skillSelectWindow.SelectedSkillChanged += onSelectedSkillChanged;
    }

    void OnClick()
    {
        SkillItem centeredSkillItem = skillSelectWindow.getSelectedSkillItem();
		
        if(centeredSkillItem!=null && centeredSkillItem.skillProduct.purchased)
		{
            selectCenteredSkill();
        }

    }
	
    void selectCenteredSkill()
    {
        SkillItem centeredSkillItem = skillSelectWindow.getSelectedSkillItem();
        string skillId = centeredSkillItem.skillProduct.item_id;
        GameServiceLayer.serviceLayer.characterService.setSelectedSkill(skillId, skillIndex);
        showSkill(skillId);
    }

    void showSkill(string skillId)
    {
      
        skillIconsViewFactory.getSkillIconsView().showSkillIcon(skillId);
    }
	
    void onSelectedSkillChanged()
    {
        SkillItem centeredSkillItem = skillSelectWindow.getSelectedSkillItem();

        if (centeredSkillItem.skillProduct.purchased)
        {
            skillSlotButton.isEnabled = true;
        } else
        {
            skillSlotButton.isEnabled = false;
        }
    }
}
