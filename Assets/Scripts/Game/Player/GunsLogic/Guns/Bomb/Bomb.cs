﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class Bomb : MonoBehaviour, IGun, IExplosive
    {

        [SerializeField]private LayerMask layerToStick;
        public float timeUntilExplode;
        private bool alredyShoot;
        [HideInInspector]public bool readyToExplode;

        [SerializeField] private GameObject explosion;

        private bool initExplosion;
        private Rigidbody2D rb;

        //Analytics
        [SerializeField] private UsesPerWeapon usesPer;

        private void Start()
        {
            usesPer = this.GetComponent<UsesPerWeapon>();
        }

        private void Awake()
        {
            initExplosion = false;
            readyToExplode = false;
        }
        private void Update()
        {
            if(readyToExplode){
                if (timeUntilExplode > 0 )
                {
                    timeUntilExplode -= Time.deltaTime;
                }
                else
                {
                    initExplosion=true;
                }
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

            if(initExplosion)
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
                            Grid.audioManager.Play("PlatformDestroyFX");
                            obj.GetComponent<BreakableTile>().explode = true;
                        }
                }
                Grid.gameStateManager.hasCurrentAmmo = false;
                explosion.transform.position = gameObject.transform.position;
                Instantiate(explosion);
                Grid.audioManager.Play("ExplodeFX");
                Destroy(gameObject);
            }
            
            


        }
        private void OnCollisionEnter2D(Collision2D other) {
            if(((1<<other.gameObject.layer) & layerToStick) != 0){
                Destroy(rb);
                transform.SetParent(other.transform);
                
            }
        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2);
        }
    }
}
