using GameControllers.Properties;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.NetworkEntities
{
    public abstract class HealthHandler : MonoBehaviour, ICanDealDamage
    {
        public abstract void DealDamage(PhotonView damageTakerView, PhotonView damageDealerView);
    }
}

