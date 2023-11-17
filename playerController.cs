//Player Controller

using UnityEngine; 
{
public class PlayerController : MonoBehaviour 
{ 
public float speed = 10f; 
public float rotationSpeed = 100f; 
private Rigidbody rb;
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
// Apply movement to the rigidbody rb.MovePosition(transform.position + movement);
 // Rotate the player based on horizontal input float rotation = moveHorizontal * rotationSpeed * Time.deltaTime; 
Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f)); 
rb.MoveRotation(rb.rotation * deltaRotation); 
}
 }

