using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private Vector3 m_forward,m_up,m_right;
    [SerializeField]
    private float m_speed, m_acceleration, m_maxSpeed, m_deceleration, m_noInputDecelarate, m_rotation = 30;
    private Rigidbody m_car;

    // Start is called before the first frame update
    void Start()
    {
        m_forward = new Vector3(0,0,1);
        m_up = new Vector3(0,1,0);
        m_right = Vector3.Cross(m_up,m_forward);
        m_right.Normalize();
        Debug.Log("Right vector: "+m_right);
        m_car = GetComponent<Rigidbody>();
        Debug.Log("Cos: "+Mathf.Cos(0*Mathf.PI/180.0f) + " Sin: "+ Mathf.Sin(90*Mathf.PI/180.0f));
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position+= m_forward*Time.deltaTime*m_speed;
        //m_car.AddForce(m_forward*Time.deltaTime*m_speed);
        HandleInput();
    }

    private void HandleInput()
    {
        
        AccelerateCar();
        //RotateCar();
        DebugCode();
        CapSpeed();

    }

    private void AccelerateCar()
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
    }

    private void RotateCar()
    {
        if( Input.GetKey(KeyCode.A))
        {
            //Rotate wrt (0,1,0) -m_rotation*Time.deltatiel angle
            transform.Rotate(new Vector3(0,1,0),-m_rotation*Time.deltaTime);
            //Rotate wrt y axis
            m_forward= m_forward*Mathf.Cos(-m_rotation*Time.deltaTime*Mathf.PI/180) +m_right*Mathf.Sin(-m_rotation*Time.deltaTime*Mathf.PI/180);
            m_forward.Normalize();
            //Calculate right vector
            m_right = Vector3.Cross(m_up,m_forward);
            m_right.Normalize();
        }
        if( Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0,1,0),m_rotation*Time.deltaTime);
            m_forward= m_forward*Mathf.Cos(-m_rotation*Time.deltaTime*Mathf.PI/180) - m_right*Mathf.Sin(-m_rotation*Time.deltaTime*Mathf.PI/180);
            m_forward.Normalize();
            //Calculate left vector
            m_right = Vector3.Cross(m_up,m_forward);
            m_right.Normalize();
        }
    }
    private void CapSpeed()
    {
        //Descelerate when W or S is not pressed
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
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

    private void DebugCode()
    {
        if(Input.GetKey(KeyCode.X))
        {
            Debug.Log("Forward: "+m_forward);
            Debug.Log("Right: "+m_right);
            Debug.Log("Up: "+m_up);
        }
    }
    
    public float GetCarVelocity()
    {
        return m_speed;
    }
}
