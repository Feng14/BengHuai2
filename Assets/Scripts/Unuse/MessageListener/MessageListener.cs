using UnityEngine;
using System.Collections;

public interface MessageListener {

    bool onResiveMessage(Message message);
}
