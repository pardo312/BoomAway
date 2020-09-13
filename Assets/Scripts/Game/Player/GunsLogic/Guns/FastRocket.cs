using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;


namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class FastRocket : MonoBehaviour, IGun, IExplosive
    {
        [SerializeField]private LayerMask layerToHit;
        private bool waitForRocket = false;
        private bool isShooting = false;
        private bool readyToExplode = false;
        private float shootForce;
        private Rigidbody2D rb;

        public void shoot(float shootForce, BoxCollider2D bc, Rigidbody2D rb)
        {
           if(!isShooting)
            {
                Vector3 tempPosition = transform.position;
                transform.SetParent(null);
                transform.position = tempPosition;

                this.shootForce = shootForce;
                this.rb = rb;

                bc.isTrigger = false;
                isShooting = true;
                Grid.gameStateManager.currentAmmo[Constants.FAST_ROCKET_TYPE]--;
            }
        }

        public void explode(float radiousOfImpact, float explosionForce, LayerMask layerToExplode)
        {
                if (readyToExplode)
                {
                    Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radiousOfImpact, layerToExplode);

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

        private void FixedUpdate()
        {
            if (isShooting)
            {
                var locVel = transform.InverseTransformDirection(rb.velocity);

                locVel.x = shootForce;
                rb.velocity = transform.TransformDirection(locVel);


                Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 0.8f, layerToHit);

                if (objects.Length != 0 )
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