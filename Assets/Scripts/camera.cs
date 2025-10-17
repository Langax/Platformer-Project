using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player;
    public Vector3 camera_offset_position;
    public Quaternion camera_offset_rotation;
    private Vector3 target_position;

    private void Start()
    {
        camera_offset_position = transform.position;
        camera_offset_rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        target_position = player.transform.position + camera_offset_position;
        transform.position = target_position;
        transform.rotation = camera_offset_rotation;
    }
}
