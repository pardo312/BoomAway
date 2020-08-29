using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BoomAway.Assets.Scripts.Game.Player.Guns{
    public class Shoot : MonoBehaviour
    {
        [SerializeField]private GameObject worldParent;
        [SerializeField]private float shootForce;
        private IGun gun;
        private Rigidbody2D rb;
        private BoxCollider2D bc;
        // Start is called before the first frame update
        void Start()
        {
            rb = this.GetComponent<Rigidbody2D>();
            bc = this.GetComponent<BoxCollider2D>();
            gun = this.GetComponent<IGun>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q)){   
                gun.shoot(shootForce,bc,rb,worldParent);
            }
        }
    }

}
