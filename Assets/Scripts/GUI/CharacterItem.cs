using UnityEngine;
using System.Collections;
using System;

public class CharacterItem : MonoBehaviour
{

		public UILabel characterDesc;
		public UILabel characterPrice;
		public UILabel characterName;
		public GameObject buyCharacterObject;
		private PlayerCharacter playerCharacter;
		private IAPProduct characterProduct;

		public void setupCharacterItem (PlayerCharacter playerCharacter, IAPProduct characterProduct)
		{
				this.playerCharacter = playerCharacter;
				this.characterProduct = characterProduct;

				characterName.text = characterProduct.name;
				characterDesc.text = characterProduct.description;

				if (!characterProduct.purchased) {
						buyCharacterObject.SetActive (true);
						characterPrice.text = characterProduct.price.ToString ();
				}

		}

		public void setupCharacterItem (PlayerCharacter playerCharacter)
		{
				this.playerCharacter = playerCharacter;

		}

		public void showCHaracterInPreview ()
		{
				GameServiceLayer.serviceLayer.optionsService.setPreviewPlayerCharacter (playerCharacter);
		}

		public void onCharacterSelected ()
		{
				if (characterProduct != null && !characterProduct.purchased) {
						
						try {
								GameServiceLayer.serviceLayer.itemService.PurchaseCompleted += onCharacterPurchaseCompleted;
								GameServiceLayer.serviceLayer.itemService.buyIAPProduct (characterProduct.item_id);	
						} catch (NotEnoughTokensException e) {
								Debug.Log (e);
								unsubscribeForCharacterPurchaseCompleted ();
						} catch (Exception e) {
								unsubscribeForCharacterPurchaseCompleted ();
						}

				} else {
						GameServiceLayer.serviceLayer.optionsService.setSelectedPlayerCharacter (playerCharacter);
				}

				
		}

		void onCharacterPurchaseCompleted ()
		{
				unsubscribeForCharacterPurchaseCompleted ();
				GameServiceLayer.serviceLayer.optionsService.setSelectedPlayerCharacter (playerCharacter);
				buyCharacterObject.SetActive (false);
		}

		void unsubscribeForCharacterPurchaseCompleted ()
		{
				GameServiceLayer.serviceLayer.itemService.PurchaseCompleted -= onCharacterPurchaseCompleted;
		}
}
