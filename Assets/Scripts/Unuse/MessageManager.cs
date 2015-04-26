using UnityEngine;
using System.Collections;

//Unuse
// 这是一个环状链表，用于储存Message对象，其中，拥有一个指向
public class MessageManager{
    // 主界面事件


    //public static event MessageManager.MessageDelegate_MainInterfaceMode MainInterface_Mode_Battle;
    //public static event MessageManager.MessageDelegate_MainInterfaceMode MainInterface_Mode_Goods;
    //public static event MessageManager.MessageDelegate_MainInterfaceMode MainInterface_Mode_Pray;
    //public static event MessageManager.MessageDelegate_MainInterfaceMode MainInterface_Mode_Friend;
    //public static event MessageManager.MessageDelegate_MainInterfaceMode MainInterface_Mode_Other;



    private MessageManager()
    {
    }

/**
    //private static MessageManager massageManger = new MessageManager();
	private static Hashtable messageObjectTable = new Hashtable();
    private static Hashtable messageListenerTable = new Hashtable();

    // 获取一个信息对象
    public static T getMessageInstance<T>(string messageClassName) where T : Message{
        //Debug.Log("getMessageInstance");
        // 哈希表中有这个对象池
        //messageObjectTable.Contains[T];
        if (messageObjectTable.Contains(messageClassName))
            return ((PoolContainer<T>)messageObjectTable[messageClassName]).getObjectInstance();
        else
        {
            //哈希表中没有这个对象池，开始创建
            PoolContainer<T> p = new PoolContainer<T>();
            messageObjectTable.Add(messageClassName, p);
            return p.getObjectInstance();
        }
    }

    // 发出一个消息（注册接受这个消息的对象将会接收到消息）
    public static void boradcastMessage<MessageType>(string messageClassName, MessageType message) where MessageType : Message
    {
        Debug.Log("boradcastMessage:" + messageClassName);
        if (messageListenerTable.Contains(messageClassName))
        {
            //Debug.Log("Contains");
            ArrayList arrayList = (ArrayList)messageListenerTable[messageClassName];
            //Debug.Log("length: " + arrayList.Count);
            foreach (MessageListener listener in arrayList) {
                Debug.Log("Send Message: " + messageClassName);
                //typeof(Message.receiver);
                //Object a = listener;
                //MessageType.receiver;
                //Message.GetType();

                //MessageListener l2 = (typeof(MessageType.receiver))listener;
                //((MessageType.receiver.GetType())listener).onResiveMessage(message);
            }
        }
    }

    // 注册消息接收
    public static void registerMessageResive(string messageClassName, MessageListener listener)
    {
        //Debug.Log("registerMessageResive");
        if (!messageListenerTable.Contains(messageClassName))
        {
            ArrayList list = new ArrayList();
            list.Add(listener);
            messageListenerTable.Add(messageClassName, list);
        }
        else
        {
            ((ArrayList)messageListenerTable[messageClassName]).Add(listener);
        }
    }
 * 
 * **/
}
