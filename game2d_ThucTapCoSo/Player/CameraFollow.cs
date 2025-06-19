using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;
    public float yOffset = 1f;
    private void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y+yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position,newPos,FollowSpeed*Time.deltaTime);
    }
}
