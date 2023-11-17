

Using UnityEngine;

//keyboard controller

//Use Script to attach to character object.
//Keyboard Movements:
//W-Forward (vertical)
//S-Backward (vertical)
//A-Left (horizontal)
//D-Right (horizontal)

public class CharacterController : MonoBehaviour
{
public float moveSpeed = 5f;
private void Update()
{
//Get keyboard input
float horizontalInput = Input.GetAxis(“Horizontal”);
float verticalInput= Input.GetAxis(“Vertical”);
//Calculate movement direction
Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
//Move the character
transform.Translate(movement *  moveSpeed * Time.deltaTime);
}
}
