using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateChasing : EnemyStateMachine
{

    [SerializeField] Transform target;
    [SerializeField] float speed = 25.0f;
    public override EnemyStateMachine GetAndRunState()
    {
        base.GetAndRunState();
        return this;
    }
    
    private void Update()
    {
        //Debug.Log("Al ataque!!!");
        if (target == null){
            myController.ChangeState(EnemyStateController.State.Patroll);
        }else{
            transform.position += (target.position - transform.position).normalized * Time.deltaTime * speed;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            myController.ChangeState(EnemyStateController.State.Patroll);
        //Debug.Log((target.position - transform.position).normalized);
    }
    /*
    private void OnTriggerEnter(Collider other) { // me matas
        //Debug.Log("te veo");
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }*/

    
    private void OnCollisionEnter(Collision collision) { // me matas
        Debug.Log("te toco");
        IDamageable target = collision.gameObject.GetComponent<IDamageable>();
        if (target != null)
        {
            target.TakeDmg(100.0f);
        }else{
            myController.ChangeState(EnemyStateController.State.Patroll);
        }
    }
    
    private void OnTriggerExit(Collider other) { //salimos de este estado
        if (other.tag == "Player")
        {
            myController.ChangeState(EnemyStateController.State.Patroll);
        }
        //transform.position = new Vector3(0,0,2) * Time.deltaTime;
    }
}
