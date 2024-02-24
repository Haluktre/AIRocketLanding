using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour
{
    // Start is called before the first frame update
    
    Rigidbody EngineRigidbody;
    public ParticleSystem TrustEffect;
    public float ThrustPower = 0;
    
    void Start()
    {
        EngineRigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(ThrustPower);
        EngineRigidbody.AddForce(-transform.up * ThrustPower * 15f);

        if(ThrustPower > 0.000f)
        {
            TrustEffect.Play();
        }
        else if(ThrustPower <= 0.000f)
        {
            TrustEffect.Stop();
            //ThrustPower = 0f;
        }
    }
}
