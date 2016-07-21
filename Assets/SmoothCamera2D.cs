using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

	public Transform target;
	public Vector3 offset = Vector3.zero;
	public float angle = 0.0f;
	public float smoothTime = 0.2f;

	private Vector2 velocity = Vector2.zero;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void LateUpdate () {
		if (this.target) {
			Debug.Log (this.offset);
			this.transform.position = (Vector3)Vector2.SmoothDamp(this.transform.position, this.target.position + this.offset, ref this.velocity, this.smoothTime) + this.offset;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, this.target.rotation, Time.deltaTime);
		}
	}
}
