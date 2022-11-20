using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public GameObject playerManager;
    public float moveSpeed = 0.5f;
    public float hp = 1;
    private Color startColor;
    public float getHitCooldown = 1f; 
    SpriteRenderer sprite;


    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;

    }


    void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, PlayerManager.instance.playerPos - new Vector2(0, 0.1f), moveSpeed * Time.deltaTime);
        getHitCooldown -= 0.1f * Time.fixedDeltaTime;
    }

    //check for collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SwordHitBox") 
        {
            if (getHitCooldown <= 0) 
            {
                hp -= 1;
                sprite.color = new Color (1, 0, 0, 1);
                Invoke(nameof(ChangeColorBack), 0.2f);
                getHitCooldown = 2f;
            }

        }
    }
    
    void ChangeColorBack(){
        sprite.color = startColor;
    }
}
