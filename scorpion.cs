using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scorpion : MonoBehaviour
{

    public Transform[] patrolPoints;
    public float speed;
    int currentPointIndex;

    public int damage;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = patrolPoints[0].position;
        transform.rotation = patrolPoints[0].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Allows the enemy to patrol from one patrolpoint to another
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        if (transform.position == patrolPoints[currentPointIndex].position)
        {
                transform.rotation = patrolPoints[currentPointIndex].rotation;
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex++;
                }
                else
                {
                    currentPointIndex = 0;
                }                          
        }
    }

    //Allows the player to take damage when collide with enemy box collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Bandit>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Invoke("ScoAfterDeathTimer", 1);
            Destroy(gameObject);
        }
    }
    public void ScoAfterDeathTimer()
    {
        Destroy(gameObject);
    }
}

