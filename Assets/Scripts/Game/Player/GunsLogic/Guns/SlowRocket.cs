using System.Collections.Generic;
using UnityEngine;


namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class SlowRocket : MonoBehaviour, IGun, IExplosive
    {

        

        [SerializeField]private LayerMask layerToHit;
        private bool waitForRocket = false;
        private bool isShooting = false;
        private bool readyToExplode = false;
        private float shootForce;
        private Collider2D myCollider;

        public GameObject explosion;

        private Rigidbody2D rb;
        //Analytics
        [SerializeField] private UsesPerWeapon usesPer;

        private void Start()
        {
            usesPer = this.GetComponent<UsesPerWeapon>();
            myCollider=this.GetComponent<BoxCollider2D>();
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
                    Grid.audioManager.Play("PlatformDestroyFX");
                    obj.GetComponent<BreakableTile>().explode = true;
                }
            }
                Grid.gameStateManager.hasCurrentAmmo = false;
                explosion.transform.position = gameObject.transform.position;
                explosion.transform.localScale = gameObject.transform.localScale;
                explosion.transform.localPosition = gameObject.transform.localPosition;
                Instantiate(explosion);
                Grid.audioManager.Play("ExplodeFX");
                Destroy(gameObject);
            }
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
                Grid.gameStateManager.currentAmmo[Constants.SLOW_ROCKET_TYPE]--;

                //Analytics
                usesPer.updateUse();
            }
        }

        private void FixedUpdate()
        {
            if (isShooting)
            {
                var locVel = transform.InverseTransformDirection(rb.velocity);

                locVel.x = shootForce;
                rb.velocity = transform.TransformDirection(locVel);

                Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 0.5f , layerToHit);

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