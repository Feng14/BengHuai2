using UnityEngine;
using System.Collections;

// 子弹
public class Bullets : MonoBehaviour {
    public enum Sender { Player, Monster };

    //public GameObject bulletGameObject;
    public Weapon weapon;
    public Sender sender;

    public void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("Bullet OnTriggerEnter");
        if ((sender == Sender.Player || collider.tag == "Player") && (gameObject.tag != collider.tag))
            GameRoot_InBattle.getSingleton<MessageManager_InBattle>().SendMessage_BulletCollisionEvent(this, collider.gameObject, collider.GetComponent<CharacterData>());
    }


    // 设置图片
    public void setImage(string path)
    {

    }
}
