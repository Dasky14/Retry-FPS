using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TimeObjectPlayer : TimeObject {

	[HideInInspector]
	public List<Quaternion> cameraList;
	private Rigidbody rb;
	private Camera cam;
	private FirstPersonController fpsScript;

	void Start () {
		rb = transform.GetComponent<Rigidbody>();
		cam = transform.GetComponentInChildren<Camera>();
		fpsScript = transform.GetComponent<FirstPersonController>();
	}

	public override void StartRewind () {
		isRewinding = true;
		fpsScript.enabled = false;
	}

	public override void StopRewind () {
		isRewinding = false;
		fpsScript.enabled = true;
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
			fpsScript.m_MouseLook.Init(transform, cam.transform);
			StopRewind();
		}
	}

	public override void Record () {
		if (timeInstanceList.Count >= rewindCount)
			timeInstanceList.RemoveAt(timeInstanceList.Count - 1);
		if (cameraList.Count >= rewindCount)
			cameraList.RemoveAt(cameraList.Count - 1);

		timeInstanceList.Insert(0, new TimeInstance(transform.position, transform.rotation, rb.velocity));
		cameraList.Insert(0, cam.transform.rotation);
	}

}
