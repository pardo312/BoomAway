using UnityEngine;
using UnityEngine.EventSystems;
namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private float shootForce;
        [SerializeField] private KeyCode gunKeyCode;
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
            if (!Grid.gameStateManager.editing && !Grid.gameStateManager.IsPaused)
            {
                if(EventSystem.current.currentSelectedGameObject == null){
                    if (Input.GetKeyDown(gunKeyCode))
                    {
                        gun.shoot(shootForce, bc, rb);
                    }
                }
            }
            
        }
    }

}
