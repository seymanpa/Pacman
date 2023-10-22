using UnityEngine;

namespace PacMan
{
    public class PelletContoller : MonoBehaviour
    {
        public int point = 10;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                Eat();
            }
        }

        protected virtual void Eat()
        {
           FindObjectOfType<GameManager>().PelletEaten(this);
        }
    }
}

