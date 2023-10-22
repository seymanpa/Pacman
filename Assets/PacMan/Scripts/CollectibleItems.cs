using UnityEngine;

namespace PacMan
{
    public class CollectibleItems : PelletContoller
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                Eat();
            }
        }

        protected override void Eat()
        {
            FindObjectOfType<GameManager>().ItemEaten(this);
        }
    }
}

