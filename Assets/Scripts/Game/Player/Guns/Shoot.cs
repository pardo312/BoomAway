using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BoomAway.Assets.Scripts.Game.Player.Guns{
    public class Shoot : MonoBehaviour
    {
        private IGun gun;
        // Start is called before the first frame update
        void Start()
        {
            gun = GetComponent<IGun>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q)){                
                gun.shoot();
            }
        }
    }

}
