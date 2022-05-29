using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
	public Vector2 Move;
	public Vector2 Look;
	public bool Sprint;
	public bool Aim;
	public bool Shoot;
	public bool OpenDoor;
	public bool Reload;
	public bool Suntregun_Turret;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	

	

	public void MoveInput(Vector2 newMoveDirection)
	{
		Move = newMoveDirection;
	}
	public void LookInput(Vector2 newLookDirection)
	{
		Look = newLookDirection;
	}
	public void SprintInput(bool newSprintState)
	{
		Sprint = newSprintState;
	}

	public void ShootInput(bool newShootState)
	{
		Shoot = newShootState;
	}

	public void AimInput(bool newAimState)
	{
		Aim = newAimState;
	}

	public void OpenDoorInput(bool newOpenDoorState)
    {
		OpenDoor = newOpenDoorState;

	}public void ReloadInput(bool ReloadState)
    {
		Reload = ReloadState;

	}public void Suntregun_TurretInput(bool Suntregun_TurretState)
    {
		Suntregun_Turret = Suntregun_TurretState;

	}
	/*--------------------------------*//*--------------------------------*//*--------------------------------*/
	public void OnShoot(InputValue value)
	{
		ShootInput(value.isPressed);
	}
	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnAim(InputValue value)
	{
		AimInput(value.isPressed);
	}

	public void OnLook(InputValue value)
	{
			LookInput(value.Get<Vector2>());	
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}

	public void OnOpenDoor(InputValue value)
	{
		OpenDoorInput(value.isPressed);
	}
	public void OnReload(InputValue value)
	{
		ReloadInput(value.isPressed);
	}public void OnSuntregun_Turret(InputValue value)
	{
		Suntregun_TurretInput(value.isPressed);
	}
	 


	//display Mouse
	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
