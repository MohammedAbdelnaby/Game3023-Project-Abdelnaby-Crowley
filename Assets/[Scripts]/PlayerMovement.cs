using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Animator animator;

    private PlayerAnimationState direction;

    public float Speed { get => speed; set => speed = value; }

    private void Start()
    {
        direction = PlayerAnimationState.IDLE_SOUTH;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        UpdateAnimation();
    }

    private void Move()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(X * Speed * Time.fixedDeltaTime, Y * Speed * Time.fixedDeltaTime, 0.0f);
    }

    private void ChangeAnimationDirectionState(PlayerAnimationState state)
    {
        direction = state;
        animator.SetInteger("State", (int)direction);
    }

    private void UpdateAnimation()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");
        Debug.Log(X + " , " + Y);
        //Walking
        //North
        if (Y > 0.0f)
        {
            ChangeAnimationDirectionState(PlayerAnimationState.WALK_NORTH);
        }
        //East
        else if (X > 0.0f)
        {
            ChangeAnimationDirectionState(PlayerAnimationState.WALK_EAST);
        }
        //South
        else if(Y < 0.0f)
        {
            ChangeAnimationDirectionState(PlayerAnimationState.WALK_SOUTH);
        }
        //West
        else if(X < 0.0f)
        {
            ChangeAnimationDirectionState(PlayerAnimationState.WALK_WEST);
        }//IDLE
        else if (X == 0.0f && Y == 0.0f)
        {
            switch (direction)
            {
                case PlayerAnimationState.WALK_NORTH:
                    animator.SetInteger("State", (int)PlayerAnimationState.IDLE_NORTH);
                    break;
                case PlayerAnimationState.WALK_EAST:
                    animator.SetInteger("State", (int)PlayerAnimationState.IDLE_EAST);
                    break;
                case PlayerAnimationState.WALK_SOUTH:
                    animator.SetInteger("State", (int)PlayerAnimationState.IDLE_SOUTH);
                    break;
                case PlayerAnimationState.WALK_WEST:
                    animator.SetInteger("State", (int)PlayerAnimationState.IDLE_WEST);
                    break;
                default:
                    break;
            }
        }

    }
}
