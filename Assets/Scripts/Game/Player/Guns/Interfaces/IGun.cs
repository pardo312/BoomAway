using UnityEngine;

namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public interface IGun
    {
        void shoot(float shootForce,BoxCollider2D bc,
        Rigidbody2D rb,GameObject worldParent);
    }
}
