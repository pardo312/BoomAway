using System.Collections;
using UnityEngine;

namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class C4 : MonoBehaviour, IGun, IExplosive
    {
        
        [SerializeField]private LayerMask layerToStick;
        [SerializeField] private KeyCode gunExplosionKeyCode;
        private bool alredyShoot;
        private bool readyToExplode;
        private Rigidbody2D rb;

        //Analytics
        [SerializeField] private UsesPerWeapon usesPer;

        private void Start()
        {
            usesPer = this.GetComponent<UsesPerWeapon>();
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
                Grid.gameStateManager.currentAmmo[Constants.C4_TYPE]--;
                StartCoroutine(setReadyToExplode());
                //ANALYTICS
                usesPer.updateUse();
            }
        }

        IEnumerator setReadyToExplode()
        {
            yield return new WaitForSeconds(0.5f);
            readyToExplode = true;
        }

        public void explode(float radiousOfImpact, float explosionForce, LayerMask layerToHit)
        {
            if (Input.GetKeyDown(gunExplosionKeyCode))
            {
                if (readyToExplode)
                {
                    Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radiousOfImpact, layerToHit);

                    foreach (Collider2D obj in objects)
                    {
                        // Podemos mirar si se pueden hacer scrips para cada tipo de bloque (todos implementan explosión)
                            Vector2 direction = obj.transform.position - transform.position;
                            if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D prueba))
                            {
                                obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
                            }

                            Debug.Log(obj.TryGetComponent<BreakableTile>(out BreakableTile hola));
                            if (obj.TryGetComponent<BreakableTile>(out BreakableTile hola2))
                            {
                                obj.GetComponent<BreakableTile>().explode = true;
                            }
                        }
                        //Instanciar otro
                        Grid.gameStateManager.hasCurrentAmmo = false;
                        Destroy(gameObject);
                }
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

