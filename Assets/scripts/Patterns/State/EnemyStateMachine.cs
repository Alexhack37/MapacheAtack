using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] float radius = 2.3f;
    [SerializeField] protected EnemyStateController myController;
    protected SphereCollider detector;
    
    public virtual EnemyStateMachine GetAndRunState(){
        detector.radius = radius;
        return this;   
    }

    private void Awake(){
        detector = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        
    }

}
