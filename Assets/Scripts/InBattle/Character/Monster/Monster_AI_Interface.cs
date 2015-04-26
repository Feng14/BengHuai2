using UnityEngine;
using System.Collections;

public interface Monster_AI_Interface{

    void MoveTo(Vector3 position);

    void Attack(Player player);

    void Stand();
}
