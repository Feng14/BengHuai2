using UnityEngine;
using System.Collections;


// 附着在玩家人物身上
public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //void OnTriggerEnter(Collider collider)
    //{
    //    float distanceZ = (transform.lossyScale.z + collider.transform.lossyScale.z) / 2;

    //    switch (collider.tag)
    //    {
    //        case "Wall" :
    //            transform.position = new Vector3(transform.position.x, transform.position.y, collider.transform.position.z - distanceZ - 4);
    //            break;
    //        case "Bar":
    //            transform.position = new Vector3(transform.position.x, transform.position.y, collider.transform.position.z + distanceZ + 3);
    //            break;
    //    }
    //    //other.gameObject.GetComponent<ColliderEvent>().dealPlayerCollider(gameObject);
    //}

}
