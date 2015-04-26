using UnityEngine;
using System.Collections;

public class Interface_Stack {
    private Linkedlist<GameObject> gameObjectStack;


    public Interface_Stack()
    {
        gameObjectStack = new Linkedlist<GameObject>();
    }

    public void push(GameObject go)
    {
        gameObjectStack.push(go);
    }

    public GameObject pop()
    {
        return gameObjectStack.pop();
    }

    public int getLength()
    {
        Node<GameObject> node = gameObjectStack.first;
        int length = 0;
        while (node != null)
        {
            length++;
            node = node.next;
        }
        return length;
    }

    public void clear()
    {
        if (gameObjectStack.first != null)
        {
            while (gameObjectStack.first.next != null)
            {
                //Debug.Log("Destroy : " + gameObjectStack.first.t.name);
                gameObjectStack.first = gameObjectStack.first.next;
                //Debug.Log("Destroy : " + gameObjectStack.first.previous.t.name);
                //Debug.Log("next == null  " + (gameObjectStack.first.t == null).ToString());
                gameObjectStack.first.previous.t.transform.parent = null;
                GameObject.Destroy(gameObjectStack.first.previous.t);
            }
            
            GameObject.Destroy(gameObjectStack.first.t);
            gameObjectStack.first = null;
            gameObjectStack.last = null;
        }
    }

    // 获取栈顶的一个对象（不弹出）
    public GameObject getStackTop()
    {
        return gameObjectStack.getLast();
    }

    // 是否为空
    public bool ifEmpty()
    {
        if (gameObjectStack.first == null)
            return true;

        return false;
    }
}
