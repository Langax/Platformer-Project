using UnityEngine;
using System.Collections;

public class level_end : MonoBehaviour
{
    public GameObject cam_object;
    private camera cam_script;
    public Quaternion cam_rotation;
    public Vector3 cam_position;

    private void Start()
    {
        cam_script = cam_object.GetComponent<camera>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("You hit the end!");
        cam_script.camera_offset_position = cam_position;
        cam_script.camera_offset_position = cam_position;
    }
}
