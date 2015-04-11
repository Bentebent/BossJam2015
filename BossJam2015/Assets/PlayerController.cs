using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public float m_maxSpeed;
    public float m_acceleration;
    public float m_currentSpeed;

    public float m_oldX;
    public float m_oldZ;

    public float m_rotationSpeed;

    public string m_playerName;

    private float m_threshold = 0.3f;

    private GameObject m_turret;

    private Vector3 m_turretOffset = new Vector3(0.01f, -0.02f, 1.367f);
    private Quaternion shit;

    private float m_shotLimit = 1.0f;
    private float m_shotTimer = 0.0f;


	// Use this for initialization
	void Start () 
    {
        m_turret = GetChildGameObject(this.gameObject, "tank_turret");
        shit = m_turret.transform.rotation;
	}

    public void SetPlayerTag(string tag)
    {
        m_turret.tag = tag;
        this.tag = tag;
    }

    static public GameObject GetChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) 
            return t.gameObject;
        return null;
    }

    static public Vector3 Vec3Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
	
	// Update is called once per frame
	void Update () 
    {
        float x = 0;
        float z = 0;

        z = Input.GetAxis("MoveHorizontal_Player" + m_playerName);
        x = Input.GetAxis("MoveVertical_Player" + m_playerName);

        RotateTracks(x, z);
        MoveTank(x, z);
        RotateTurret();
        Shoot();
       
	}

    void Shoot()
    {
        m_shotTimer -= Time.deltaTime;

        if (Input.GetButtonDown("RBumper_Player" + m_playerName) && m_shotTimer <= 0.0f)
        {
            // Shoot
            GameObject newBullet = (GameObject)Instantiate(Resources.Load("Bullet"));

            BulletBehaviour bulletInfo = newBullet.GetComponent<BulletBehaviour>();
            bulletInfo.Direction = -m_turret.transform.forward;
            bulletInfo.ParentTag = gameObject.tag;
            newBullet.transform.position = m_turret.transform.position + bulletInfo.Direction;
            newBullet.transform.forward = m_turret.transform.forward;
            m_shotTimer = m_shotLimit;
        }
    }

    void LateUpdate()
    {
        
    }

    private void MoveTank(float x, float z)
    {
        if (Input.GetAxis("LTrigger_Player" + m_playerName) > 0)
        {
            if (m_currentSpeed < m_maxSpeed)
                m_currentSpeed += m_acceleration * Time.deltaTime;

            gameObject.transform.position -= transform.forward * m_currentSpeed * Time.deltaTime;
            m_oldX = x;
            m_oldZ = z;
        }
        else if (Input.GetAxis("RTrigger_Player" + m_playerName) < 0)
        {
            if (m_currentSpeed < m_maxSpeed)
                m_currentSpeed += m_acceleration * Time.deltaTime;

            gameObject.transform.position += transform.forward * m_currentSpeed * Time.deltaTime;
            m_oldX = x;
            m_oldZ = z;
        }

       
    }

    void RotateTurret()
    {
        float xRot = Input.GetAxis("TargetHorizontal_Player" + m_playerName);
        float yRot = Input.GetAxis("TargetVertical_Player" + m_playerName);
        float rotation_threshold = 0.1f;

        if (Mathf.Abs(xRot) > rotation_threshold || Mathf.Abs(yRot) > rotation_threshold)
        {
            Vector3 direction = new Vector3(xRot, 0.0f, yRot).normalized;
            Vector3 crossDir = -Vector3.Cross(direction, new Vector3(0, 1, 0));
            Quaternion derp = Quaternion.Slerp(m_turret.transform.rotation, Quaternion.LookRotation(crossDir, new Vector3(0, 1, 0)), m_rotationSpeed * Time.deltaTime);
            m_turret.transform.rotation = derp;
        }

    }

    void RotateTracks(float x, float z)
    {
        float rotation_threshold = 0.1f;
        if (Mathf.Abs(x) > rotation_threshold || Mathf.Abs(z) > rotation_threshold)
        {
            Vector3 direction = new Vector3(x, 0.0f, -z).normalized;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, transform.up), m_rotationSpeed * Time.deltaTime);
            
        }
    }
}
