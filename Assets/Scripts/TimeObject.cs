using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeObject {

	private float rewindCount = 5f / Time.fixedDeltaTime; 

	public abstract void Rewind ();
	public abstract void Resume ();

}
