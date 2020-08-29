using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class C4 : MonoBehaviour, IGun
{
        [SerializeField]private float force;
        [SerializeField]private GameObject worldParent;
        private Rigidbody2D rb;
        private BoxCollider2D bc;
        private bool alredyShoot;
        

        private void Awake() {
            rb = this.GetComponent<Rigidbody2D>();
            bc = this.GetComponent<BoxCollider2D>();
        }

        public void shoot()
        {
            if(!alredyShoot){
                
                transform.parent = worldParent.transform;
                bc.isTrigger = false;
                rb.isKinematic = false;
                rb.AddForce(transform.right * force *-1);
                alredyShoot=true;
            }
            else
                Destroy(gameObject);
                
        }

    }

}
