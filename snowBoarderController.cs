//SnowBoarder Controller

using UnityEngine; 
public class SnowboarderController : MonoBehaviour 
{
	public float speed = 10f; 
	public float rotationSpeed = 100f; 
	public float jumpForce = 5f;
	public float groundRaycastDistance = 1f; 
	
	private Rigidbody rb; 
	private bool isGrounded; 
	
	private void Awake() 
	{
		rb = GetComponent<Rigidbody>(); 
	} 
	private void Update() 
	{
		// Handle player input 
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical"); 
		
		// Calculate movement direction
		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical); 
		movement = movement.normalized * speed * Time.deltaTime; 
		
		// Apply movement to the rigidbody 
		rb.AddForce(movement); 
		
		// Rotate the player based on horizontal input 
		float rotation = moveHorizontal * rotationSpeed * Time.deltaTime; 		
		Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f));
		rb.MoveRotation(rb.rotation * deltaRotation); 
		
		// Handle jump input 
		if (Input.GetButtonDown("Jump") && isGrounded)
		{ 
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
		}
 
	}
	
	private void FixedUpdate() 
	{
		
		// Check if the player is grounded 
		isGrounded = CheckGrounded(); 
	}
	
	private bool CheckGrounded() 
	{
		RaycastHit hit; 
		Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f;  // Offset slightly above the ground 
		
		// Perform a raycast downwards to check if the player is touching the ground 
		if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, groundRaycastDistance)) 
		{
			// Adjust the ground detection based on your snowboarding terrain 
			return true; 
		}
		return false; 
	}
 } 
