using UnityEngine;

public class WheelControler : MonoBehaviour
{
    [SerializeField]
    private CarController cc;
    [SerializeField]
    private GameObject[] m_frontTiers;
    [SerializeField]
    private float m_maxRotate = 35.0f,m_rotate;
    private float m_speed;
    private Vector3 m_right,m_up,m_forward;
    private bool m_isFrontTier,mmm;

    public int target = 30;
      
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_right = this.transform.right;
        m_up = new Vector3(0,1,0);  //y axis
        m_forward = this.transform.forward; //z axis
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
        // Debug.Log("Forward : "+m_forward);
        // Debug.Log("Right : "+m_right);
        // Debug.Log("Up : "+m_up);
    }

    private void SpeedRotation()
    {
        if(cc!=null)
        {
            m_speed = cc.GetCarVelocity();
            
        }else m_speed = 0;
        this.transform.Rotate(new Vector3(1,0,0),m_speed*Time.deltaTime*360,Space.Self);
        
        m_forward = this.transform.forward;
        m_right=this.transform.right;
    }

    private void SteeringRotation()
    {
        //Vector3 currentRotation = transform.rotation.eulerAngles;
        if(Input.GetKey(KeyCode.A))
        {
            //if( !(currentRotation.y < 360-m_maxRotate && currentRotation.y >180))
                this.transform.Rotate(m_up,-m_maxRotate*Time.deltaTime,Space.World);        //Rotate the Tire left
                
            //else Debug.Log("A" + currentRotation.y);

            m_forward = this.transform.forward;
            m_right = this.transform.up;
        }
        if(Input.GetKey(KeyCode.D))
        {
            //if(!(currentRotation.y <180 && currentRotation.y > m_maxRotate))
                this.transform.Rotate(m_up,m_maxRotate*Time.deltaTime,Space.World);     //Rotate the Tire right
                
            //else Debug.Log("D " +currentRotation.y);

            m_forward = this.transform.forward;
            m_right = this.transform.up;
        }
        
        // if(currentRotation.y < 360-m_maxRotate && currentRotation.y > 180) {currentRotation.y = -m_maxRotate;mmm=true;}
        // if(currentRotation.y < 180 && currentRotation.y > m_maxRotate ) {currentRotation.y =  m_maxRotate;mmm=false;}
        // Debug.Log(currentRotation + " --> " + mmm);
        // this.transform.rotation = Quaternion.Euler(currentRotation);
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y = ClampAngle(currentRotation.y,-m_maxRotate,m_maxRotate);
        transform.localRotation = Quaternion.Euler (currentRotation);
    }

    float ClampAngle( float angle, float min, float max )
     {
         if ( angle < -360 )
             angle += 360;
         if ( angle > 360 )
             angle -= 360;
         
         return Mathf.Clamp( angle, min, max );
     }
}
