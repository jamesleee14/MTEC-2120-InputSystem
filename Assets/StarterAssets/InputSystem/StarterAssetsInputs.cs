using UnityEngine;
using System.Collections;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		public float burstSpeed;
		public GameObject projectile;

		private bool m_Charging;



		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		//public void OnMove(InputValue value)
		//{
		//	MoveInput(value.Get<Vector2>());
		//}

		public void OnMove(InputAction.CallbackContext context)
		{
			MoveInput(context.ReadValue<Vector2>());
		}


		//public void OnLook(InputValue value)
		//{
		//	if(cursorInputForLook)
		//	{
		//		LookInput(value.Get<Vector2>());
		//	}
		//}

		public void OnLook(InputAction.CallbackContext context)
		{
            if (cursorInputForLook)
            {
                LookInput(context.ReadValue<Vector2>());
            }

        }



		//public void OnJump(InputValue value)
		//{
		//	JumpInput(value.isPressed);
		//}

		public void OnJump(InputAction.CallbackContext context)
		{
			JumpInput(context.ReadValue<float>()==1);
		}


		//public void OnSprint(InputValue value)
		//{
		//	SprintInput(value.isPressed);
		//}

		public void OnSprint(InputAction.CallbackContext context)
		{
			SprintInput(context.ReadValue<float>()==1);
		}


		public void OnFire(InputAction.CallbackContext context)
		{
			switch (context.phase)
			{
				case InputActionPhase.Performed:
					if (context.interaction is SlowTapInteraction)
					{
						StartCoroutine(BurstFire((int)(context.duration * burstSpeed)));
					}
					else
					{
						Fire();
					}
					m_Charging = false;
					break;

				case InputActionPhase.Started:
					if (context.interaction is SlowTapInteraction)
						m_Charging = true;
					break;

				case InputActionPhase.Canceled:
					m_Charging = false;
					break;
			}
		}

		public void OnGUI()
		{
			if (m_Charging)
				GUI.Label(new Rect(100, 100, 200, 100), "Charging...");
		}
#endif



		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}



		private IEnumerator BurstFire(int burstAmount)
		{
			for (var i = 0; i < burstAmount; ++i)
			{
				Fire();
				yield return new WaitForSeconds(0.1f);
			}
		}

		private void Fire()
		{
			var transform = this.transform;
			var newProjectile = Instantiate(projectile);
			newProjectile.transform.position = transform.position + transform.forward * 0.6f + transform.up;
			newProjectile.transform.rotation = transform.rotation;
			const int size = 1;
			newProjectile.transform.localScale *= size;
			newProjectile.GetComponent<Rigidbody>().mass = Mathf.Pow(size, 3);
			newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
			newProjectile.GetComponent<MeshRenderer>().material.color =
				new Color(Random.value, Random.value, Random.value, 1.0f);
		}
	}
	
}