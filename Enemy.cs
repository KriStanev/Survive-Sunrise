using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [HideInInspector] public StateMachine m_stateMachine;
    [HideInInspector] public NavMeshAgent m_navAgent;
    [HideInInspector] public Animator m_anim;
    [HideInInspector] public Collider m_range;
    [HideInInspector] public Transform m_transform;
    public GameObject[] m_wanderPoints = new GameObject[10]; //assign in inspector
    public int m_hitPoints;   
    Vector3 oldPos = new Vector3(0, 0, 0);
    
    void Awake()
    {        
        m_navAgent = GetComponent<NavMeshAgent>();
        m_range = GetComponent<SphereCollider>();
        m_transform = GetComponent<Transform>();
        m_anim = GetComponent<Animator>();
        m_hitPoints = 100;
        m_stateMachine = new StateMachine(this);
    }

	// Use this for initialization
	void Start () 
    {        
        
	}

    // Update is called once per frame
    void Update()
    {
        if (oldPos != gameObject.transform.position)
        {
            m_anim.SetFloat("Vertical", 1.0f);
        }

        m_stateMachine.runStateMachine();

        print(m_stateMachine.getCurrentState());

        oldPos = gameObject.transform.position;

        if (m_stateMachine.getTargetType() != 2)
        {
            m_stateMachine.setTargetType(2);
        }
    }

    void OnTriggerEnter(Collider col)
    {                                 
        if (col.gameObject.tag == "Player")
        {
            m_stateMachine.setTargetType(1);
        }
                
        if (col.gameObject.tag == "WanderPoint")
        {
            col.gameObject.SetActive(false);
            m_stateMachine.setTargetType(0);
            print("point reached and target type set to 0");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_stateMachine.setTargetType(0);
        }
    }

    public int GetRand()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        return Random.Range(0, 65535);
    }

    public float getDeltaTime()
    {
        return Time.deltaTime;
    }

    public float getDistance(Vector3 t_thispos, Vector3 t_targetpos)
    {
        return Vector3.Distance(t_thispos, t_targetpos);
    }
}
