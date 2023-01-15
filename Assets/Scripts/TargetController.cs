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

        /// <summary>
        /// Время (в секундах) до самоуничтожения объекта
        /// </summary>
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
        /// Внешний проигрыватель звуковых эффектов
        /// </summary>
        public AudioSource SoundPlayer { get; set; }
        /// <summary>
        /// Звуковой эффект при появлении врага на сцене
        /// </summary>
        public AudioClip EnemyRespawnSound { get; set; }
        /// <summary>
        /// Звуковой эффект при появлении врага на сцене
        /// </summary>
        public AudioClip SelfDestructSound { get; set; }

        /// <summary>
        /// Эквивалент конструктора в Unity
        /// </summary>
        private void Awake()
        {
            uiScript = GameObject.FindObjectOfType<UIHandler>();
        }

        /// <summary>
        /// При появлении объекта проигрывает звук
        /// </summary>
        private void Start()
        {            
            SoundPlayer?.PlayOneShot(EnemyRespawnSound);               
        }

        /// <summary>
        /// Обработчик события попадания "пули" во врага
        /// </summary>
        /// <param name="collision"></param>
        /// <remarks>
        /// Уничтожает пулю и врага
        /// </remarks>
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

        /// <summary>
        /// Обработчик уничтожения объекта - проигрывает звуки, а при самоуничтожении также вычитает очки у Игрока.
        /// </summary>
        private void OnDestroy()
        {
            if (!uiScript.IsGameOver)
            {
                if (isSelfDescruction)
                {
                    SoundPlayer?.PlayOneShot(SelfDestructSound);
                    uiScript.RemoveOnePoint();
                }
                else
                {
                    SoundPlayer?.Play();
                }
            }
        }
    }
}