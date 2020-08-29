using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class Bomb : MonoBehaviour,IGun,IExplosive
    {
        
        private bool alredyShoot;
        private bool readyToExplode;
        
        [SerializeField]private int timeUntilExplode;

        void Awake()
        {
            readyToExplode = false;
        }
        private void Update() {
            if(timeUntilExplode>0 && readyToExplode){
                Debug.Log("Tiempo Restante: "+timeUntilExplode);
                timeUntilExplode--;
            }
        }
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
            yield return new WaitForSeconds(1);
            readyToExplode = true;
        }

        public void explode(float radiousOfImpact, float explosionForce, LayerMask layerToHit)
        {
            
            if(timeUntilExplode == 0){

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
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,2);
        }

        
    }
}
