using BulletStuff;
using UnityEngine;
using UnityEngine.Events;


public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType
    {
        Straight, 
        Spin, 
        SpinReversed
    }
    
    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;
    public Color bulletsColor = Color.white;
    
    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    
    private GameObject spawnedBullet;
    private float timer = 0f;

    public static UnityAction<GameObject, float, float, Color> OnInstantiateBullet;
    
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) 
            transform.eulerAngles = new Vector3(0f,0f,transform.eulerAngles.z+1f);
        if (spawnerType == SpawnerType.SpinReversed) 
            transform.eulerAngles = new Vector3(0f,0f,transform.eulerAngles.z-1f);
        
        if (timer >= firingRate) 
        {
            Fire();
            timer = 0;
        }
    }
    
    private void Fire() 
    {
        if (bullet) 
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            OnInstantiateBullet.Invoke(spawnedBullet, speed, bulletLife, bulletsColor);
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}