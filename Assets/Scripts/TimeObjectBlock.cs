using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObjectBlock : TimeObject {

	private Rigidbody rb;

	void Start () {
		rb = transform.GetComponent<Rigidbody>();
	}

	public override void StartRewind () {
		isRewinding = true;
		rb.isKinematic = true;
	}

	public override void StopRewind () {
		isRewinding = false;
		rb.isKinematic = false;
	}

	public override void Rewind () {
		if (timeInstanceList.Count > 0) {
			transform.position = timeInstanceList[0].position;
			transform.rotation = timeInstanceList[0].rotation;
			rb.velocity = timeInstanceList[0].velocity;
			timeInstanceList.RemoveAt(0);
		}
	}

	public override void Record () {
		if (timeInstanceList.Count >= rewindCount)
			timeInstanceList.RemoveAt(timeInstanceList.Count - 1);

		timeInstanceList.Insert(0, new TimeInstance(transform.position, transform.rotation, rb.velocity, rb.angularVelocity));
	}

}
