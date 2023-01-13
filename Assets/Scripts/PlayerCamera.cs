using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	//private static readonly string[] WORKER_TAG_NAMES = { "Farmer", "Builder", "Miner", "Gatherer", "Fisher" };

	public float speed = 1.5f;
	public float acceleration = 10f;
	public float sensitivity = 5f; // чувствительность мыши
	public Camera mainCamera;

    [SerializeField] private GameObject enemyTagHolder;

	private CrossHair crossHairScript;
    private SkinLoader skinLoader;
    private Color[] fireColors = { Color.green, Color.blue, Color.white, Color.red, Color.cyan, Color.magenta };
 

    private void Start()
	{
        crossHairScript = GetComponent<CrossHair>();
        skinLoader = GameObject.FindObjectOfType<SkinLoader>();
    }

    private void Update()
	{
		Rotate();
        // по нажатию ПКМ меняем вид прицела
        if (Input.GetMouseButtonUp(1))
        {
            crossHairScript.skin = skinLoader.GetRandomSkin();
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.LogWarning("App exit requested");
            Application.Quit();
        }
    }

    /// <summary>
    /// Метод вращения камеры
    /// </summary>
    private void Rotate()
	{
        const int MIN_VERTICAL_ANGLE = 20;
        const int MAX_VERTICAL_ANGLE = 340;
        const int MIN_HORIZONTAL_ANGLE = 140;
        const int MAX_HORIZONTAL_ANGLE = 295;

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float rotX = mainCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        float rotY = mainCamera.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity;
        // вращение камеры ограничиваем
        //Debug.Log($"rotY: {rotY}, rotX: {rotX}");
        if (rotY >= MIN_VERTICAL_ANGLE && rotY <= 100)
        {
            rotY = MIN_VERTICAL_ANGLE;
        }
        else if (rotY >= 300 && rotY <= 360)
        {
            rotY = Mathf.Max(rotY, MAX_VERTICAL_ANGLE); ;
        }
        mainCamera.transform.localEulerAngles = new Vector3(rotY,	Mathf.Max(MIN_HORIZONTAL_ANGLE, 
            Mathf.Min(rotX, MAX_HORIZONTAL_ANGLE), 0));
	}

}
