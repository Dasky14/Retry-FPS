﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	private Rigidbody rb;
	private Camera cam;
	private Vector3 velocity;
	private Vector3 rotationX;
	private Vector3 rotationY;
	private float minimumX = -90f;
	private float maximumX = 90f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		ApplyMovement();
		ApplyRotationX();
		ApplyRotationY();
	}

	void ApplyMovement () {
		if (velocity != Vector3.zero)
			rb.MovePosition(rb.position + velocity * Time.deltaTime);
	}

	void ApplyRotationX () {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationX));
	}

	void ApplyRotationY () {
		if (cam != null) {
			cam.transform.Rotate(rotationY);
			Quaternion camRot = cam.transform.localRotation;
			camRot = ClampRotationAroundXAxis (camRot);
			cam.transform.localRotation = camRot;
		}
	}

	public void Move (Vector3 _velocity) {
		velocity = _velocity;
	}

	public void RotateX (Vector3 _rotation) {
		rotationX = _rotation;
	}

	public void RotateY (Vector3 _rotation) {
		rotationY = -_rotation;
	}

	Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
		angleX = Mathf.Clamp (angleX, minimumX, maximumX);
		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
	
}