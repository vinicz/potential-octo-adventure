using UnityEngine;
using System.Collections;

public class TransparencyController : MonoBehaviour {

    public Material transparentMaterial;
    public float transparencyExpireTime = 1f;
	public bool changeBack = true;

    private Material normalMaterial;
    private float transparencyTimer = 0f;
    private bool transparent;

    void Start()
    {
        normalMaterial = this.renderer.material;
    }


    void Update()
    {
        if (transparent && changeBack)
        {
            transparencyTimer -= Time.deltaTime;

            if (transparencyTimer <= 0)
            {
                makeVisible();
            }
        }
    }
        

    public void makeVisible()
    {
        transparent = false;
        this.renderer.material = normalMaterial;
    }

    public void makeTransparent()
    {
        transparent = true;
        transparencyTimer = transparencyExpireTime;
        this.renderer.material = transparentMaterial;
    }
}
