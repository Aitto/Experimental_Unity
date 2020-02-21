using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private Vector3 m_forward;
    [SerializeField]
    private float m_speed, m_acceleration, m_maxSpeed, m_deceleration, m_noInputDecelarate, m_rotation = 30;
    private Rigidbody m_car;

    // Start is called before the first frame update
    void Start()
    {
        m_forward = new Vector3(0,0,1);
        m_car = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position+= m_forward*Time.deltaTime*m_speed;
        //m_car.AddForce(m_forward*Time.deltaTime*m_speed);
        HandleInput();
    }

    private void HandleInput()
    {
        if( Input.GetKey(KeyCode.W))
        {
            m_speed+=m_acceleration*Time.deltaTime;
            if(m_speed > m_maxSpeed) m_speed = m_maxSpeed;
        }
        if( Input.GetKey(KeyCode.S) )
        {
            m_speed-=m_deceleration*Time.deltaTime;
            if( m_speed < -m_maxSpeed) m_speed = -m_maxSpeed;
        }
        if( Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0,1,0)*-m_rotation*Time.deltaTime);
            m_forward= transform.rotation.eulerAngles + new Vector3(1,0,0);
        }
        if( Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0,1,0)*m_rotation*Time.deltaTime);
            m_forward= transform.rotation.eulerAngles + new Vector3(1,0,0);
        }
        if(!Input.anyKey)
        {
            if(m_speed>0)
            {
                m_speed-=m_noInputDecelarate*Time.deltaTime;
                if(m_speed< 0) m_speed=0.0f;
            }
            if(m_speed<0) 
            {
                m_speed+=m_noInputDecelarate*Time.deltaTime;
                if(m_speed > 0) m_speed=0.0f;
            }
        }

    }
}
