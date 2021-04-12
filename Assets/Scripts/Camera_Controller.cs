using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offsetPos;
    public float followSpeed;
    public float xMin;
    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        offsetPos = new Vector3(0, 2, -10);
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = playerTransform.position + offsetPos;
        Vector3 clampedPos = new Vector3(Mathf.Clamp(targetPos.x, xMin, float.MaxValue), targetPos.y, targetPos.z);
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, clampedPos, ref velocity, followSpeed * Time.fixedDeltaTime);

        this.transform.position = smoothPos;
    }
}
