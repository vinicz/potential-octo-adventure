using UnityEngine;
using System.Collections;

public class PlayerCharacterPreview : MonoBehaviour
{

		private GameObject playerCharacter;
		private GameObject playerCharacterPreview;

		void Start ()
		{
				PlayerCharacter playerCharacterStruct = GameServiceLayer.serviceLayer.optionsService.getSelectedPlayerCharacter ();

				if (playerCharacterStruct != null) {
						playerCharacter = playerCharacterStruct.playerCharacter;
						createCharacterPreview ();
				}

				GameServiceLayer.serviceLayer.optionsService.SelectedCharacterChanged += onSelectedCharacterChanged;
				GameServiceLayer.serviceLayer.optionsService.PreviewCharacterChanged += onPreviewCharacterChanged;
		}

		void OnDestroy ()
		{
				GameServiceLayer.serviceLayer.optionsService.SelectedCharacterChanged -= onSelectedCharacterChanged;
				GameServiceLayer.serviceLayer.optionsService.PreviewCharacterChanged -= onPreviewCharacterChanged;
		}

		void onSelectedCharacterChanged ()
		{
				GameObject newPlayerCharacter = GameServiceLayer.serviceLayer.optionsService.getSelectedPlayerCharacter ().playerCharacter;

				showNewCharacter (newPlayerCharacter);
		}

		void onPreviewCharacterChanged ()
		{
				GameObject newPlayerCharacter = GameServiceLayer.serviceLayer.optionsService.getPreviewPlayerCharacter ().playerCharacter;
		
				showNewCharacter (newPlayerCharacter);
		}

		void createCharacterPreview ()
		{
				playerCharacterPreview = (GameObject)Instantiate (playerCharacter);
				playerCharacterPreview.transform.parent = this.transform;
				playerCharacterPreview.transform.localPosition = Vector3.zero;
				playerCharacterPreview.transform.rotation = Quaternion.identity;
				playerCharacterPreview.transform.localScale = new Vector3 (8f, 8f, 8f);
				playerCharacterPreview.rigidbody.constraints = RigidbodyConstraints.FreezePosition;

				AccelometerController controller = playerCharacterPreview.GetComponent<AccelometerController> ();
				controller.enabled = false;
		}
		
		void showNewCharacter (GameObject newPlayerCharacter)
		{
				if (newPlayerCharacter != playerCharacter) {
						playerCharacter = newPlayerCharacter;
						Destroy (playerCharacterPreview);
						createCharacterPreview ();
				}
		}
}
