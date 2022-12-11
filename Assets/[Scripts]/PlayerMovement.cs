using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public float Speed { get => speed; set => speed = value; }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(X * Speed * Time.fixedDeltaTime, Y * Speed * Time.fixedDeltaTime, 0.0f);
    }
}
