using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInstance {
	
	public Vector3 position;
	public Quaternion rotation;
	public Vector3 velocity;

	public TimeInstance (Vector3 _position, Quaternion _rotation, Vector3 _velocity) {
		position = _position;
		rotation = _rotation;
		velocity = _velocity;
	}

}
