using UnityEngine;

namespace Assets.Scripts
{
    public class TargetController : MonoBehaviour
    {
        [Header(@"Объект ""пули"", столкновение с которым уничтожит данный объект")]
        [SerializeField] private GameObject bulletPrefab;

        private float lifeTime = 2;

        public float LifeTime
        {
            get => lifeTime;
            set
            {
                lifeTime = value;
                GameObject.Destroy(gameObject, lifeTime);
            }
        }


        /// <summary>
        /// Обработчик события пробития врага на вылет - уничтожает пулю и врага
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(bulletPrefab.tag))
            {                           
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {            
            gameObject.GetComponentInParent<AudioSource>()?.Play();            
        }
    }
}