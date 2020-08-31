using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class FastRocket : MonoBehaviour, IGun, IExplosive
    {

        private bool waitForRocket = false;

        public void explode(float radiousOfImpact, float explosionForce, LayerMask layerToHit)
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radiousOfImpact, layerToHit);

            foreach (Collider2D obj in objects)
            {
                Vector2 direction = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
            }
        }

        public void shoot(float shootForce, BoxCollider2D bc, Rigidbody2D rb)
        {
            if (!waitForRocket)
            {
                Vector3 tempPosition = transform.position;
                transform.SetParent(null);
                transform.position = tempPosition;

                bc.isTrigger = false;
                rb.isKinematic = false;
                rb.AddForce(transform.right * shootForce * -1);

                Grid.gameStateManager.currentAmmo[1]--;
                StartCoroutine(waitForFastRocket());
            }
        }

        IEnumerator waitForFastRocket()
        {
            yield return new WaitForSeconds(1);
            waitForRocket = true;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }

}