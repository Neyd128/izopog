using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float hp = 10;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20;
    public float playerSpeed = 2;
    private Vector2 movementVector;
    private Transform bulletSpawn;
    public GameObject hpBar;
    private Scrollbar hpScrollBar;
    private NavMeshAgent NavMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        movementVector = Vector2.zero;
        bulletSpawn = transform.Find("BulletSpawn");
        hpScrollBar = hpBar.GetComponent<Scrollbar>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * movementVector.x);
        //transform.Translate(Vector3.forward * movementVector.y * Time.deltaTime * playerSpeed);
        if (movementVector.y > 0)
        {
            NavMeshAgent.isStopped = false;
            NavMeshAgent.destination = transform.position + transform.forward;
        }

        if (movementVector.y is 0)
        {
            NavMeshAgent.isStopped = true;
        }
    }
    
    private void OnMove(InputValue inputValue) 
    {
        movementVector = inputValue.Get<Vector2>();

        //Debug.Log(movementVector.ToString());
    }

    private void OnFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*bulletSpeed, ForceMode.VelocityChange);
        Destroy(bullet, 5);
    }

    private void Die()
    {
        //GetComponent<BoxCollider>().enabled = false;
        //transform.Translate(Vector3.up);
        //transform.Rotate(Vector3.right * -90);

        //Time.timeScale = 0;
        SceneManager.LoadScene("Restart");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            hp--;
            if (hp <= 0) Die();
            hpScrollBar.size = hp / 10;
            Vector3 pushVector = other.gameObject.transform.position - transform.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(pushVector.normalized * 5, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("Heal"))
        {
            hp = 10;
            hpScrollBar.size = hp / 10;
            Destroy(other.gameObject);
        }
    }
}
