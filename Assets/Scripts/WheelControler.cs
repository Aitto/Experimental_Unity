using UnityEngine;

public class WheelControler : MonoBehaviour
{
    [SerializeField]
    private CarController cc;
    [SerializeField]
    private GameObject[] m_frontTiers;
    [SerializeField]
    private float m_maxRotate = 35.0f;
    private float m_speed;
    private Vector3 m_right,m_up,m_forward;
    private bool m_isFrontTier;

    public int target = 30;
      
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_right = new Vector3(1,0,0);   //x axis
        m_up = new Vector3(0,1,0);  //y axis
        m_forward = new Vector3(0,0,1); //z axis
        m_isFrontTier = false;
        for(int i=0;i<m_frontTiers.Length;i++)
        {
            if(this.gameObject.name == m_frontTiers[i].gameObject.name)
            {
                m_isFrontTier = true;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpeedRotation();
        if(m_isFrontTier)
        {
            SteeringRotation();
        }
        Debug.Log("Forward : "+m_forward);
        Debug.Log("Right : "+m_right);
        Debug.Log("Up : "+m_up);
    }

    private void SpeedRotation()
    {
        if(cc!=null)
        {
            m_speed = cc.GetCarVelocity();
            
        }else m_speed = 0;
        this.transform.Rotate(m_right,m_speed*Time.deltaTime*360,Space.World);
        //fix forward vect
        m_forward = Vector3.Cross(m_right,m_up);
        m_forward.Normalize();
    }

    private void SteeringRotation()
    {
        float angle =this.transform.rotation.eulerAngles.y;
        if( angle >= (360-m_maxRotate) || angle <= m_maxRotate )
        {
            if(Input.GetKey(KeyCode.A))
            {
                this.transform.Rotate(m_up,-m_maxRotate*Time.deltaTime,Space.World);
                m_forward= m_forward*Mathf.Cos(-m_maxRotate*Time.deltaTime*Mathf.PI/180) + m_right*Mathf.Sin(-m_maxRotate*Time.deltaTime*Mathf.PI/180);
                m_forward.Normalize();
                //Calculate right vector
                m_right = Vector3.Cross(m_up,m_forward);
                m_right.Normalize();
            }
            if(Input.GetKey(KeyCode.D))
            {
                this.transform.Rotate(m_up,m_maxRotate*Time.deltaTime,Space.World);
                m_forward= m_forward*Mathf.Cos(-m_maxRotate*Time.deltaTime*Mathf.PI/180) - m_right*Mathf.Sin(-m_maxRotate*Time.deltaTime*Mathf.PI/180);
                m_forward.Normalize();
                //Calculate left vector
                m_right = Vector3.Cross(m_up,m_forward);
                m_right.Normalize();
            }
        }
        if(angle > m_maxRotate && angle < 180 )
        {
            Quaternion rot = Quaternion.Euler(0,m_maxRotate,0);
            this.transform.rotation = rot;
            m_forward = (new Vector3(0,0,1)*Mathf.Cos(m_maxRotate*Mathf.PI/180))+ (new Vector3(1,0,0)*Mathf.Sin(m_maxRotate*Mathf.PI/180));
            m_forward.Normalize();
            m_right = Vector3.Cross(m_up,m_forward);
            m_right.Normalize();
        }
        if(angle < 360-m_maxRotate && angle > 180)
        {
            Quaternion rot = Quaternion.Euler(0,360-m_maxRotate,0);
            this.transform.rotation = rot;
            m_forward = (new Vector3(0,0,1)*Mathf.Cos(m_maxRotate*Mathf.PI/180))- (new Vector3(1,0,0)*Mathf.Sin(m_maxRotate*Mathf.PI/180));
            m_forward.Normalize();
            m_right = Vector3.Cross(m_up,m_forward);
            m_right.Normalize();
        }

        
        
    }
}
