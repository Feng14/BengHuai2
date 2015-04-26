using UnityEngine;
using System.Collections;

//Unuse
// 消息的父类
public class Message : MonoBehaviour {
    //public static Message receiver = new Message();

	// 触发时间
	public float happenTime;

	// 更新时间（现在)
	public void setCurrentTime(){
		happenTime = Time.time;
	}
}
