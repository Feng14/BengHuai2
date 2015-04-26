using UnityEngine;
using System.Collections;

// 塞入界面栈的东西要实现的接口
public interface Interface_Stack_Layer {

    // 在创建时调用
    void OnCreate();

    // 塞入时要调用的方法
    void OnPush();

    // 弹出时要调用的方法（清除）
    void OnPop();

    // 被暂停隐藏起来时调用
    void OnPause();

    // 在重新被使用的时候调用
    void OnReUse();
}
