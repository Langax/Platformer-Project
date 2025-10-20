using UnityEngine;
using System.Collections;

public class level_end : MonoBehaviour
{
    public GameObject cam_object;
    private camera cam_script;
    public Vector3 cam_rotation; // We store this as a vector 3 then convert into Quaternion later so we can multiply the value for a smooth transition
    public Vector3 cam_position;

    private void Start()
    {
        cam_script = cam_object.GetComponent<camera>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("You hit the end!");

        float currentTime = 0;
        while (currentTime < 1)
        {
            var someValueFrom0To1 = currentTime / 1;
            currentTime += Time.deltaTime;

            if (currentTime >= 1) {currentTime = 1;} // This is so the final loop ends with a x1 multiplier instead of something like 1.00168

            cam_script.camera_offset_position = cam_position * currentTime;
            cam_script.camera_offset_rotation = Quaternion.Euler(cam_rotation * currentTime);
        }
        
        // cam_script.camera_offset_position = cam_position;
         //cam_script.camera_offset_rotation = cam_rotation;
    }
}
