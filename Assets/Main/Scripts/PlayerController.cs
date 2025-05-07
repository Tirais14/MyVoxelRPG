using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	//public static Inventory Inventory {  get; private set; }
	//private string playingAnimation;
	private Vector3 direction;
	private Camera mainCamera;
	private int interactRadiusFoundCollidersCount;
	private Collider[] collidersInInteractRadius;
	private Collider[] foundColliders;
	private GameObject[] interactableObejcts;
	[Header("Components")]
	[SerializeField] private RectTransform interactActionGFX;
	[SerializeField] private Rigidbody rigidBodyComp;
	[SerializeField] private Animator animatorComp;
	[SerializeField] private Character character;
	//[SerializeField] private Inventory inventory;
	[Header("Player Settings")]
	[SerializeField] private float movementSpeed = 6f;
	private float currentMovementSpeed;
	[SerializeField] private float speedMultiplier = 3f;
	[SerializeField] private float turnSmoothTime = 0.3f;
	[SerializeField] private float interactRadius = 10f;
	[Header("Techically")]
	private readonly int maxOverllapCollidersCount = 5;
	private float turnSmoothVelocity;
	private bool isWalking = true;
	private bool isRunning;
	[Range(50f, 1000f)]
	[SerializeField] private float jumpForce = 400f;

	//public void Move()
	//{
	//	float horizontalInput = Input.GetAxisRaw("Horizontal");
	//	float verticalInput = Input.GetAxisRaw("Vertical");

	//	Vector3 direction = new(horizontalInput, 0f, verticalInput);
	//	currentMovementSpeed = direction.normalized.magnitude;

	//	if (direction.magnitude > 0.1f)
	//	{
	//		animatorComp.SetBool("IsMoving", true);
	//		float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
	//		float currentCalculatedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
	//		transform.rotation = Quaternion.Euler(0f, currentCalculatedAngle, 0f);
	//		Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

	//		if (isWalking)
	//		{ characterControllerComp.Move(movementSpeed * Time.deltaTime * moveDirection.normalized); }
	//		else
	//		{ characterControllerComp.Move(movementSpeed * speedMultiplier * Time.deltaTime * moveDirection.normalized); }
	//	}
	//	else
	//	{ animatorComp.SetBool("IsMoving", false); }
	//}
	//public void Move()
	//{
	//	float horizontalInput = Input.GetAxisRaw("Horizontal");
	//	float verticalInput = Input.GetAxisRaw("Vertical");

	//	Vector3 direction = new(horizontalInput, 0f, verticalInput);
	//	currentMovementSpeed = direction.magnitude;

	//	if (direction.magnitude > 0.1f)
	//	{
	//		animatorComp.SetBool("IsMoving", true);
	//		float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
	//		float currentCalculatedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
	//		transform.rotation = Quaternion.Euler(0f, currentCalculatedAngle, 0f);
	//		Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

	//		if (isWalking)
	//		{ rigidBodyComp.MovePosition(movementSpeed * Time.fixedDeltaTime * moveDirection.normalized + transform.position); }
	//		else
	//		{ rigidBodyComp.MovePosition(movementSpeed * speedMultiplier * Time.fixedDeltaTime * moveDirection.normalized + transform.position); }
	//	}
	//	else
	//	{
	//		//rigidBodyComp.velocity = Vector3.zero;
	//		animatorComp.SetBool("IsMoving", false); 
	//	}
	//}

	private void GetInput()
	{
		//float horizontalInput = Input.GetAxisRaw("Horizontal");
		//float verticalInput = Input.GetAxisRaw("Vertical");
		float horizontalInput = GameManagers.Controlls.MoveRightAndLeft.ReadValue<float>();
		float verticalInput = GameManagers.Controlls.MoveForwardAndBackward.ReadValue<float>();

		direction = new(horizontalInput, 0f, verticalInput);
		currentMovementSpeed = direction.magnitude;
	}

	public void Move()
	{
		if (direction.magnitude > 0.1f)
		{
			animatorComp.SetBool("IsMoving", true);
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
			float currentCalculatedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, currentCalculatedAngle, 0f);
			Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			moveDirection = moveDirection.normalized;

			
			if (isWalking)
			{ rigidBodyComp.velocity = new Vector3(0f, rigidBodyComp.velocity.y, 0f) + movementSpeed * moveDirection; }
			else
			{ rigidBodyComp.velocity = new Vector3(0f, rigidBodyComp.velocity.y, 0f) + movementSpeed * speedMultiplier * moveDirection; }
		}
		else
		{
			if (animatorComp.GetBool("IsMoving"))
			{
				rigidBodyComp.velocity = new Vector3(0f, rigidBodyComp.velocity.y, 0f);
				animatorComp.SetBool("IsMoving", false); 
			}
		}
	}

