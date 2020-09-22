using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BoomAway.Assets.Scripts.Game.Player.Guns{
    public class Explosion : MonoBehaviour
    {
        [SerializeField]private float explosionForce;
        [SerializeField]private float radiousOfImpact;
        [SerializeField]private LayerMask layerToExplode;
        private IExplosive explosive;
        // Start is called before the first frame update
        void Start()
        {
            explosive = GetComponent<IExplosive>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!Grid.gameStateManager.editing)
            { 
                explosive.explode(radiousOfImpact, explosionForce, layerToExplode);
            }
        }
    }

}