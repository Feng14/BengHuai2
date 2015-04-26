using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("rotation : " + transform.rotation);
        Debug.Log("localRotation : " + transform.localRotation);
        Debug.Log("eulerAngles : " + transform.eulerAngles);
        Debug.Log("localEulerAngles : " + transform.localEulerAngles);
        Debug.Log("localToWorldMatrix : " + transform.localToWorldMatrix);
	}
}
