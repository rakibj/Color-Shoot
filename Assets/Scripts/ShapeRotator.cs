using UnityEngine;

public class ShapeRotator : MonoBehaviour {

    public float speed;

    private void Start()
    {
        speed = 100f;
        //speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
