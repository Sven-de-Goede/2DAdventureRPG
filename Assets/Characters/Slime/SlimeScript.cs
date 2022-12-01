using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public GameObject playerManager;
    public float moveSpeed = 0.5f;
    public float hp = 2;
    private Color startColor;
    public float getHitCooldown = 1f;
    public float damagePlayerCooldown = 2f;
    private bool addedScore = false;
    Animator animator;
    SpriteRenderer sprite;
    Collider2D collideBox;

    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", true);
        collideBox = GetComponent<BoxCollider2D>();
    }


    void Update()
    {  
        transform.position = Vector2.MoveTowards(transform.position, PlayerManager.instance.playerPos - new Vector2(0, 0.1f), moveSpeed * Time.deltaTime);
        getHitCooldown -= 0.1f * Time.fixedDeltaTime;
        damagePlayerCooldown -= 0.1f * Time.fixedDeltaTime;

        //kill slime if under 0 hp
        if (hp <= 0)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isDead", true);
            moveSpeed = 0f;
            collideBox.enabled = false;
            Destroy(this.gameObject, 3);
            
            if (addedScore == false)
            {
                PlayerManager.instance.enemiesKilled++;
                addedScore = true;
            }
            
        }
    }

    //check for collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //take damage if hit by sword
        if (collision.gameObject.name == "SwordHitBox") 
        {
            if (getHitCooldown <= 0) 
            {
                hp -= 1 * PlayerManager.instance.damageMultiplier;
                sprite.color = new Color (1, 0, 0, 1);
                Invoke(nameof(ChangeColorBack), 0.2f);
                getHitCooldown = 2f;
            }
        }
        //damage player
        if (collision.gameObject.name == "Player") {
            if (damagePlayerCooldown <= 0)
            {
                PlayerManager.instance.playerHealth -= 1;
                damagePlayerCooldown = 1f;
            }
        }


    }
    

    void ChangeColorBack(){
        sprite.color = startColor;
    }

}
