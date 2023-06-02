using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public enum State { Chase, Patroll}

    [SerializeField] State state = State.Patroll;

    EnemyStateMachine currentState;

    EnemyStateMachine patrollState;
    EnemyStateMachine chaseState;

    private void Awake()
    {
        patrollState = GetComponent<EnemyStatePatroll>();
        chaseState = GetComponent<EnemyStateChasing>();
    }

    private void Start()
    {
        patrollState.enabled = false;
        chaseState.enabled = false;

        ChangeState(state);
    }

    public void ChangeState(State state)
    {
        if (currentState != null)
            currentState.enabled = false;   //Desactivo el estado actual

        this.state = state;                 //Cambio la referencia visual
        currentState = HandleState(state);  //Asigno y ejecuto el estado desado

        currentState.enabled = true;        //Activo el scprit
    }

    EnemyStateMachine HandleState(State state)
    {
        switch (state)
        {
            case State.Patroll:
                return patrollState.GetAndRunState();
            case State.Chase:
                return chaseState.GetAndRunState();
            default: 
                return null;
        }
    }
}
