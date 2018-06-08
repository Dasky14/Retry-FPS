using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	private PlayerMotor motor;

	[Range(0.1f, 25f)]
	public float speed = 6;
	public float horizSensitivity = 1;
	public float vertSensitivity = 1;

	void Start () {
		motor = GetComponent<PlayerMotor>();
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update () {
		//Character basic movement
		Vector3 movHorizontal = transform.right * Input.GetAxisRaw("Horizontal");
		Vector3 movVertical = transform.forward * Input.GetAxisRaw("Vertical");

		Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

		motor.Move(velocity);

		//Player horizontal aiming
		Vector3 rotationX = new Vector3(0f, Input.GetAxisRaw("Mouse X"), 0f) * horizSensitivity;
		motor.RotateX(rotationX);

		//Player vertical aiming
		Vector3 rotationY = new Vector3(Input.GetAxisRaw("Mouse Y"), 0f, 0f) * vertSensitivity;
		motor.RotateY(rotationY);
	}
}
