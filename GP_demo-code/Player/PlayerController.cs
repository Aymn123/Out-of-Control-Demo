using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
	[Header("Player")]
	public float MoveSpeed = 2.0f;
	public float SprintSpeed = 5.335f;
	[Range(0.0f, 0.3f)]
	public float RotationSmoothTime = 0.12f;
	public float SpeedChangeRate = 10.0f;
	public float Sinsevity;
	bool _rotateOnMove = true;
	[Header("Cinemachine")]
	public GameObject CinemachineCameraTarget;
	public float TopClamp = 70.0f;
	public float BottomClamp = -30.0f;
	public float CameraAngleOverride = 0.0f;
	public bool LockCameraPosition = false;
	// cinemachine
	private float _cinemachineTargetX;
	private float _cinemachineTargetY;
	// player
	private float _speed;
	private float _targetRotation = 0.0f;
	private float _rotationVelocity;
	float grv = -9.8f;
	private Input _input;
	private CharacterController _controller;
	private GameObject _mainCamera;
	private Animator _animator;
	float currentHorizontalSpeed;
	Vector3 targetDirection;
	public int Money = 0;
	[SerializeField] UI_Handler _ui;
	public HealthSystem _healthSystem;
	[SerializeField] GameObject Suntregun_Turret;
	[SerializeField] public bool TakeButton_FromGround=false;
	public float targetSpeed;
	private void Awake()
	{
		// get a reference to our main camera
		if (_mainCamera == null)
		{
			_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		}
		
	}
	void Start()
	{
		_input = GetComponent<Input>();
		_controller = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
		
		_healthSystem = new HealthSystem(100);

	}

	// Update is called once per frame
	void Update()
	{
		Move();
		
		//Physics.gravity = new Vector3(0, -1.0F, 0);
		//Debug.Log(_healthSystem.GetHealth());
		die();
		
		_ui.UI_Money(Money);

		Suntregun(Suntregun_Turret);
	}
	private void LateUpdate()
	{
		CameraRotation();
		
	}

	private void CameraRotation()
	{
		

		_cinemachineTargetX += _input.Look.x * Time.deltaTime * Sinsevity;
		_cinemachineTargetY += _input.Look.y * Time.deltaTime * Sinsevity;


		// clamp our rotations so our values are limited 360 degrees
		_cinemachineTargetX = ClampAngle(_cinemachineTargetX, float.MinValue, float.MaxValue);
		_cinemachineTargetY = ClampAngle(_cinemachineTargetY, BottomClamp, TopClamp);

		// Cinemachine will follow this target
		CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetY, _cinemachineTargetX, 0.0f);
		
	}
	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

	

	void Move()
	{
        if (_input.Shoot || _input.Aim)
        {
			targetSpeed = 1.0f;
		}
        else
        {
			targetSpeed = _input.Sprint ? SprintSpeed : MoveSpeed;
		}
		if (_input.Move == Vector2.zero) targetSpeed = 0f;
		//Move the player
		Vector3 MoveDir = new Vector3(_input.Move.x, 0, _input.Move.y) * Time.deltaTime * targetSpeed;
		_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, SpeedChangeRate);

		currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0f, _controller.velocity.z).magnitude;
		//Debug.Log(currentHorizontalSpeed);


		//rotation with the base  in our camera when player moving
		if (_input.Move != Vector2.zero)
		{
			//_mainCamera.transform.eulerAngles.y reltave to wrold space start from 0 end 360 dgree
			_targetRotation = Mathf.Atan2(MoveDir.x, MoveDir.z) * Mathf.Rad2Deg+ _mainCamera.transform.eulerAngles.y;
			float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
			// rotate to face input direction relative to camera positionw
			if (_rotateOnMove)
			{
				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}
		}
		targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
		_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime)+new Vector3(0,grv*Time.deltaTime,0));
		_animator.SetFloat("Speed", _speed);
	}

	public void setSinsevity(float newSinsevity)
	{
		Sinsevity = newSinsevity;
	}
	public void SetRotateOnMove(bool newRotateOnMove)
	{
		_rotateOnMove = newRotateOnMove;
	}

	void die()
    {
        if (_healthSystem.GetHealth() <= 0)
        {
			_animator.SetBool("Die", true);
			GetComponent<PlayerControllerShooter>().enabled = false;
			enabled = false;
			GetComponent<AK>().enabled = false;

		}
    }
	public void CalculatorMoney(int money)
    {
		Money -= money;
    }

	void FootSteps_Sound()
    {
		SoundManager.PlaySound("FootSteps_Sound");
    }

	void Suntregun(GameObject Suntregun_Turret)
    {
		if(Money>=30000)
        if (_input.Suntregun_Turret)
        {
			Instantiate(Suntregun_Turret, (new Vector3(transform.position.x, transform.position.y+0.303f, transform.position.z)), transform.rotation);
			_input.Suntregun_Turret = false;
				Money -= 30000;
		}
		
    }

}
