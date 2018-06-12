using System.Collections.Generic;
using UnityEngine;

public abstract class TimeObject : MonoBehaviour {

	public int rewindCount;
	public bool isRewinding;
	public float rewindSpeed = 2;
	[HideInInspector]
	public List<TimeInstance> timeInstanceList;

	public abstract void StartRewind ();
	public abstract void StopRewind ();
	public abstract void Rewind ();
	public abstract void Record ();

	void Awake () {
		rewindCount = (int)Mathf.Round(5f / Time.fixedDeltaTime);
		timeInstanceList = new List<TimeInstance>();
		isRewinding = false;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			StartRewind();
			Time.timeScale = rewindSpeed;
		}
	}

	void FixedUpdate () {
		if (isRewinding) {
			Rewind();
			if (timeInstanceList.Count <= 0) {
				StopRewind();
				Time.timeScale = 1;
			}
		} else
			Record();
	}
}
