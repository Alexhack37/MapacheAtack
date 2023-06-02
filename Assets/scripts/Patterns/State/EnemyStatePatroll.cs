using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyStatePatroll : EnemyStateMachine
{
    //codigo de patrullar https://medium.com/geekculture/how-to-make-a-basic-patrolling-system-in-unity-c-3bec0cf63478
    //con bastantes cambios mios
    public Transform[] waypoints;
    private Vector3[] vectorPoints;
    private Vector3 currentPonts;
    private int _currentWaypointIndex = 0;
    [SerializeField] private float _speed = 30f; //esto es mio

    public override EnemyStateMachine GetAndRunState() 
    {
        base.GetAndRunState();
        return this;
    }
    public void Start(){ //esto es mio
        vectorPoints = new Vector3 [waypoints.Length];
        
        for(int i = 0; i < waypoints.Length; i++){
            vectorPoints[i] = waypoints[i].position;
        }
        for(int i = 0; i < waypoints.Length; i++){
            waypoints[i].parent = null;
        }
        currentPonts = vectorPoints[0];
    }
    private void Update()
    {
        //Debug.Log("Estoy patrullando");

        if (Input.GetKeyDown(KeyCode.Escape))
            myController.ChangeState(EnemyStateController.State.Chase); //esto es mio

        //Transform wp = waypoints[_currentWaypointIndex];
        
        if (Vector3.Distance(transform.position, currentPonts) < 0.01f)
        {
            //_currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            ChangeWaypoints();
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                currentPonts,
                _speed * Time.deltaTime);
            
            //transform.position += (currentPonts - transform.position).normalized * Time.deltaTime * _speed;
        }
    }
    private void ChangeWaypoints(){ //esto es mio
        _currentWaypointIndex++;
        if(_currentWaypointIndex >= vectorPoints.Length){
            _currentWaypointIndex = 0;
        }
        currentPonts = vectorPoints[_currentWaypointIndex];
        
    }

    private void OnTriggerEnter(Collider other) { //esto es mio
        //Debug.Log("te veo");
        if (other.tag == "Player")
        {
            myController.ChangeState(EnemyStateController.State.Chase);
        }
    }
}
