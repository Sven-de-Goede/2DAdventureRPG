using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScript : MonoBehaviour
{
    Animator animator;
    public GameObject swordHitBox;
    public float swordCooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {        
        swordHitBox = transform.Find("SwordHitBox").gameObject; 
        animator = GetComponent<Animator>();
   
    }

    // Update is called once per frame
    void Update()
    {
        swordCooldown -= 1f * Time.deltaTime;
    }

private void OnTriggerEnter2D(Collider2D collision)
    {

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

    //function to activate sword hitbox
    public IEnumerator activateSwordHitbox(float activeTime)
   {
      swordHitBox.SetActive(true);
      yield return new WaitForSeconds(activeTime);
      swordHitBox.SetActive(false);
   }
}
