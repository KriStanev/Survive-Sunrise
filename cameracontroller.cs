using UnityEngine;
using System.Collections;

public class cameracontroller : MonoBehaviour {

    public GameObject m_player;
    public GameObject m_camera;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        transform.position = m_player.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = m_player.transform.position;

        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.Rotate(0f, -75f * Time.deltaTime, 0f,Space.World);
            }

            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.Rotate(0f, 75f * Time.deltaTime, 0f, Space.World);
            }

            if (Input.GetAxis("Mouse Y") < 0 && (transform.rotation.eulerAngles.x + 25f*Time.deltaTime  > 342.0f  || transform.rotation.eulerAngles.x + 25f * Time.deltaTime < 40f))
            {
                    transform.Rotate(25f * Time.deltaTime, 0f, 0f, Space.Self);
            }

            if (Input.GetAxis("Mouse Y") > 0 && (transform.rotation.eulerAngles.x - 25f * Time.deltaTime > 342.0f || transform.rotation.eulerAngles.x - 25f * Time.deltaTime < 40))
            {
                    transform.Rotate(-25f * Time.deltaTime, 0f, 0f, Space.Self);
            }
        }
	}
}
