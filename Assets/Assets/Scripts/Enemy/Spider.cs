using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField] private GameObject _acidPrefab;
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }


    public override void Movement()
    {
    }

    public override void Update()
    {
    }

    public void Attack()
    {
        Instantiate(_acidPrefab, transform.position, Quaternion.identity);
    }

    public void Damage()
    {
        if (isDead == true)
            return;
        Health--;
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
