using UnityEngine;

namespace Assets.Scripts
{
    public class TargetController : MonoBehaviour
    {
        [Header(@"Объект ""пули"", столкновение с которым уничтожит данный объект")]
        [SerializeField] private GameObject bulletPrefab;        

        private float lifeTime = 2;
        private UIHandler uiScript;
        private bool isSelfDescruction = true;
        private AudioSource parentPlayer;

        public float LifeTime
        {
            get => lifeTime;
            set
            {
                lifeTime = value;
                GameObject.Destroy(gameObject, lifeTime);
            }
        }

        private void Start()
        {
            uiScript = GameObject.FindObjectOfType<UIHandler>();
            parentPlayer = gameObject.GetComponentInParent<AudioSource>();
            parentPlayer?.PlayOneShot(gameObject.GetComponentInParent<EnemySpawner>().enemyRespawnSound);
        }

        /// <summary>
        /// Обработчик события пробития врага на вылет - уничтожает пулю и врага
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(bulletPrefab.tag))
            {
                isSelfDescruction = false;
                Destroy(collision.gameObject);
                uiScript.AddOnePoint();
                Destroy(gameObject);
            }            
        }

        private void OnDestroy()
        {
            if (!uiScript.IsGameOver)
            {
                if (isSelfDescruction)
                {
                    parentPlayer?.PlayOneShot(gameObject.GetComponentInParent<EnemySpawner>().enemySelfDestructSound);
                    uiScript.RemoveOnePoint();
                }
                else
                {
                    parentPlayer?.Play();
                }
            }
        }
    }
}