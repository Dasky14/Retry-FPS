using System.Collections.Generic;
using UnityEngine;

public abstract class TimeObject : MonoBehaviour{

	public int rewindCount;
	public bool isRewinding;
	[HideInInspector]
	public List<TimeInstance> timeInstanceList;

	public abstract void StartRewind ();
	public abstract void StopRewind ();
	public abstract void Rewind ();
	public abstract void Record ();

	void Awake () {
		rewindCount = (int)(5f / Time.fixedDeltaTime);
		timeInstanceList = new List<TimeInstance>();
		isRewinding = false;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
			StartRewind();
	}

	void FixedUpdate () {
		if (isRewinding)
			Rewind();
		else
			Record();
	}
}
