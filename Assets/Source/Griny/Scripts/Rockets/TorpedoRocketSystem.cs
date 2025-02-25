using System.Collections.Generic;
using UnityEngine;
using WordGame;

namespace Enemy
{
    public class TorpedoRocketSystem : MonoBehaviour
    {
        private const string _torpedoBorder = "TorpedoBorder";

        [SerializeField] private SetRockets _setRockets;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<BackgroundBorder>(out var backgroundBorder))
            {
                if(_torpedoBorder == backgroundBorder.GetName())
                {
                    for(int i = 0; i < _setRockets.GetCountRocket(); i++)
                    {
                        _setRockets.DisablSprite(_setRockets.GetNumber());
                        _setRockets.FireRocket(_setRockets.GetNumber());
                        _setRockets.UpNumberShot();
                    }
                }
            }
        }


        //protected override void AttackPlayer(Collider2D collision, List<SpriteRenderer> spritsRockets,
        //    List<RocketLauncher> rocketLaunchers,
        //    int number)
        //{
        //    if (collision.gameObject.TryGetComponent<BackgroundBorder>(out var backgroundBorder))
        //    {
        //        Debug.Log("заметил корабль");
        //        Debug.Log(number);

        //        if (backgroundBorder.GetName() == _torpedoBorder)
        //        {
        //            foreach (RocketLauncher rocketLauncher in rocketLaunchers)
        //            {
        //                spritsRockets[number].gameObject.SetActive(false);
        //                rocketLauncher.SetRocket();
        //                number++;
        //            }
        //        }
        //    }
        //}
    }
}