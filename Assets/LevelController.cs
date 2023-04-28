using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject Zombie;
    public GameObject Heal;
    public int MaxHealCount;
    public int MaxZombieCount;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnZombie();
        SpawnHeal();

    }

    private void SpawnZombie()
    {
        var currentZombieCount = GameObject.FindGameObjectsWithTag("Enemy");

        if (currentZombieCount.Count() < MaxZombieCount)
        {
            
            var position = GetRandomPosition(0);


            if (Physics.CheckSphere(position, 5))
            {
                Instantiate(Zombie, position, Quaternion.identity);
            }
        }
    }

    private void SpawnHeal()
    {
        var currentHealCount = GameObject.FindGameObjectsWithTag("Heal");

        if (currentHealCount.Count() < MaxHealCount)
        {
            var position = GetRandomPosition(1);

            if (Physics.CheckSphere(position, 5))
            {
                Instantiate(Heal, position, Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandomPosition(int y)
    {
        var x = Random.Range(-10, 10);
        var z = Random.Range(-10, 10);
        var position = new Vector3(x, y, z);
        return position;
    }
}
