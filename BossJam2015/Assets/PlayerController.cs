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



	// Use this for initialization
	void Start () 
    {
	
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
            Debug.Log("HURHUFHASUIFHAISHFIAUFHIUASH");
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

    }

    void RotateTracks(float x, float z)
    {
        float rotation_threshold = 0.1f;
        if (Mathf.Abs(x) > rotation_threshold || Mathf.Abs(z) > rotation_threshold)
        {
            Vector3 direction = new Vector3(x, 0.0f, -z).normalized;            
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(direction, gameObject.transform.up), m_rotationSpeed * Time.deltaTime);
        }
    }
}
