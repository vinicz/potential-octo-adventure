﻿using UnityEngine;
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
		StartCoroutine (PlayAnimations ());
	}
	
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

		if (animationsOver)
		{
			Application.LoadLevel(1);
		}
    }

	public IEnumerator PlayAnimations()
	{
		yield return new WaitForSeconds(1.5f);
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

		yield return new WaitForSeconds(2f);

		Debug.Log ("switch off lights");
		logoSound.SetActive (false);
		logoSound.SetActive (true);

		spotlight1.SetActive (false);
		spotlight2.SetActive (false);
		spotlight3.SetActive (false);
		spotlight4.SetActive (false);
		muddictiveLogo.SetActive (false);
		
		yield return new WaitForSeconds(0.5f);

		animationsOver = true;
	}
}
