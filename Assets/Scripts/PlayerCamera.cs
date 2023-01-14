using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // чувствительность мыши
    [SerializeField] private float sensitivity = 3f; 
	
    public Camera mainCamera;

	private CrossHair crossHairScript;
    private SkinLoader skinLoader;

    private bool IsCursorLocked
    {
        get => Cursor.lockState == CursorLockMode.Locked;
        set => Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void Start()
	{
        crossHairScript = GetComponent<CrossHair>();
        skinLoader = GameObject.FindObjectOfType<SkinLoader>();
        IsCursorLocked = true; 
    }

    private void Update()
	{
        if (IsCursorLocked)
        {
            Rotate();
        }
        if (Debug.isDebugBuild && Input.GetKeyUp(KeyCode.Space))
        {
            GameObject.FindObjectOfType<EnemySpawner>()?.SpawnRandomEnemy();
        }
        // по нажатию ПКМ меняем вид прицела
        else if (Input.GetMouseButtonUp(1))
        {
            crossHairScript.skin = skinLoader.GetRandomSkin();
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.LogWarning("App exit requested");
            Cursor.lockState = CursorLockMode.None;
            this.IsCursorLocked = false;
            Application.Quit();
        }
        else if (!IsCursorLocked && Input.anyKey)
        {
            IsCursorLocked = true;
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
