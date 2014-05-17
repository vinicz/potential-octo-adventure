using UnityEngine;
using System.Collections;

public class TransparencyController : MonoBehaviour {

    public Material transparentMaterial;
    public float transparencyExpireTIme = 1f;

    private Material normalMaterial;
    private float transparencyTimer = 0f;
    private bool transparent;

    void Start()
    {
        normalMaterial = this.renderer.material;
    }


    void Update()
    {
        if (transparent)
        {
            transparencyTimer-= Time.deltaTime;

            if(transparencyTimer<=0)
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
        this.renderer.material = transparentMaterial;
    }
}
