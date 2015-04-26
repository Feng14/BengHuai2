//--------------------------------------------
//            NGUI: HUD Text
// Copyright © 2012 Tasharen Entertainment
//--------------------------------------------

using UnityEngine;

/// <summary>
/// Simple class that will set up and add HUDText simulating a conversation.
/// </summary>

[AddComponentMenu("NGUI/Examples/Chat Participant")]
public class ChatParticipant : MonoBehaviour
{
	// The UI prefab that is going to be instantiated above the player
	public GameObject prefab;
	public Transform lookAt;

	HUDText mText = null;

	/// <summary>
	/// Used by ChatManager when adding text above the participant's head.
	/// </summary>

	public HUDText hudText { get { return mText; } }

	// Use this for initialization
	void Start()
	{
		// We need the HUD object to know where in the hierarchy to put the element
		if (HUDRoot.go == null)
		{
			GameObject.Destroy(this);
			return;
		}

		GameObject child = NGUITools.AddChild(HUDRoot.go, prefab);
		mText = child.GetComponentInChildren<HUDText>();
		child.AddComponent<UIFollowTarget>().target = transform;

		// Add this character as part of conversation.
		if (ChatManager.instance != null) ChatManager.instance.AddParticipant(this);
	}
}
