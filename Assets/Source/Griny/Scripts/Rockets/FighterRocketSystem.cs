using ShipBase;
using UnityEngine;

namespace Enemy
{
    public class FighterRocketSystem : MonoBehaviour
    {
        [SerializeField] private SetRockets _setRockets;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<ShipBase.Shield>(out var shield) || collision.gameObject.TryGetComponent<Ship>(out var ship))
            {
                if (_setRockets.GetNumber() < _setRockets.GetCountRocket())
                {
                    _setRockets.DisablSprite(_setRockets.GetNumber());
                    _setRockets.FireRocket(_setRockets.GetNumber());
                    _setRockets.UpNumberShot();
                }
            }


            //protected override void AttackPlayer(Collider2D collision,
            //    List<SpriteRenderer> spritsRockets, List<RocketLauncher> rocketLaunchers, int number)
            //{
            //    if (collision.gameObject.TryGetComponent<ShipBase.Shield>(out var shield) || collision.gameObject.TryGetComponent<Ship>(out var ship))
            //    {
            //        if (number < rocketLaunchers.Count)
            //        {
            //            Number++;
            //            spritsRockets[number].gameObject.SetActive(false);
            //            rocketLaunchers[number].FierRocket();
            //        }
            //        else
            //        {
            //            Number = 0;
            //        }
            //    }
            //}
        }
    }
}