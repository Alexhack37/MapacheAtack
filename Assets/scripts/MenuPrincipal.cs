using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns.Observer.Interfaces;
using UnityEngine.SceneManagement;
using Patterns.Observer;

public class MenuPrincipal : MonoBehaviour, IObserver<float>
{
    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        ObserverBarriga observerBarriga = player.GetComponent<ObserverBarriga>();
        observerBarriga.AddObserver(this);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void UpdateObserver(float data)
    {
        Debug.Log("Funcion" + data);
        if (data == 3 && SceneManager.GetActiveScene().buildIndex==1) { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
            
         }
        if (data>=5 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (data >= 5 && SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void QuitGame()
    {
        Debug.Log("PA' FUERA");
        Application.Quit(); 
    }
}
