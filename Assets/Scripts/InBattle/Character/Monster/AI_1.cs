using UnityEngine;
using System.Collections;

public class AI_1 : MonoBehaviour {
    private Player player;
    private bool closePlayer;

	// Use this for initialization
	void Start () {
        player = GameRoot_InBattle.getSingleton<PlayerManager_InBattle>().player.GetComponent<Player>();
        if (player != null)
            moveToPlayer(player.transform.position);

        registerEvent();
        closePlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
        // 如果靠着玩家，就进行攻击
        if (closePlayer)
        {
            if (player != null)
                gameObject.SendMessage("Attack", player);
        }
	}

    void OnDestroy()
    {
        UnRegisterEvent();
    }

    // 注册事件监听
    private void registerEvent()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().playerMoveEvent
            += new MessageManager_InBattle.PlayerMoveDelegrate(moveToPlayer);
    }

    private void UnRegisterEvent()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().playerMoveEvent
            -= new MessageManager_InBattle.PlayerMoveDelegrate(moveToPlayer);
    }

    // 碰撞
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            UnRegisterEvent();
            gameObject.SendMessage("Stand");
            closePlayer = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            registerEvent();
            closePlayer = false;
        }
    }

    // 奔向玩家
    public void moveToPlayer(Vector3 position)
    {
        gameObject.SendMessage("MoveTo", position);
    }
}
