//--------------------------------------------
//            NGUI: HUD Text
// Copyright � 2012 Tasharen Entertainment
//--------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Simple script to control the talking between two or more characters.
/// It's an example script that shows lines of text above two or more targets, alternating them with each line.
/// </summary>

[AddComponentMenu("NGUI/Examples/Chat Manager")]
public class ChatManager : MonoBehaviour
{
	public static ChatManager instance;

	public string[] chatMessages;
	public LookAtTarget cameraLookAt;

	List<ChatParticipant> mParticipants = new List<ChatParticipant>();
	int mCurrentChatter = 0;
	int mCurrentMessage = 0;
	bool mDisplay = false;

	void Awake () { instance = this; }
	void OnDestroy () { instance = null; }

	/// <summary>
	/// Used by the Chat Participant to register it with the Manager.
	/// </summary>

	public void AddParticipant (ChatParticipant participant) { mParticipants.Add(participant); }

	/// <summary>
	/// Display a new HUDText every 2 seconds.
	/// </summary>

	void Update ()
	{
		if (!mDisplay && chatMessages != null) StartCoroutine(ProgressChat());
	}

	IEnumerator ProgressChat ()
	{
		mDisplay = true;

		// Get the Combat text for the current chatter.
		HUDText ct = mParticipants[mCurrentChatter].hudText;

		if (ct != null)
		{
			ct.Add(chatMessages[mCurrentMessage].Replace("\\n", "\n"), Color.white, 2f);
			cameraLookAt.target = mParticipants[mCurrentChatter].lookAt;
		}

		yield return new WaitForSeconds(4f);

		mCurrentChatter++;
		mCurrentMessage++;

		if (mCurrentChatter >= mParticipants.Count) mCurrentChatter = 0;

		// Rand out of message start again
		if (mCurrentMessage >= chatMessages.Length)
		{
			mCurrentMessage = 0;
			yield return new WaitForSeconds(5f);
		}

		mDisplay = false;
	}
}
