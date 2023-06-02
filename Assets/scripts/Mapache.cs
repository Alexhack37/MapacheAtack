using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapache : SimpleSampleCharacterControl, IDamageable
{
    // Mapache es el jugador 
    [SerializeField] private float life = 100f;

    public void TakeDmg(float damage){
        life -= damage;
        if(life <= 0.0f){
            Death();
        }
    }
    public void Death(){
        //Destroy(gameObject);
        //tepeo spawn
        Debug.Log("destruir");
        //Transform respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        //transform.position = respawn.position;
        
        //myCharacter.enabled = false;



        Transform respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        transform.position = respawn.position;

        //myCharacter.enabled = true;
    }   
    // Update is called once per frame
}
