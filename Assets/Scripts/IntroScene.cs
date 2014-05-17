using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour
{
	public GameObject spotlight1;
	public GameObject spotlight2;
	public GameObject spotlight3;
	public GameObject spotlight4;

	public GameObject muddictiveLogo;
	public GameObject logoSound;

	private bool animationsOver = false;

    void Start()
    {
		//guiTexture.color = new Color(guiTexture.color.r, guiTexture.color.g, guiTexture.color.b, 0);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
		StartCoroutine (PlayAnimations ());
	}
	
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

		if (Input.touchCount > 0)
			animationsOver = true;

		if (animationsOver)
		{
			Application.LoadLevel(2);
		}
    }

	public IEnumerator PlayAnimations()
	{
		yield return new WaitForSeconds(0.5f);
		spotlight1.SetActive (true);

		yield return new WaitForSeconds(1f);
		spotlight2.SetActive (true);

		yield return new WaitForSeconds(1.5f);
		spotlight3.SetActive (true);

		yield return new WaitForSeconds(0.5f);
		spotlight4.SetActive (true);

		yield return new WaitForSeconds(2);
		muddictiveLogo.SetActive (true);
		logoSound.SetActive (true);

		yield return new WaitForSeconds(2.5f);
		
		//yield return new WaitForSeconds(0.5f);

		animationsOver = true;
	}
}
