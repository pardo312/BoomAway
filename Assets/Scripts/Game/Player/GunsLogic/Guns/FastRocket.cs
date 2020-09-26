using System.Collections.Generic;
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
        private Collider2D myCollider;
        [SerializeField] private UsesPerWeapon usesPer;

        private void Start()
        {
            usesPer = this.GetComponent<UsesPerWeapon>();
            myCollider=this.GetComponent<BoxCollider2D>();
        }

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
                //ANALYTICS
                usesPer.updateUse();
            }
        }

        public void explode(float radiousOfImpact, float explosionForce, LayerMask layerToExplode)
        {
                if (readyToExplode)
                {
                Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radiousOfImpact , layerToExplode);
                
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


                ContactFilter2D filter2D = new ContactFilter2D();
                filter2D.layerMask = layerToHit;
                List<Collider2D> objects = new List<Collider2D>();
                Physics2D.OverlapCollider(myCollider, filter2D, objects);

                if (objects.Count != 0 )
                {
                    readyToExplode = true;
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(myCollider.bounds.center, myCollider.bounds.size);
        }

        

    }

}