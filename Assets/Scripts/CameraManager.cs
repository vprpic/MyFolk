using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public Transform followTransform;
	public Transform cameraRig;
	public Transform rotateRig;
	public Camera mainCam;

	public float normalSpeed;
	public float movementSpeed;
	public float movementTime;
	public float rotationAmount;
	public Vector3 zoomAmount;

	[HideInInspector]
	public Vector3 newPosition;

	public Vector3 _LocalRotation;

	[HideInInspector]
	public Vector3 dragStartPosition;
	[HideInInspector]
	public Vector3 dragCurrentPosition;
	[HideInInspector]
	public Vector3 rotateStartPosition;
	[HideInInspector]
	public Vector3 rotateCurrentPosition;

	bool isHit = false;

	protected float _CameraDistance = 10f;

	public float MouseSensitivity;
	public float ScrollSensitvity;
	public float OrbitDampening;
	public float ScrollDampening;


	private void Start()
	{


		mainCam = Camera.main;
		newPosition = transform.position;
		_LocalRotation.y = rotateRig.eulerAngles.x;
		_LocalRotation.x = rotateRig.eulerAngles.y;
	}

	private void Update()
	{
		if (followTransform != null)
		{
			transform.position = followTransform.position;
		}
		else
		{
			HandleMouseInput();
			HandleMovementInput();
		}
	}

	void HandleMouseInput()
	{
		if (Input.GetMouseButtonDown(1))
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);

			Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

			float entry;
			if(plane.Raycast(ray, out entry))
			{
				dragStartPosition = ray.GetPoint(entry);
			}
		}
		if (Input.GetMouseButton(1))
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);

			Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

			float entry;
			if (plane.Raycast(ray, out entry))
			{
				dragCurrentPosition = ray.GetPoint(entry);
				newPosition = transform.position + dragStartPosition - dragCurrentPosition;
			}
		}

		if (Input.GetMouseButton(2))
		{
			//Rotation of the Camera based on Mouse Coordinates
			if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
			{
				_LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
				_LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

				//Clamp the y Rotation to horizon and not flipping over at the top
				if (_LocalRotation.y < 10f)
					_LocalRotation.y = 10f;
				else if (_LocalRotation.y > 80f)
					_LocalRotation.y = 80f;
			}
		}
		//Zooming Input from our Mouse Scroll Wheel
		if (Input.GetAxis("Mouse ScrollWheel") != 0f)
		{
			float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

			ScrollAmount *= (this._CameraDistance * 0.3f);

			this._CameraDistance += ScrollAmount * -1f;

			this._CameraDistance = Mathf.Clamp(this._CameraDistance, 1.5f, 100f);
		}
	}

	void HandleMovementInput()
	{
		#region position

		Vector3 temp;
		movementSpeed = normalSpeed * (this._CameraDistance * 0.3f);
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			temp = (rotateRig.rotation * transform.forward).normalized;
			temp.y = 0f;
			newPosition += (temp * movementSpeed);
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			temp = (rotateRig.rotation * transform.forward).normalized;
			temp.y = 0f;
			newPosition += (temp * -movementSpeed);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			temp = (rotateRig.transform.right * movementSpeed);
			temp.y = 0f;
			newPosition += temp;
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			temp = (rotateRig.transform.right * -movementSpeed);
			temp.y = 0f;
			newPosition += temp;
		}
		#endregion position

		if (Input.GetKey(KeyCode.Q))
		{
			_LocalRotation.x += rotationAmount;
		}
		else if (Input.GetKey(KeyCode.E))
		{
			_LocalRotation.x -= rotationAmount;
		}

		//if (Input.GetKey(KeyCode.R))
		//{
		//	newZoom += zoomAmount;
		//}
		//if (Input.GetKey(KeyCode.F))
		//{
		//	newZoom -= zoomAmount;
		//}

		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
	}

	void LateUpdate()
	{
		//Actual Camera Rig Transformations
		Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
		this.rotateRig.rotation = Quaternion.Lerp(this.rotateRig.rotation, QT, Time.deltaTime * OrbitDampening);

		if (this.mainCam.transform.localPosition.z != this._CameraDistance * -1f)
		{
			this.mainCam.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this.mainCam.transform.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
		}
	}

	public void RemoveFollow()
	{
		this.followTransform = null;
	}
}
