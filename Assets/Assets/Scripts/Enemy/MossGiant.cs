using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();

    }

    public void Damage()
    {
        if (isDead == true)
            return;
        isHit = true;
        anim.SetTrigger("Hit");
        Health--;
        anim.SetBool("InCombat", true);
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            Destroy(this.gameObject, 2.0f);

            GameObject diamond = (GameObject)Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().value = base.gems;
        }
    }
}
