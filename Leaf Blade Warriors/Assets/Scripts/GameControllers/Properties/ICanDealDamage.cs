using Photon.Pun;

namespace GameControllers.Properties
{
    public interface ICanDealDamage
    {
        public void DealDamage(PhotonView damageTakerView, PhotonView damageDealerView);
    }
}

