using UnityEngine;
using System.Collections;

public class CharacterItem : MonoBehaviour
{

		public UILabel characterDesc;
		public UILabel characterPrice;
		public UILabel characterName;
		private PlayerCharacter playerCharacter;
		private IAPProduct characterProduct;

		public void setupCharacterItem (PlayerCharacter playerCharacter, IAPProduct characterProduct)
		{
				this.playerCharacter = playerCharacter;
				this.characterProduct = characterProduct;

				characterName.text = characterProduct.name;

				if (!characterProduct.purchased) {
						characterDesc.text = characterProduct.description;
						characterPrice.text = characterProduct.price.ToString ();
				}

		}

		public void setupCharacterItem (PlayerCharacter playerCharacter)
		{
				this.playerCharacter = playerCharacter;

		}

		public void onCharacterSelected ()
		{
		if (characterProduct != null && !characterProduct.purchased) {
						GameServiceLayer.serviceLayer.itemService.buyIAPProduct (characterProduct.item_id);
						GameServiceLayer.serviceLayer.itemService.PurchaseCompleted += onCharacterPurchaseCompleted;
				} else {
						GameServiceLayer.serviceLayer.optionsService.setSelectedPlayerCharacter (playerCharacter);
				}

				
		}

		void onCharacterPurchaseCompleted ()
		{
				GameServiceLayer.serviceLayer.optionsService.setSelectedPlayerCharacter (playerCharacter);
		}
	

}
