using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class Bomb : MonoBehaviour,IGun,IExplosive
    {
        
        [SerializeField]private int timeUntilExplode;
        private bool alredyShoot;
        private bool readyToExplode;

        private void Awake() {
            readyToExplode = false;
        }
        private void Update() {
            if(timeUntilExplode>0 && readyToExplode){
                if(timeUntilExplode%100 == 0){                   
                    Debug.Log("Tiempo Restante: "+timeUntilExplode/100);
                }
                timeUntilExplode--;
            }
        }
        public void shoot(float shootForce,BoxCollider2D bc, Rigidbody2D rb)
        {
            if (!alredyShoot)
            {
                Vector3 tempPosition = transform.position;
                transform.SetParent (null);
                transform.position = tempPosition;

                bc.isTrigger = false;
                rb.isKinematic = false;
                rb.AddForce(transform.right * shootForce * -1);

                alredyShoot=true;
                Grid.gameStateManager.currentAmmo[0]--;
                
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
                Grid.gameStateManager.hasCurrentAmmo = false;
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
