using UnityEngine;
using System.Collections;

public class test_script1 : MonoBehaviour {
    public static test_script1 t;

	// Use this for initialization
	void Start () {
        t = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void fucking()
    {
        Debug.Log("Fucking " + gameObject.name);

    }
}
