using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public GameObject m_cameraAnchor;

	private Animator m_anim;
    private Rigidbody m_rigBody;
    private float horizontal;
    private float vertical;
    public int m_hp;
    private float m_vertical, m_horizontal;
    public bool m_combat = false;
    public GameObject m_target;
    public List<GameObject> m_enemyArr = new List<GameObject>();

    



	// Use this for initialization
	void Start () 
    {
        
		m_anim = GetComponent<Animator>();
        m_rigBody = GetComponent<Rigidbody>();
        m_hp = 100;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {

        if (m_enemyArr.Count > 0)
        {
            m_combat = true;

            GameObject temp = null;

            for (int i = 0; i < m_enemyArr.Count; i++)
            {
                Enemy enemy = m_enemyArr[i].GetComponent<Enemy>();

                if (Vector3.Angle(transform.forward, m_enemyArr[i].transform.position - transform.position) < 10)
                {
                    if (enemy.m_stateMachine.getCurrentState() != enemy.m_stateMachine.m_deadState)
                    {
                        temp = m_enemyArr[i];
                    }
                    else
                    {
                        m_enemyArr.RemoveAt(i);
                    }
                }
            }

            m_target = temp;
        }
        else
        {
            m_combat = false;
        }

        if (m_hp < 1)
        {
            m_anim.Play("Die");
            m_hp = 1;
        }

        m_anim.SetBool("combat", m_combat);

        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");

        if (m_vertical > 0)
        {            
            m_rigBody.velocity = m_rigBody.transform.forward * Time.deltaTime * 100 ;            
        }
        if (m_vertical < 0)
        {
            m_rigBody.velocity = -m_rigBody.transform.forward * Time.deltaTime * 100;           
        }
        if (m_horizontal < 0)
        {            
            m_rigBody.AddTorque(new Vector3(0, -1f, 0)* Time.deltaTime * 10);
        }
        if (m_horizontal > 0)
        {            
            m_rigBody.AddTorque(new Vector3(0, 1f, 0) * Time.deltaTime * 10);
        }

        m_anim.SetFloat("Horizontal", m_horizontal);
        m_anim.SetFloat("Vertical", m_vertical);

        if (Input.GetMouseButtonDown(1))
        {
            switch(Random.Range(0,2) % 2)
            {
                case 0:
                    m_anim.Play("leftPunch");
                    m_target.GetComponent<Enemy>().m_stateMachine.m_combatState.takeDamage(20);
                    break;

                case 1:
                    m_anim.Play("rightPunch");
                    m_target.GetComponent<Enemy>().m_stateMachine.m_combatState.takeDamage(15);
                    break;
            }
        }               
    }

    public void takeDamage(int t_dmg)
    {
        if (Random.Range(0, 100) % 4 == 0)
        {
            m_anim.Play("Dodge");
        }
        else
        {
            m_hp -= t_dmg;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (!checkArray(col.gameObject))
            {
                m_enemyArr.Add(col.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            m_enemyArr.Remove(col.gameObject);
        }
    }

    bool checkArray(GameObject t_enemy)
    {
        for (int i = 0; i < m_enemyArr.Count; i++)
        {
            if (m_enemyArr[i].gameObject.name == t_enemy.name)
            {
                return true;
            }
        }
            return false;
    }

}