using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
private GameObject player;
public static PlayerManager instance;
public Vector2 playerPos;

//Statistics trackers
public int enemiesKilled = 0;

//player stats
public int playerHealth = 3;
public float moveSpeed = 1f;
public float damageMultiplier = 1f;

private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
    }
}
