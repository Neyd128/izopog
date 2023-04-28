using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    public int HP;
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var seesPlayer = false;
        var hearsPlayer = false;
        
        var playerVector = player.transform.position - transform.position;

        Physics.Raycast(transform.position, playerVector, out var hit);

        if (hit.collider.gameObject.CompareTag("Player"))
        {
            seesPlayer = true;
        }

        var nearby = Physics.OverlapSphere(transform.position, 5);

        foreach(var collider in nearby)
        {
            if (collider.transform.CompareTag("Player"))
            {
                hearsPlayer = true;
            }
        }

        if (seesPlayer || hearsPlayer)
        {
            navMeshAgent.destination = player.transform.position;
        }
    }

    public void ReciveDamage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die() => Destroy(gameObject);

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Bullet"))
    //    {
    //        Destroy(collision.gameObject);
    //        HP--;
    //        if(HP <= 0)
    //        {
    //            collision.gameObject.tag = string.Empty;
    //            transform.Translate(Vector3.up);
    //            transform.Rotate(Vector3.right * -90);
    //            GetComponent<BoxCollider>().enabled = false;
    //            Destroy(transform.gameObject, 10);
    //        }
    //    }
    //}
}
