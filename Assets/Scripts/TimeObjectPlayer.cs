using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TimeObjectPlayer : TimeObject {

	[HideInInspector]
	public List<Quaternion> cameraList;
	private Rigidbody rb;
	private Camera cam;
	private PlayerController pc;

	void Start () {
		rb = transform.GetComponent<Rigidbody>();
		pc = GetComponent<PlayerController>();
		cam = transform.GetComponentInChildren<Camera>();
	}

	public override void StartRewind () {
		isRewinding = true;
		ControlsEnabled(false);
	}

	public override void StopRewind () {
		isRewinding = false;
		ControlsEnabled(true);
	}

	public override void Rewind () {
		if (timeInstanceList.Count > 0) {
			transform.position = timeInstanceList[0].position;
			transform.rotation = timeInstanceList[0].rotation;
			rb.velocity = timeInstanceList[0].velocity;
			timeInstanceList.RemoveAt(0);
		}
		if (cameraList.Count > 0) {
			cam.transform.rotation = cameraList[0];
			cameraList.RemoveAt(0);
		}
		if (cameraList.Count <= 0 && timeInstanceList.Count <= 0) {
			StopRewind();
		}
	}

	public override void Record () {
		if (timeInstanceList.Count >= rewindCount)
			timeInstanceList.RemoveAt(timeInstanceList.Count - 1);
		if (cameraList.Count >= rewindCount)
			cameraList.RemoveAt(cameraList.Count - 1);

		timeInstanceList.Insert(0, new TimeInstance(transform.position, transform.rotation, rb.velocity, rb.angularVelocity));
		cameraList.Insert(0, cam.transform.rotation);
	}

	void ControlsEnabled (bool _enabled) {
		if (pc != null)
			pc.enabled = _enabled;
	}
}
