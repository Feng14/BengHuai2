using UnityEngine;
using System.Collections;

// 附着在Monster的GameObject上的脚本
public class MonsterGO : MonoBehaviour, Monster_AI_Interface
{
    public SkeletonAnimation skeletonAnimation;
    public Monster monster;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // 设置怪物数据
    public void SetMonster(Monster monster)
    {
        this.monster = monster;
        gameObject.GetComponent<CharacterData>().HP_Current = monster.Hp;
    }

    public void MoveTo(Vector3 position)
    {
        //Debug.Log("Monster : MoveTo " + position.ToString());

        skeletonAnimation.AnimationName = "Run";
        if (monster == null)
            return;

        float x = position.x - transform.position.x,
            z = position.z - transform.position.z,
            angle = Mathf.Atan2(z , x);

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (x < 0 ? -1 : 1), transform.localScale.y, transform.localScale.z);

        //Debug.Log("Velocity : " + monster.Speed * Mathf.Sin(angle) + "   " + monster.Speed * Mathf.Cos(angle));
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(monster.Speed * Mathf.Cos(angle), 0, monster.Speed * Mathf.Sin(angle));

    }

    public void Attack(Player player)
    {
        //Debug.Log("Monster : Attack");
        skeletonAnimation.AnimationName = "Attack";
    }

    public void Stand()
    {
        //Debug.Log("Monster : Stand");
        skeletonAnimation.AnimationName = "Stand";
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
