using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] public int gems;
    [SerializeField] protected Transform pointA, pointB;

    public GameObject diamondPrefab;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected Player player;
    protected bool isHit = false;
    protected bool isDead = false;
    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
       
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == true)
        {
            return;
        }
        if (isDead == false)
        { 
            Movement();
        }
    }

    public virtual void Movement()
    {
        if(currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else if(currentTarget == pointB.position)
        {
            sprite.flipX = false;
        }
        if (transform.position == pointA.position)
        {

            anim.SetTrigger("Idle");
            currentTarget = pointB.position;
        }
        else if (transform.position == pointB.position)
        {

            anim.SetTrigger("Idle");
            currentTarget = pointA.position; 
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if(distance > 2.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }
        if(isHit == false) { 
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        Vector3 direction = player.transform.localPosition - transform.localPosition;
        if (direction.x > 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = false;
        }
        else if (direction.x < 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = true;
        }



    }

    


}
