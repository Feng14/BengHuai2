using UnityEngine;
using System.Collections;

public class PoolNode<T> where T : Message {
    public PoolNode<T> previous, next;
    public T t;
    private bool active = false;

    public PoolNode()
    {
        t = System.Activator.CreateInstance<T>();
    }

    public T Object
    {
        get { return t; }
        set { t = value; }
    }

    public PoolNode<T> Previous
    {
        get { return previous; }
        set { previous = value; }
    }

    public PoolNode<T> Next
    {
        get { return next; }
        set { next = value; }
    }

    public bool Using{
        get{return active;}
        set { active = value; }
    }
}
