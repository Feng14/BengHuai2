using UnityEngine;
using System.Collections;

// 接口当玩家碰到本物体时该如何处理玩家，在实现本接口的类中定义
public interface ColliderEvent {

    void dealPlayerCollider(GameObject player);
}
