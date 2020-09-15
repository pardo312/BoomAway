using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class Bomb : MonoBehaviour, IGun, IExplosive
    {

        [SerializeField]private LayerMask layerToStick;
        [SerializeField] private int timeUntilExplode;
        private bool alredyShoot;
        private bool readyToExplode;
        private Rigidbody2D rb;

        private void Awake()
        {
            readyToExplode = false;
            timeUntilExplode *= 100;
        }
        private void Update()
        {
            if (timeUntilExplode > 0 && readyToExplode)
            {
                if (timeUntilExplode % 100 == 0)
                {
                    Debug.Log("Tiempo Restante: " + timeUntilExplode / 100);
                }
                timeUntilExplode--;
            }
        }
        public void shoot(float shootForce, BoxCollider2D bc, Rigidbody2D rb)
        {
            if (!alredyShoot)
            {
                Vector3 tempPosition = transform.position;
                transform.SetParent(null);
                transform.position = tempPosition;

                bc.isTrigger = false;
                rb.isKinematic = false;
                rb.AddForce(transform.right * (shootForce * 100) * -1);
                
                this.rb = rb;

                alredyShoot = true;
                Grid.gameStateManager.currentAmmo[Constants.BOMB_TYPE]--;

                StartCoroutine(setReadyToExplode());
            }
        }

        IEnumerator setReadyToExplode()
        {
            yield return new WaitForSeconds(0.5f);
            readyToExplode = true;
        }

        public void explode(float radiousOfImpact, float explosionForce, LayerMask layerToHit)
        {

            if (timeUntilExplode == 0)
            {

                Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radiousOfImpact, layerToHit);

                foreach (Collider2D obj in objects)
                {
                    Vector2 direction = obj.transform.position - transform.position;
                    if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D prueba))
                    {
                        obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
                    }

                    if (obj.TryGetComponent<BreakableTile>(out BreakableTile hola2))
                    {
                        obj.GetComponent<BreakableTile>().explode = true;
                    }
                }
                Grid.gameStateManager.hasCurrentAmmo = false;
                Destroy(gameObject);
            }
            
            


        }
        private void OnCollisionEnter2D(Collision2D other) {
            if(((1<<other.gameObject.layer) & layerToStick) != 0){
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2);
        }
    }
}
