using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;


namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class FastRocket : MonoBehaviour, IGun, IExplosive
    {

        private bool waitForRocket = false;
        private bool isShooting = false;
        private bool readyToExplode = false;

        private Rigidbody2D rb; 
        public void explode(float radiousOfImpact, float explosionForce, LayerMask layerToHit)
        {
                if (readyToExplode)
                {
                    Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radiousOfImpact, layerToHit);

                    foreach (Collider2D obj in objects)
                    {
                        // Podemos mirar si se pueden hacer scrips para cada tipo de bloque (todos implementan explosión)
                        // 
                        Vector2 direction = obj.transform.position - transform.position;
                        obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
                    }
                    Grid.gameStateManager.hasCurrentAmmo = false;
                    Destroy(gameObject);
                }
        }

        public void shoot(float shootForce, BoxCollider2D bc, Rigidbody2D rb)
        {
                Vector3 tempPosition = transform.position;
                transform.SetParent(null);
                transform.position = tempPosition;

                this.rb = rb;
                isShooting = true;
                bc.isTrigger = false;
                //Grid.gameStateManager.currentAmmo[1]--;
        }

        private void FixedUpdate()
        {
            Debug.Log(isShooting);
            if (isShooting)
            {
                var locVel = transform.InverseTransformDirection(rb.velocity);

                locVel.x = 4f;
                rb.velocity = transform.TransformDirection(locVel);


                Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 0.8f, 5);
                Collider2D[] objects2 = Physics2D.OverlapCircleAll(transform.position, 0.8f, 8);

                if (objects.Length != 0 || objects2.Length != 0)
                {
                    readyToExplode = true;
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }

    }

}