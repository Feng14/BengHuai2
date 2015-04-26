using UnityEngine;
using System.Collections;

// 这是一个对象池（环状链表），用于储存Message对象，其中包含活跃对象和废弃对象，拥有指向最后一次被时候的活跃对象的指针（lastActive）(在装满的情况下指向最后使用的活跃对象)，和指向最早进来的活跃对象的指针（firstActive）
// 两个指针中间的对象一旦被废弃，将被挪移到end指针后
// 一旦容器被装满(End=Start-1),将自动扩容
public class PoolContainer<T> : MonoBehaviour where T : Message {
    public int length;
    public PoolNode<T>  lastActive, firstActive;
	
    public PoolContainer(){

    }

    // 对象池对象的数量
    public int Length
    {
        get { return length; }
        set { length = value; }
    }

    // 获取一个废弃的对象
    public T getObjectInstance()
    {
        if (length == 0)
            return addNewObject().t;
        else
        {
            if (lastActive.next != firstActive) // 还有废弃的没有，直接拿出去
            {
                lastActive = lastActive.next;
                return lastActive.t;
            }
            else    // 没有废弃的了，需要添加新的进来
            {
                return addNewObject().t;
            }
        }
    }

    // 往对象池内添加新对象
    private PoolNode<T> addNewObject()
    {
        PoolNode<T> node = new PoolNode<T>();
        if (length == 0)    // 空
        {


            node.next = node;
            node.previous = node;

            firstActive =  node;
            lastActive = node;
        }
        else    // 满
        {
            lastActive.next.previous = node;
            node.next = lastActive.next;
            lastActive.next = node;
            node.previous = lastActive;

            lastActive = node;
        }
        return node;
    }
}
