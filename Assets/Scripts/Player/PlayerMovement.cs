using Unity.VisualScripting;
using UnityEngine;

/// <summary>Handles player movement and directional animation updates.</summary>

public class PlayerMovement : MonoBehaviour
{

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;

    [SerializeField]
    private PlayerData playerData;

    private Vector2 movement;

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(inputX, inputY);
        movement = input.normalized;

        bool isWalking = input != Vector2.zero;
        animator.SetBool("isWalking", isWalking);



        if (isWalking)
        {
            animator.SetFloat("inputx", input.x);
            animator.SetFloat("inputy", input.y);

            animator.SetFloat("lastInputX", input.x);
            animator.SetFloat("lastInputY", input.y);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playerData.moveSpeed * Time.fixedDeltaTime);
        rb.rotation = 0f;
    }

   
}
