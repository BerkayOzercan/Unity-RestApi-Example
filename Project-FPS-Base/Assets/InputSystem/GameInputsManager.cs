using Assets.GameSystem.Scripts;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Assets.InputSystem
{
	public class GameInputsManager : Singleton<GameInputsManager>
	{
		[Header("Character Input Values")]
		public Vector2 Move;
		public Vector2 Look;
		public bool Jump;
		public bool Sprint;
		public bool Fire;
		public bool Escape;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnFire(InputValue value)
		{
			FireInput(value.isPressed);
		}

		public void OnEscape(InputValue value)
		{
			EscapeInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			Move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			Look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			Jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			Sprint = newSprintState;
		}

		public void FireInput(bool isFire)
		{
			Fire = isFire;
		}

		public void EscapeInput(bool isEsc)
		{
			Escape = isEsc;
		}
	}

}