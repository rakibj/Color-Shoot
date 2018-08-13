using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public static CameraFollow instance;
    Transform target;
    public float cameraOffsetY = 2f;
    public float speed = 1f;
    public float camMinY = 0f;

    bool inGameCameraMoved;

    void Start()
    {
        inGameCameraMoved = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        instance = this;
    }

    void Update()
    {
        if (target == null) return;
        if (GameManager.inGame)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        if (GameManager.inGame)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, target.position.y + cameraOffsetY, target.position.z - 10), Time.deltaTime * speed);
        }
        else if(!inGameCameraMoved)
        {
            transform.position = new Vector3(3.36f, -.26f, transform.position.z);
            inGameCameraMoved = true;
        }
        //transform.position = new Vector3(transform.position.x, target.position.y + cameraOffsetY, target.position.z - 10);
        if (transform.position.y <= camMinY)
        {
            target = null;
        }
    }


}
