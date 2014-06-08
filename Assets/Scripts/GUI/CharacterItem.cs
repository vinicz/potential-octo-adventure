using UnityEngine;
using System.Collections;
using System;

public class CharacterItem : MonoBehaviour
{

		public UILabel characterDesc;
		public UILabel characterPrice;
		public UILabel characterName;
		public GameObject buyCharacterObject;
		public UIButton characterButton;
		private PlayerCharacter playerCharacter;
		private IAPProduct characterProduct;
		private GameObject currentWindow;
		private GameObject targetWindow;

		public void setupCharacterItem (PlayerCharacter playerCharacter, IAPProduct characterProduct, GameObject currentWindow, GameObject targetWindow)
		{
				this.playerCharacter = playerCharacter;
				this.characterProduct = characterProduct;
				this.targetWindow = targetWindow;
				this.currentWindow = currentWindow;

				characterName.text = characterProduct.name;
				characterDesc.text = characterProduct.description;

				if (!characterProduct.purchased) {
						buyCharacterObject.SetActive (true);
						characterPrice.text = characterProduct.price.ToString ();
						
						setupLabels ();
				}

				GameServiceLayer.serviceLayer.itemService.ItemCountChanged += setupLabels;

		}

		public void setupCharacterItem (PlayerCharacter playerCharacter, GameObject currentWindow, GameObject targetWindow)
		{
				this.playerCharacter = playerCharacter;
				this.targetWindow = targetWindow;
				this.currentWindow = currentWindow;
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
						selectCharacter ();
				}

				
		}

		void onCharacterPurchaseCompleted ()
		{
				unsubscribeForCharacterPurchaseCompleted ();
				buyCharacterObject.SetActive (false);
				selectCharacter ();
		}

		void unsubscribeForCharacterPurchaseCompleted ()
		{
				GameServiceLayer.serviceLayer.itemService.PurchaseCompleted -= onCharacterPurchaseCompleted;
		}

		void selectCharacter ()
		{
				GameServiceLayer.serviceLayer.optionsService.setSelectedPlayerCharacter (playerCharacter);
				currentWindow.SetActive (false);
				targetWindow.SetActive (true);
		}

		void setupLabels ()
		{
				if (GameServiceLayer.serviceLayer.itemService.getTokenCount () < characterProduct.price) {
						characterName.color = Color.red;
						characterDesc.color = Color.red;
						characterPrice.color = Color.red;
						characterButton.isEnabled = false;
				} else {

						characterName.color = Color.white;
						characterDesc.color = Color.white;
						characterPrice.color = Color.white;
						characterButton.isEnabled = true;
				}
		}

		void OnDestroy ()
		{
				GameServiceLayer.serviceLayer.itemService.ItemCountChanged -= setupLabels;
		}
}