#pragma warning disable IDE0060 // Удалите неиспользуемый параметр
	public void Attack(InputAction.CallbackContext context)
	{
		Quaternion cameraRotation = new Quaternion(0f, mainCamera.transform.rotation.y, 0f, mainCamera.transform.rotation.w);
		transform.SetPositionAndRotation(transform.position, cameraRotation);
		character.Attack();
	}

	internal void Jump(InputAction.CallbackContext context)
	{
		rigidBodyComp.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
	}

	public void MoveForwardAndBackward(InputAction.CallbackContext context)
	{
		//float buttonValue = context.ReadValue<float>();
		//Debug.Log(buttonValue);
		//direction.x = buttonValue;
	}

	public void MoveRightAndLeft(InputAction.CallbackContext context)
	{
		//float buttonValue = context.ReadValue<float>();
		//Debug.Log(buttonValue);
		//direction.z = buttonValue;
	}

	public void SwitchWalkingToRun(InputAction.CallbackContext context)
	{
		isWalking = !isWalking;
		isRunning = !isRunning;
		animatorComp.SetBool("Walking", isWalking);
		animatorComp.SetBool("Running", isRunning);
	}
#pragma warning restore IDE0060 // Удалите неиспользуемый параметр

	private void FindInteractableObejcts()
	{
		int foundColliderCount = Utils.FindInCollidersByComponentNonAloc<Interactable>(
			collidersInInteractRadius,
			interactRadiusFoundCollidersCount,
			foundColliders);

		if (interactableObejcts.Count() > 0)
		{ Array.Clear(interactableObejcts, 0, interactableObejcts.Length); }

		for (int i = 0; i < foundColliderCount; i++)
		{ interactableObejcts[i] = foundColliders[i].gameObject; Debug.Log("game object found"); }
	}

	private void FindCollidersInInteractRadius()
	{
		int layerBitMask = 1 << 6;
		if (interactRadiusFoundCollidersCount > 0)
		{ Array.Clear(collidersInInteractRadius, 0, collidersInInteractRadius.Length); }
		interactRadiusFoundCollidersCount = Physics.OverlapSphereNonAlloc(transform.position,
																	interactRadius,
																	collidersInInteractRadius,
																	layerBitMask);
	}

	public void Initialize()
	{
		mainCamera = Camera.main;
		rigidBodyComp = GetComponent<Rigidbody>();
		collidersInInteractRadius = new Collider[maxOverllapCollidersCount];
		foundColliders = new Collider[maxOverllapCollidersCount];
		interactableObejcts = new GameObject[maxOverllapCollidersCount];
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(Vector3.zero, interactRadius);
	}

	private void Update()
	{
		animatorComp.SetFloat("MovementSpeed", currentMovementSpeed);

		GetInput();

		rigidBodyComp.AddForce(0f, -60f, 0f, ForceMode.Force);
		//Physics.Raycast(GlobalProperties.FromScreenCenterRay, out RaycastHit hitInfo, interactRadius);
		//if (hitInfo.transform != null && hitInfo.transform.TryGetComponent(out Interactable interactable))
		//{
		//	Debug.Log("Hitted");
		//}
	}

	private void FixedUpdate()
	{
		//AnimationSwticher();
		Move();
		FindCollidersInInteractRadius();
		FindInteractableObejcts();
	}
}
