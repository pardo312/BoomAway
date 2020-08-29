using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class C4 : MonoBehaviour, IGun, IExplosive
    {

        private bool alredyShoot;
        private bool readyToExplode;
        public void shoot(float shootForce,BoxCollider2D bc, Rigidbody2D rb, GameObject worldParent)
        {
            if (!alredyShoot)
            {
                transform.parent = worldParent.transform;
                bc.isTrigger = false;
                rb.isKinematic = false;
                rb.AddForce(transform.right * shootForce * -1);
                alredyShoot=true;
                StartCoroutine(setReadyToExplode());
            }
        }

        IEnumerator setReadyToExplode()
        {
            yield return new WaitForSeconds(2);
            readyToExplode = true;
        }

        public void explode(float radiousOfImpact, float explosionForce, LayerMask layerToHit)
        {
            
            if(Input.GetKeyDown(KeyCode.Q)){
                if(readyToExplode){
                    Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radiousOfImpact, layerToHit);

                    foreach (Collider2D obj in objects)
                    {
                        Vector2 direction = obj.transform.position - transform.position;
                        obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
                    }   
                    //ponerle sprite explosion
                    Destroy(gameObject);
                }
            }
        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,2);
        }

    }

}
