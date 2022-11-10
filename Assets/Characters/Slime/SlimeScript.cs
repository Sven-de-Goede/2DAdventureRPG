using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public GameObject playerManager;
    public float moveSpeed = 0.6f;
    private Vector2 followPoint;
    private float hitCooldown = 0f;
    private int slimeHealth = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        followPoint = PlayerManager.instance.playerPos - new Vector2(0, 0.13f);
        transform.position = Vector2.MoveTowards(transform.position, followPoint, moveSpeed * Time.deltaTime);

        hitCooldown -= (1f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerTrigger"))
        {
            if (hitCooldown <= 0) { 
                DamagePlayer(10); 
            }            
        }
    }

    public void DamagePlayer(int Damage)
    {
        PlayerManager.instance.playerHealth -= Damage;
        hitCooldown = 1f;   
    }

}
