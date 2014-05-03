using UnityEngine;
using System.Collections;

public class TouchRotator : MonoBehaviour {
   
    private Vector2 switeStartPosition;
    private Quaternion initialOrinetation;
	
    void Start()
    {

        initialOrinetation = this.gameObject.transform.localRotation;
    }


	void Update () {

        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);



            if(firstTouch.phase == TouchPhase.Began)
            {
                initialOrinetation = this.gameObject.transform.localRotation;
                switeStartPosition = firstTouch.position;
            }else if(firstTouch.phase == TouchPhase.Moved)
            {
                Quaternion rotateX = Quaternion.Euler(new Vector3(firstTouch.position.y/2f,0, firstTouch.position.x/2f));
                
                this.gameObject.transform.localRotation = rotateX *initialOrinetation;
            }
            else if(firstTouch.phase == TouchPhase.Ended)
            {
                this.gameObject.transform.localRotation = initialOrinetation;
            }
        }

	}
}
