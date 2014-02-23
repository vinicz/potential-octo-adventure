using UnityEngine;
using System.Collections;

public class ShockWaveScript : MonoBehaviour
{


    public float shockWaveForce;
    public float shockDistance =1f;

    public int shockWaveCooldown=10;

    private float _shockWaveCooldownRemaining=0;


    // Use this for initialization
    void Start()
    {
        particleSystem.Stop();
    
    }

    public void createShockWave()
    {
        if (_shockWaveCooldownRemaining == 0)
        {
            particleSystem.Play();

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, shockDistance);
            foreach (var shockHit in hitColliders)
            {
                
                if (shockHit && shockHit.rigidbody)
                    shockHit.rigidbody.AddExplosionForce(shockWaveForce, transform.position, shockDistance, 3.0F);
            }
            _shockWaveCooldownRemaining = shockWaveCooldown;
        }


    }
    
    // Update is called once per frame
    void Update()
    {
        if (_shockWaveCooldownRemaining > 0)
        {
            _shockWaveCooldownRemaining -= Time.deltaTime;
        } else
        {
            _shockWaveCooldownRemaining=0;
        }

    }

    public float ShockWaveCooldownRemaining
    {
        get
        {
            return _shockWaveCooldownRemaining;
        }
        set
        {
            _shockWaveCooldownRemaining = value;
        }
    }
}
