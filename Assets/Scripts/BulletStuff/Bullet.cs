using System;
using UnityEngine;
using UnityEngine.Events;

namespace BulletStuff
{
    public class Bullet : MonoBehaviour
    {
        public float bulletLife = 1f; // Defines how long before the bullet is destroyed
        public float rotation = 0f;
        public float speed = 1f;
        public int damageAmt = 1;
        
        [SerializeField] private SpriteRenderer bulletSprite;

        private Vector2 spawnPoint;
        private float timer = 0f;

        private GameManager gMan;

        void OnEnable()
        {
            BulletSpawner.OnInstantiateBullet += SetUpBullet;
            
            gMan = GameManager.Instance;
            if (!gMan)
                Debug.LogError("No GameManager found!");
            
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"OnTriggerEnter2D, my tag is {gameObject.tag}");
            Debug.Log($"OnTriggerEnter2D, other's tag is {other.gameObject.tag}");
            
            if (other.gameObject.tag.Equals("Player") && gameObject.tag.Equals("EnemyBullet"))
            {
                gMan.player.takeDamage(damageAmt);
                Destroy(gameObject);
                return;
            }

            if (other.gameObject.tag.Equals("EnemyBoss") && gameObject.tag.Equals("PlayerBullet"))
            {
                gMan.boss.takeDamage(damageAmt);
                Destroy(gameObject);
            }
        }

        private Vector2 Movement(float timer)
        {
            if (gameObject.tag.Equals("PlayerBullet"))
            {
                //Move bullet up for player variant instead - FigJ
                float xUp = timer * speed * transform.up.x;
                float yUp = timer * speed * transform.up.y;
                return new Vector2(xUp + spawnPoint.x, yUp + spawnPoint.y);
            }

            // Moves right according to the bullet's rotation
            float x = timer * speed * transform.up.x * -1f;
            float y = timer * speed * transform.up.y * -1f;
            return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
        }

        //doing this instead of calling GetComponent on Update like the og tutorial does. - FigJ
        private void SetUpBullet(GameObject goToCheck, float setSpeed, float setLife, Color setColor)
        {
            if (goToCheck != gameObject)
                return;
            
            speed = setSpeed;
            bulletLife = setLife;
            
            //commenting out for now for new sprites
            //bulletSprite.color = setColor;
        }
    }
}
