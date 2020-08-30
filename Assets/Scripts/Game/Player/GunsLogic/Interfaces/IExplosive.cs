using UnityEngine;

namespace BoomAway.Assets.Scripts.Game.Player.Guns
{
    public interface IExplosive
    {
        void explode(float radiousOfImpact,float force,
        LayerMask layerToHit);
    }
}
