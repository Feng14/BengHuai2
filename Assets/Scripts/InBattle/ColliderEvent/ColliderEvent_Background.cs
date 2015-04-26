using UnityEngine;
using System.Collections;

public class ColliderEvent_Background : MonoBehaviour, ColliderEvent
{

    public void dealPlayerCollider(GameObject player)
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
            transform.position.z - gameObject.GetComponent<Collider>().bounds.size.z / 2 - 1);
        Debug.Log("Collider z : " + gameObject.GetComponent<Collider>().bounds.size.z);
    }
}
