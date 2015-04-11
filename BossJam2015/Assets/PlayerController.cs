﻿using UnityEngine;
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

    private bool m_moved = false;
    public bool m_dead = false;

    public Rigidbody m_rigidBody;
    public Rigidbody m_turretRB;

    public Vector3 m_contactPoint;

    public int m_lives = 10;

    private Vector3 m_startPos;
    private Quaternion m_startRot;
    private Vector3 m_turretStartPos;
    private Quaternion m_turretStartRot;

	// Use this for initialization
	void Start () 
    {
        m_turret = GetChildGameObject(this.gameObject, "tank_turret");
        m_rigidBody = GetComponent<Rigidbody>();
        m_turretRB = m_turret.GetComponent<Rigidbody>();


        m_startPos = transform.position;
        m_startRot = transform.rotation;
        m_turretStartPos = m_turret.transform.position;
        m_turretStartRot = m_turret.transform.rotation;
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

    public void ResetMe()
    {
        m_turret.transform.parent = gameObject.transform;
        m_rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        m_turretRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        m_turretRB.useGravity = false;
        m_turretRB.isKinematic = true;

        m_turretRB.velocity = Vector3.zero;
        m_turretRB.angularVelocity = Vector3.zero;
        m_rigidBody.velocity = Vector3.zero;
        m_rigidBody.angularVelocity = Vector3.zero;

        transform.position = m_startPos;
        transform.rotation = m_startRot;

        m_turret.transform.position = m_turretStartPos;
        m_turret.transform.rotation = m_turretStartRot;

        m_dead = false;
    }
	
	// Update is called once per frame
	void Update () 
    {
        //UR DED LOL
        if (m_dead && Input.GetButtonDown("RBumper_Player" + m_playerName))
            ResetMe();
        else if (m_dead)
            return;

        float x = 0;
        float z = 0;

        m_moved = false;

        z = Input.GetAxis("MoveHorizontal_Player" + m_playerName);
        x = Input.GetAxis("MoveVertical_Player" + m_playerName);

        LayerMask boardTiles = 1 << LayerMask.NameToLayer("Environment");
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = new Vector3(0, -1, 0);
        
        RaycastHit hitInfo;

        Physics.Raycast(ray, out hitInfo, 20.0f, boardTiles);
        float deaccSpeed = 0.0f;

        if (hitInfo.distance < 1.0f)
        {
            RotateTracks(x, z);
            MoveTank(x, z);
            deaccSpeed = 50.0f;
        }
        else
            deaccSpeed = 5.0f;

        gameObject.transform.position += transform.forward * m_currentSpeed * Time.deltaTime;



        if (m_currentSpeed > 0.0f && !m_moved)
            m_currentSpeed -= deaccSpeed * Time.deltaTime;
        else if (m_currentSpeed < 0.0f && !m_moved)
            m_currentSpeed += deaccSpeed * Time.deltaTime;
        
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

        
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (m_dead)
            return;

        if (collision.gameObject.tag == "Projectile")
        {
        }
        else if (collision.gameObject.tag == "Player1" 
            || collision.gameObject.tag == "Player2"
            || collision.gameObject.tag == "Player3"
            || collision.gameObject.tag == "Player4")
        {
            PlayerController colliderPC = collision.gameObject.GetComponent<PlayerController>();

            if (colliderPC != null)
            {
                Vector3 v = transform.forward * m_currentSpeed;
                v = Vector3.Normalize(v);

                Vector3 vv = colliderPC.gameObject.transform.position - transform.position;
                vv = Vector3.Normalize(vv);

                float ab = Vector3.Angle(v, vv);

                bool killCollider = false;
                bool killMe = false;

                if (ab < 45.0f)
                {
                    if (m_currentSpeed > 20.0f || m_currentSpeed < -20.0f)
                    {
                        killCollider = true;
                    }
                }

               v = colliderPC.gameObject.transform.forward * colliderPC.m_currentSpeed;
               v = Vector3.Normalize(v);

               vv = transform.position - colliderPC.gameObject.transform.position;
               vv = Vector3.Normalize(vv);

               ab = Vector3.Angle(v, vv);

               if (ab < 45.0f)
               {
                   if (colliderPC.m_currentSpeed > 20.0f || colliderPC.m_currentSpeed < -20.0f)
                   {
                       killMe = true;
                   }
               }

               ContactPoint contact = collision.contacts[0];
               m_contactPoint = contact.point;
                if (killMe)
                {
                    BounceMe();
                }

                if (killCollider)
                {
                    colliderPC.m_turret.transform.parent = null;

                    colliderPC.m_rigidBody.constraints = RigidbodyConstraints.None;
                    colliderPC.m_turretRB.constraints = RigidbodyConstraints.None;
                    colliderPC.m_turretRB.useGravity = true;
                    colliderPC.m_turretRB.isKinematic = false;

                    colliderPC.m_rigidBody.AddExplosionForce(1000.0f, contact.point, 100.0f);
                    colliderPC.m_turretRB.AddExplosionForce(1000.0f, contact.point, 100.0f);
                    colliderPC.m_dead = true;
                }

            }
           
        }
    }

    public void BounceMe()
    {
        Debug.Log(tag + " BOUNCED");
        m_turret.transform.parent = null;

        m_rigidBody.constraints = RigidbodyConstraints.None;
        m_turretRB.constraints = RigidbodyConstraints.None;
        m_turretRB.useGravity = true;
        m_turretRB.isKinematic = false;

        m_rigidBody.AddExplosionForce(10000.0f, m_contactPoint, 100.0f);
        m_turretRB.AddExplosionForce(10000.0f, m_contactPoint, 100.0f);
        m_dead = true;
    }

    private void MoveTank(float x, float z)
    {
        if (Input.GetAxis("LTrigger_Player" + m_playerName) < 0)
        {
            if (m_currentSpeed > -m_maxSpeed)
                m_currentSpeed -= m_acceleration * Time.deltaTime;

            m_oldX = x;
            m_oldZ = z;

            m_moved = true;
        }
        else if (Input.GetAxis("RTrigger_Player" + m_playerName) > 0)
        {
            if (m_currentSpeed < m_maxSpeed)
                m_currentSpeed += m_acceleration * Time.deltaTime;

            m_oldX = x;
            m_oldZ = z;

            m_moved = true;
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
