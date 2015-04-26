using UnityEngine;
using System.Collections;
using System;

// 链表
public class Linkedlist<T1> {
    public Node<T1> first, last;


    public Linkedlist()
    {
        first = last = null;
    }

    public void push(T1 t)
    {
        Node<T1> node = new Node<T1>();
        node.t = t;
        if (first == null)
        {
            first = node;
            last = node;

            first.previous = null;
            first.next = last;

            last.previous = first;
            last.next = null;
        }
        else if (first == last)
        {
            first.next = node;

            node.previous = first;
            node.next = null;

            last = node;
        }
        else
        {
            node.previous = last;
            node.next = null;

            last.next = node;
            last = node;
        }
        //Debug.Log("T == null :" + (node.t == null).ToString());
    }

    public T1 pop()
    {
        Node<T1> t = last;
        if (last.previous != null)
        {
            last = last.previous;
            last.next = null;
            t.previous = null;
        }
        return t.t;
    }

    // 获取最后一个
    public T1 getLast()
    {
        if (last == null)
            throw new Exception();
        return last.t;
    }

}
public class Node<T2>
{
    public T2 t;
    public Node<T2> previous, next;
}