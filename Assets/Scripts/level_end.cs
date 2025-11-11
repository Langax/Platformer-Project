using UnityEngine;

public class level_end : MonoBehaviour
{
    private camera cam_script;
    private PlayerController player_script;
    private bool interpolating = false;
    private GameObject camera_object; // Searches for camera object
    public Vector3 cam_rotation; // We store this as a vector 3 then convert into Quaternion later so we can multiply the value for a smooth transition
    public Vector3 cam_position;
    public bool reverse_rotation = true; // Basically should it rotate left or right when transitioning!
    private Vector3 opposite_rotation; // We use this if reverse_rotation is set to true, if we changed cam_rotation the goal would constantly change leading to errors!
    static float interp_duration = 3; // The amount of time the interpolation should take
    
    private float current_time = interp_duration; // This stores the current elapsed time of the interpolation. the interpolation is triggered by current_time being under it's "max duration"
    private Vector3 cam_starting_position; // So we can interpolate from it's starting position
    private Vector3 cam_starting_rotation; // Same as above for rotation

    private void Start()
    {
        camera_object = GameObject.Find("camera");
        cam_script = camera_object.GetComponent<camera>(); // So we can change the variables from the camera script
        player_script = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (current_time < interp_duration)
        {
            current_time += Time.deltaTime;

            if (current_time >= interp_duration)// This is so the final loop ends with a x1 multiplier instead of something like 1.00168
            { 
                current_time = interp_duration; interpolating = true;
                player_script.can_move = true;
                player_script.canJump = true;
            } 

            float interp_amount = 1/(interp_duration / current_time);  // This basically produces a value between 0 and 1 to tell what stage of the interp we are at and do maths with the positions

            //cam_script.camera_offset_position =  cam_position * interp_amount;
            cam_script.camera_offset_position = (cam_starting_position * (1 - interp_amount)) + (cam_position * interp_amount); // This is a linear interp equasion I made after little brain thinkie moment, interp amount is basically time

            if (reverse_rotation == true)
            {
                cam_script.camera_offset_rotation = Quaternion.Euler((cam_starting_rotation * (1 - interp_amount)) - (opposite_rotation * interp_amount)); // Same equasion just on rotation, btw the 1.0f is so it's a float I was getting some odd errors
            }
            else
            {
                cam_script.camera_offset_rotation = Quaternion.Euler((cam_starting_rotation * (1 - interp_amount)) + (cam_rotation * interp_amount)); // Same equasion just on rotation, btw the 1.0f is so it's a float I was getting some odd errors
            }   
        }
    } // End update

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player") { return;  } // Stops the code if the player isn't the one hitting it
        if (cam_script.camera_offset_position != cam_position && interpolating == false) // checks IF it even needs to run (stops errors happening from reversing saves game lag etc), I did ORIGINALLY have a rotation check in there however Quaternion stores really weird and one of the numbers were negative making it always trigger :c
        {
            player_script.can_move = false;
            player_script.canJump = false;
            interpolating = true;
            cam_starting_position = cam_script.camera_offset_position; // Holds the original starting position
            cam_starting_rotation = cam_script.camera_offset_rotation.eulerAngles; // the .eulearAngles converts it into vector3 for MATHS to happen. okay!
            if (reverse_rotation == true) // Reverses the angles so that it goes to the targeted angle e.g. target is 270 but without this it'd move to 90
            {
                if (cam_rotation.x != 0) { opposite_rotation = new Vector3(360 - cam_rotation.x, cam_rotation.y, cam_rotation.z); }// Getting the inverse rotation as our goal needs to change
                if (cam_rotation.y != 0) { opposite_rotation = new Vector3(cam_rotation.x, 360 - cam_rotation.y, cam_rotation.z); }// these all do it but it needs to check that it's not 0
                if (cam_rotation.z != 0) { opposite_rotation = new Vector3(cam_rotation.x, cam_rotation.y, 360 - cam_rotation.z); }// as 360 - 0 = 360 NOT 0 so it'd spin all around (idk how to get the inverse as this engine doesn't have negative rotations)
            }
            current_time = 0; // This will start the interp
        } 

    }// end collision
}// End class / script
