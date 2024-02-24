using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using DG.Tweening;

public class RocketAgent : Agent
{
    // Start is called before the first frame update
    EnvironmentParameters m_ResetParams;
    public Rigidbody HorizontalEngine0, HorizontalEngine1, HorizontalEngine2, HorizontalEngine3, VerticalEngine;
    public Transform Leg0, Leg1, Leg2, Leg3;
    public GameObject Plane;
    Rigidbody Rocket;
    float TargetRotation = 270f;
    float DistanceTarget;
    public ParticleSystem LandingSmoke;
    //float timeRemaining = 0;

    void Start()
    {
        Rocket = GetComponent<Rigidbody>();

    }

    public override void Initialize()
    {
        //Debug.Log("Initialize");
        m_ResetParams = Academy.Instance.EnvironmentParameters;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
        //Debug.Log("CollectObservations");
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.eulerAngles);
        sensor.AddObservation(Plane.transform.position);
        sensor.AddObservation(DistanceTarget);
        
        sensor.AddObservation(-VerticalEngine.velocity.y);
        sensor.AddObservation(-HorizontalEngine0.velocity.y);
        sensor.AddObservation(-HorizontalEngine1.velocity.y);
        sensor.AddObservation(-HorizontalEngine2.velocity.y);
        sensor.AddObservation(-HorizontalEngine3.velocity.y);
        

    }
    public override void OnEpisodeBegin()
    {
        //Debug.Log("OnEpisodeBegin");
        VerticalEngine.velocity = Vector3.zero;
        HorizontalEngine0.velocity = Vector3.zero;
        HorizontalEngine1.velocity = Vector3.zero;
        HorizontalEngine2.velocity = Vector3.zero;
        HorizontalEngine3.velocity = Vector3.zero;
        Rocket.velocity = Vector3.zero;
        
        VerticalEngine.angularVelocity = Vector3.zero;
        HorizontalEngine0.angularVelocity = Vector3.zero;
        HorizontalEngine1.angularVelocity = Vector3.zero;
        HorizontalEngine2.angularVelocity = Vector3.zero;
        HorizontalEngine3.angularVelocity = Vector3.zero;
        Rocket.angularVelocity = Vector3.zero;
        

        transform.position = new Vector3(401.5f,562f,848.5f);
        
        transform.eulerAngles = new Vector3(Random.Range(290f,325f),Random.Range(0f,360f),Random.Range(0f,360f));
        //Debug.Log(Rocket.angularVelocity);
        
    }
    
    
    private void OnCollisionEnter(Collision other) {
        //EndEpisode();
        /*
        if(other.gameObject == Plane){
            
        }
        Debug.Log(other.gameObject);*/
    }
        
    
    

    public override void OnActionReceived(ActionBuffers actions)
    {
        
        VerticalEngine.GetComponent<RocketEngine>().ThrustPower = (actions.ContinuousActions[0] + 1)/50;
        HorizontalEngine0.GetComponent<RocketEngine>().ThrustPower = actions.ContinuousActions[1] + 1;
        HorizontalEngine1.GetComponent<RocketEngine>().ThrustPower = actions.ContinuousActions[2] + 1;
        HorizontalEngine2.GetComponent<RocketEngine>().ThrustPower = actions.ContinuousActions[3] + 1;
        HorizontalEngine3.GetComponent<RocketEngine>().ThrustPower = actions.ContinuousActions[4] + 1;
        

        
        DistanceTarget = TargetRotation - transform.eulerAngles.x;
        Debug.Log(transform.eulerAngles.x);

        
        float DistanceToPlane = Vector3.Distance(transform.position,Plane.transform.position);
        Debug.Log(DistanceToPlane);
        if(DistanceToPlane < 100){
            LandingSmoke.Play();
            LandingGear();
        }
        

        if(Mathf.Abs(DistanceTarget) != 0.000f){
            AddReward(10f / Mathf.Abs(DistanceTarget));
        }
        else if(Mathf.Abs(DistanceTarget) == 0.000f){
            AddReward(15f);
        }
        
        if(Mathf.Abs(DistanceTarget) > 90f){
            SetReward(-500f);
            
            EndEpisode();
        }

       
        void LandingGear(){

        Leg0.DOLocalRotate(new Vector3(317.301025f,230.738922f,208.999023f),3f);
        Leg1.DOLocalRotate(new Vector3(34.2299004f,234.17659f,157.900482f),3f);
        Leg2.DOLocalRotate(new Vector3(39.5407562f,124.357597f,23.5194263f),3f);
        Leg3.DOLocalRotate(new Vector3(321.136414f,126.307587f,335.247955f),3f);
        
       }
       
    }       
}