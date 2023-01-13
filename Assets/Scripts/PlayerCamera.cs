using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	//private static readonly string[] WORKER_TAG_NAMES = { "Farmer", "Builder", "Miner", "Gatherer", "Fisher" };

	public float speed = 1.5f;
	public float acceleration = 10f;
	public float sensitivity = 5f; // чувствительность мыши
	public Camera mainCamera;

	private Vector3 direction;
    //private List<int> swappedIndices = new List<int>(WORKER_TAG_NAMES.Length);

    private void Start()
	{		
	}

    private void Update()
	{
		Rotate();
	}

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
        // по нажатию Пробела или ЛКМ стреляем
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			Shoot(mainCamera.ScreenToWorldPoint(Input.mousePosition));			
		}
		else if (Input.GetKeyUp(KeyCode.Escape))
		{
			Debug.LogWarning("App exit requested");
            Application.Quit();
        }
	}

    private void Shoot(Vector3 aim)
    {
		Debug.Log($"Trying to shoot at world point {aim}");
    }
}
