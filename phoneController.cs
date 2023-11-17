

Using UnityEngine;
public class CharacterController : MonoBehaviour
{

//Character Controller: iPhone and Android Phone
//Utilize ‘Input’ Class along with the touch input from the iPhone’s screen.
//To use this script with an iPhone, build and run the Unity project on your iPhone. 
//When you touch and drag your finger on the screen, the character will move accordingly.

//Sideways Vector (yellow)
//Velocity Vector (red)
//Collision Capsule w/ mesh

public float moveSpeed = 5f;
private void Update();
{
if (Input.touchCount > 0)
{
//Get the first touch
Touch touch = Input.GetTouch(0);
//Check if the touch phase is moving
If (touch.phase == TouchPhase.Moved)
{
//Calculate the movement direction
Vector3 movement = new Vector3(touch.deltaPosition.x, 0f, touch.deltaPosition.y).normalized;
//Move the character
Transform.Translate(movement * moveSpeed * Time.deltaTime);
}
}
}
}



