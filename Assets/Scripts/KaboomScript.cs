using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaboomScript : MonoBehaviour {

	[Header("Kaboom parameters")]
	public float range = 100f;
	public ParticleSystem explosionEffect;
	public float radius = 10f;
	public float explosionForce = 700f;
	[Range(1f/6f, 5f)]
	public float fireCooldown;

	[Header("Gun reference")]
	public GameObject gun;
	
	private Camera cam;
	private Animator gunAnime;
	private float fireTime;

	// Use this for initialization
	void Start () {
		cam = transform.GetComponentInChildren<Camera>();
		gunAnime = gun.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (cam == null) {
			Debug.Log("Camera not found!");
			return;
		}
		
		if (Input.GetButtonDown("Fire1") && Time.time >= (fireTime + fireCooldown)) {
			Fire();
			fireTime = Time.time;
		}
	}

	void Fire () {
		RaycastHit hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) {
			StartCoroutine(PlayOneShot("isFiring"));

			if (explosionEffect != null) {
				ParticleSystem effect = Instantiate(explosionEffect, hit.point, Quaternion.identity);
				Destroy(effect.gameObject, 5f);
			} else {
				Debug.Log("Explosion effect ParticleSystem not found!");
			}

			Collider[] hitColliders = Physics.OverlapSphere(hit.point, radius);

			foreach (Collider nearbyObject in hitColliders) {
				Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
				if (rb != null) {
					rb.AddExplosionForce(explosionForce, hit.point, radius);
				}
			}
		}
	}

	public IEnumerator PlayOneShot ( string _paramName )
    {
        gunAnime.SetBool( _paramName, true );
        yield return null;
        gunAnime.SetBool( _paramName, false );
    }
}
