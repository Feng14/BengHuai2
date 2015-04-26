using UnityEngine;
using System.Collections;

public class test223 : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter : " + gameObject.name);
    }

    void OnTriggerEnter(Collision collision)
    {
        Debug.Log("OnTriggerEnter : " + gameObject.name);

    }
}
