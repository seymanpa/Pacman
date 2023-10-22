using UnityEngine;

namespace PacMan
{
    public class PowerPelletController : PelletContoller
    {
        public float duration = 8.0f;

        protected override void Eat()
        {
            FindObjectOfType<GameManager>().PowerPelletEaten(this);
        }
    }
}
    

