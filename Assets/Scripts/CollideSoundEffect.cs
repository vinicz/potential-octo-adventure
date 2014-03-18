using UnityEngine;
using System.Collections;

public class CollideSoundEffect : MonoBehaviour
{

    public float effectTreshold = 4f;
    public float rechargeTime = 1f;
    public AudioSource soundEffect;

    private float remainingRechargeTime = 0f;

    void OnCollisionEnter(Collision collision)
    {

        if (remainingRechargeTime <= 0 && collision.relativeVelocity.magnitude > effectTreshold)
        {
            soundEffect.Play();
            remainingRechargeTime = rechargeTime;
        }
        
    }

    void Update()
    {
        if (remainingRechargeTime > 0)
        {
            remainingRechargeTime -= Time.deltaTime;
        }

    }
}
