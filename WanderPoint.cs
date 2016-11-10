using UnityEngine;
using System.Collections;

public class WanderPoint : MonoBehaviour {

    public GameObject wPoint = null;
    bool enemyInRange;

	// Use this for initialization
	void Start () 
    {
        if (gameObject.name.Contains("II"))
        {
            wPoint = GameObject.Find(gameObject.name.Replace(" II", ""));
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (wPoint != null && !enemyInRange)
        {
            if (wPoint.activeSelf == false)
            {
                wPoint.SetActive(true);
            }
        }
	}

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemyInRange = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemyInRange = true;
        }
    }

}
