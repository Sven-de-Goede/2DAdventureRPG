using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public GameObject playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerManager.instance.playerPos;


        Collider2D[] coll = Physics2D.OverlapCircleAll(PlayerManager.instance.playerPos, 10f);
        if (coll.Length >= 1)
        {
            float shortestDistanceSoFar = Vector2.Distance(this.gameObject.transform.position, coll[0].gameObject.transform.position);
            GameObject closestObject = coll[0].gameObject;

            for (int i = 0; i < coll.Length; i++)
            {
                float currentDistance = Vector2.Distance(PlayerManager.instance.playerPos, coll[i].gameObject.transform.position);
                if (currentDistance < shortestDistanceSoFar)
                {
                    closestObject = coll[i].gameObject;
                    shortestDistanceSoFar = currentDistance;
                }
                Debug.Log(closestObject);
            }

        }

    }
    private void fire()
    {

    }
}

