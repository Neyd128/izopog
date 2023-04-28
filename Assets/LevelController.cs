using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject Zombie;
    public int MaxZombieCount;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnZombie();
    }

    private void SpawnZombie()
    {
        var currentZombieCount = GameObject.FindGameObjectsWithTag("Enemy");

        if (currentZombieCount.Count() < MaxZombieCount)
        {
            var position = GetRandomPosition();

            if (Physics.CheckSphere(position, 5))
            {
                Instantiate(Zombie, position, Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        var x = Random.Range(-10f, 10f);
        var z = Random.Range(-10f, 10f);
        var position = new Vector3(x, 0f, z);

        return position;
    }
}
