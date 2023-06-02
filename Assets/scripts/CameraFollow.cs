using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform targetFollow;
    [SerializeField] float speed = 1;

    [SerializeField] Vector3 lookAtOffset;
   

    [SerializeField] float upDistance = 1;
    [SerializeField] float distance = 5;


    private void Update()
    {
        Vector3 a = transform.position;
        //Posicion resultante de "posicion objetiva", "posición detrás del objetivo" y "posición arriba"
        Vector3 b = targetFollow.position - (targetFollow.forward * distance) + (Vector3.up * upDistance);
        transform.position = Vector3.Lerp(a, b, speed * Time.deltaTime);

        transform.LookAt(targetFollow.position + lookAtOffset);
    }
}
