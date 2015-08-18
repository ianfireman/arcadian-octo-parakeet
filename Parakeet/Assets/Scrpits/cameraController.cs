using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	Vector3 hit_position = Vector3.zero;
	Vector3 current_position = Vector3.zero;
	Vector3 camera_position = Vector3.zero;
	
	
	public float mapWidth = 60.0f;
	public float mapHeight = 350.0f;
	public float mapDeep = 60.0f;
	private float minX, maxX, minY, maxY;
	
	
	public float perspectiveZoomSpeed = 0.5f;
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	void Update(){
		
		Touch[] touches = Input.touches;
		
		if(Input.GetMouseButtonDown(0)){
			hit_position = Input.mousePosition;
			camera_position = transform.position;
		}
		
		if (touches.Length == 2) { //ZOOM
			
			// If there are two touches on the device...
			if (Input.touchCount == 2)
			{
				
				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);
				
				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
				
				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
				
				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
				
				
				// Otherwise change the field of view based on the change in distance between the touches.
				GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				
				// Clamp the field of view to make sure it's between 0 and 180.
				GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 29.9f, 58.9f);
				
			}
		} else { 
			
			if (Input.GetMouseButton (0)) { // MOVIMENTO
				current_position = Input.mousePosition;
				LeftMouseDrag ();
				
			}
		}
		
		
	}
	
	void LeftMouseDrag(){
		
		// From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
		// with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
		
		current_position.z = hit_position.z = camera_position.y;
		
		
		// Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
		// anyways.  
		Vector3 direction = Camera.main.ScreenToWorldPoint (current_position) - Camera.main.ScreenToWorldPoint (hit_position);
		
		
		// Invert direction to that terrain appears to move with the mouse.
		direction = direction * -1;
		
		Vector3 position = camera_position + direction;
		position.y = camera_position.y;
		//veriphy ();
		transform.position = position;
		
	}
	
	void zoom(Touch[] touches){
		Touch touchOne = touches[0];
		Touch touchTwo = touches[1];
	}
	
	void veriphy(){
		if (camera_position.x > mapWidth) {
			camera_position.x = mapWidth - 1;
			
		} else {
			if(camera_position.z > mapHeight){
				camera_position.z = mapHeight - 1;
			} else {
				if(camera_position.x < (mapWidth * -1)){
					camera_position.x = (mapWidth * -1) + 1;
					
				} else {
					if(camera_position.z < 80){
						camera_position.z = 81;
						
					}
				}
			}
		}
		
	}
	
	
}