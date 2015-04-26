using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
    public enum AtTheOrientation { Z_Plus, Z_Minus };
    public AtTheOrientation orientation = AtTheOrientation.Z_Plus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider collider)
    {
        GoAway(collider);
    }

    public void OnTriggerStay(Collider collider)
    {
        GoAway(collider);
    }

    private void GoAway(Collider collider)
    {
        Debug.Log("GoAway    " + gameObject.name + "  " + collider.tag);
        if (collider.tag == "Player" || collider.tag == "Monster_Floor")
        {
            float distance = (transform.lossyScale.z + collider.transform.lossyScale.z) / 2;
            collider.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y,
                transform.position.z + (distance + 2.5f) * (orientation == AtTheOrientation.Z_Plus ? -1 : 1));
        }

    }
}
