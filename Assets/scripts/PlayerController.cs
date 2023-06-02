using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    protected CharacterController myCharacter;
    // Start is called before the first frame update
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private float yVelocity = 1.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    private List<Collider> m_collisions = new List<Collider>();

    private readonly float m_interpolation = 10;
    private float m_currentV = 0;
    private float m_currentH = 0;
    private Vector3 m_currentDirection = Vector3.zero;
    private bool m_isGrounded;
    //Quaternion currentRotation{get private set;}

    void Start()
    {
        myCharacter = GetComponent<CharacterController>();
        
        //obtengo el componente asignado del objeto
    }
    void Update()
    {
        if(myCharacter.isGrounded){
            if(Input.GetKeyDown(KeyCode.Space)){
                yVelocity = jumpHeight;
            }
        }else{
            yVelocity -= gravity;
        }
        Vector3 velocity = m_currentDirection * speed;
        velocity.y = yVelocity;
        myCharacter.Move(velocity * Time.deltaTime);
        
        //actualemtnte robado del muñeco de assets
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Transform camera = Camera.main.transform;

        m_currentV = Mathf.Lerp(m_currentV, verticalInput, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, horizontalInput, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;


        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * speed * Time.deltaTime;

            //m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }
}
