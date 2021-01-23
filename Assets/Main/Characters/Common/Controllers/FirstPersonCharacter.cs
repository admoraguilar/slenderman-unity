using System;
using UnityEngine;

namespace Slenderman.Characters
{
	[RequireComponent(typeof(CharacterController))]
	public class FirstPersonCharacter : MonoBehaviour
	{
		[Serializable]
		public class Movement
		{
			public float speed = 10f;
			public float gravity = -9.8f;

			private InputAxis _movementInput = new InputAxis();
			private Vector3 _moveDirection = Vector3.zero;
			private Vector3 _gravityDirection = Vector3.zero;

			private FirstPersonCharacter _controller = null;

			internal void Init(FirstPersonCharacter controller)
			{
				_controller = controller;
				_movementInput.SetAxisSources(
					() => Input.GetAxisRaw("Horizontal"),
					() => Input.GetAxisRaw("Vertical"));
			}

			public void Update()
			{
				_movementInput.Update();
				_moveDirection = _controller.transform.TransformDirection(new Vector3(_movementInput.axis.x, 0f, _movementInput.axis.y));

				_gravityDirection = Vector3.zero;
				if(!_controller.characterController.isGrounded) {
					_gravityDirection.y = gravity;
				}
			}

			public void FixedUpdate()
			{
				if(_movementInput.IsActive()) {
					_controller.characterController.Move((_moveDirection + _gravityDirection) * speed * Time.deltaTime);
				}
			}
		}

		[Serializable]
		public class Rotation
		{
			public Transform pOV = null;
			public float speed = 120f;

			public bool isXConstraint = true;
			public float minXConstraint = -90f;
			public float maxXConstraint = 80f;

			private InputAxis _rotationInput = new InputAxis();

			private FirstPersonCharacter _controller = null;

			internal void Init(FirstPersonCharacter controller)
			{
				_controller = controller;
				_rotationInput.SetAxisSources(
					() => Input.GetAxisRaw("Mouse X"),
					() => Input.GetAxisRaw("Mouse Y"));
			}

			public void Update()
			{
				_rotationInput.Update();
			}

			public void FixedUpdate()
			{
				if(_rotationInput.IsActive()) {
					Vector3 transformRotation = _controller.transform.localEulerAngles;
					transformRotation.y += _rotationInput.axis.x * speed * Time.deltaTime;

					Vector3 pOVRotation = pOV.localEulerAngles;
					pOVRotation.x += _rotationInput.axis.y * speed * Time.deltaTime;

					_controller.transform.localRotation = Quaternion.RotateTowards(
						_controller.transform.localRotation, Quaternion.Euler(transformRotation), 10f);

					if(_rotationInput.axis.y != 0) {
						float x = To180(pOVRotation.x);
						if(x < maxXConstraint && x >= minXConstraint) {
							pOV.localRotation = Quaternion.RotateTowards(
								pOV.localRotation, Quaternion.Euler(pOVRotation), Mathf.Infinity);
						}
					}
				}
			}

			private float To180(float value)
			{
				if(value > 180 && value <= 360) { return value - 360; }
				else if(value > 360) { return value - 360; }
				else { return value; }
			}
		}

		[Serializable]
		public class InputAxis
		{
			public Vector2 axis { get; private set; }
			
			private Func<float> _xAxis = null;
			private Func<float> _yAxis = null;

			public InputAxis() { }

			public InputAxis(Func<float> xAxis, Func<float> yAxis)
			{
				SetAxisSources(xAxis, yAxis);
			}

			public bool IsActive() => axis != Vector2.zero;

			public void SetAxisSources(Func<float> xAxis, Func<float> yAxis)
			{
				_xAxis = xAxis;
				_yAxis = yAxis;
			}

			public void Update() => axis = new Vector2(_xAxis(), _yAxis());
		}

		[SerializeField]
		private Movement _movement;

		[SerializeField]
		private Rotation _rotation;

		public new Transform transform => this.GetComponent(ref _transform);
		private Transform _transform = null;

		public CharacterController characterController => this.GetComponent(ref _characterController);
		private CharacterController _characterController = null;

		private void Start()
		{
			_movement.Init(this);
			_rotation.Init(this);
		}

		private void Update()
		{
			_movement.Update();
			_rotation.Update();
		}

		private void FixedUpdate()
		{
			_movement.FixedUpdate();
			_rotation.FixedUpdate();
		}
	}
}
