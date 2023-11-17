
Using UnityEngine;
public class CharacterController : MonoBehaviour
{


//To use this script with an Xbox controller, make sure the controller is connected to your computer. 
//In Unity, go to Edit ->Project Settings->Input to configure the input axes.
// Set the ‘horizontalAxis’ and ‘verticalAxis’ variables in the script to the corresponding input axis names defined in the input settings. Attach script to character object in the Unity Editor and the character will move based on the input from the Xbox controller.




//XBox Controller Movements: Left ThumbStick

public float moveSpeed = 5f;
public string horizontalAxis = “Horizontal”;
public string verticalAxis = “Vertical”;

private void Update()
{
//Get controller input
float horizontalInput = Input.GetAxis(horizontalAxis);
float verticalInput = Input.GetAxis(verticalAxis);

//Calculate movement direction
Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
//Move the character
transform.Translate(movement *  moveSpeed * Time.deltaTime);
}
}

