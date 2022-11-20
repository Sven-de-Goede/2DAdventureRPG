using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    public GameObject playerManager;
    public float swordCooldown = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public GameObject swordHitBox;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordHitBox = transform.Find("SwordHitBox").gameObject;
        playerManager = GameObject.Find("PlayerManager");
    }

    private void Update(){
        swordCooldown -= 1f * Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // If movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {

                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            // Set direction of sprite to movement direction
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
                swordHitBox.transform.position = transform.position - new Vector3 (0.2f, 0, 0);
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
                swordHitBox.transform.position = transform.position;
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                PlayerManager.instance.moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * PlayerManager.instance.moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Can't move if there's no direction to move in
            return false;
        }

    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }


    //on left click
    void OnFire()
    {
        if(swordCooldown <= 0)
        {
        animator.SetTrigger("swordAttack");
        swordCooldown = 1f;
        StartCoroutine(activateSwordHitbox(0.5f));
        }
    }

    public void LockMovement()
    {
        canMove = false;
    }
    public void UnlockMovement()
    {
        canMove = true;
    }

    //function to activate sword hitbox
    public IEnumerator activateSwordHitbox(float activeTime)
   {
      swordHitBox.SetActive(true);
      yield return new WaitForSeconds(activeTime);
      swordHitBox.SetActive(false);

   }

}
