﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectWindow : MonoBehaviour
{

		public UIGrid characterGrid;
		public GameObject characterItem;

		void Start ()
		{
			
				IEnumerable<IAPProduct> purchasableCharacters = 
						GameServiceLayer.serviceLayer.itemService.getIAPProductsOfType (IAPProduct.ProductType.CHARACTER);
				List<PlayerCharacter> playerCharacters = 
						GameServiceLayer.serviceLayer.optionsService.getPossiblePlayerCharacters ();


				foreach (PlayerCharacter character in playerCharacters) {

						
						if (character.productId.Equals ("")) {
								createCharacterItem (character);
						}

						foreach (IAPProduct characterProduct in purchasableCharacters) {
								if (character.productId.Equals (characterProduct.item_id)) {
									
										createCharacterItem (character, characterProduct);
										break;
								}

						}
					
				}
				
				characterGrid.Reposition ();


		}

		GameObject createCharacterItemView (GameObject parent)
		{
				GameObject newCharacterItem = NGUITools.AddChild (parent, characterItem);
				newCharacterItem.transform.localPosition = characterItem.transform.position;
				newCharacterItem.transform.localRotation = characterItem.transform.rotation;
				newCharacterItem.transform.localScale = characterItem.transform.localScale;
				return newCharacterItem;
		}

		void createCharacterItem (PlayerCharacter playerCharacter, IAPProduct characterProduct)
		{
				CharacterItem newCharacterItem = createCharacterItemObject ();
				newCharacterItem.setupCharacterItem (playerCharacter, characterProduct);
		}

		void createCharacterItem (PlayerCharacter playerCharacter)
		{
				CharacterItem newCharacterItem = createCharacterItemObject ();
				newCharacterItem.setupCharacterItem (playerCharacter);
		}

		CharacterItem createCharacterItemObject ()
		{
				GameObject newCharacterItemObject = createCharacterItemView (characterGrid.gameObject);
				CharacterItem newCharacterItem = newCharacterItemObject.GetComponent<CharacterItem> ();
				return newCharacterItem;
		}
}