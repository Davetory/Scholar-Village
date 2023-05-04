using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorAnimator : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    Vector3 door;
    Vector3 player;

    // Update is called once per frame
    void Update()
    {
        door = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform.position;

    }

    void FixedUpdate()
    {
        if (Mathf.Abs(door.x - player.x) <= 1.5 & Mathf.Abs(door.y - player.y) <= 2.5)
        animator.SetBool("isNear", true);
        else animator.SetBool("isNear", false);
    }
}
