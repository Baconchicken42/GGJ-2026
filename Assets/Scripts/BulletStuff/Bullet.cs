using System;
using UnityEngine;

namespace BulletStuff
{
    public class Bullet : MonoBehaviour
    {
        public float bulletLife = 1f; // Defines how long before the bullet is destroyed
        public float rotation = 0f;
        public float speed = 1f;
        
        [SerializeField] private SpriteRenderer bulletSprite;

        private Vector2 spawnPoint;
        private float timer = 0f;

        void OnEnable()
        {
            BulletSpawner.OnInstantiateBullet += SetUpBullet;
            
            spawnPoint = new Vector2(transform.position.x, transform.position.y);
        }

        private void OnDestroy()
        {
            BulletSpawner.OnInstantiateBullet -= SetUpBullet;
        }

        void Update()
        {
            if (timer > bulletLife)
                Destroy(gameObject);

            timer += Time.deltaTime;
            transform.position = Movement(timer);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.name.Equals("Player")) 
                return;
            
            //TODO: ADD HEALTH/DAMAGE STUFF HERE
            
            Destroy(gameObject);
        }

        private Vector2 Movement(float timer)
        {
            // Moves right according to the bullet's rotation
            float x = timer * speed * transform.right.x;
            float y = timer * speed * transform.right.y;
            return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
        }

        //doing this instead of calling GetComponent on Update like the og tutorial does. - FigJ
        private void SetUpBullet(GameObject goToCheck, float setSpeed, float setLife, Color setColor)
        {
            if (goToCheck != gameObject)
                return;
            
            speed = setSpeed;
            bulletLife = setLife;
            
            bulletSprite.color = setColor;
        }
    }
}
