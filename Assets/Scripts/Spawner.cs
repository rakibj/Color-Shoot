using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform lastSpawnedCircle;
    public float xOffset = 3.5f;
    public float yOffset = 3.0f;
    public float rotationOffset = 180;
    public static Spawner instance;
    public GameObject spawnEffect;

    ObjectPooler objectPooler;
    float nextYSpawnWall = 15;

	void Start () {
        objectPooler = ObjectPooler.instance;
        lastSpawnedCircle.rotation = new Quaternion(0, 0, 0, lastSpawnedCircle.rotation.w);
        MakeSingleton();    	
	}

    void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update () {
        SpawnCircle();
        //SpawnWall();
	}
    
    void SpawnCircle()
    {
        if (GameManager.spawnCircle)
        {
            if (lastSpawnedCircle.position.x > 0)
            {
                Vector2 newSpawnPosition = new Vector2(-xOffset, lastSpawnedCircle.position.y + yOffset);
                GameObject newSpawnedObject = objectPooler.SpawnFromPool("Circle", newSpawnPosition, Quaternion.Euler(new Vector3(0,0,lastSpawnedCircle.rotation.eulerAngles.z + 200)));
                Instantiate(spawnEffect, newSpawnPosition, Quaternion.Euler(new Vector3(0, 0, lastSpawnedCircle.rotation.eulerAngles.z + 200)));
                lastSpawnedCircle = newSpawnedObject.transform;
            }
            else
            {
                Vector2 newSpawnPosition = new Vector2(xOffset, lastSpawnedCircle.position.y + yOffset);
                GameObject newSpawnedObject = objectPooler.SpawnFromPool("Circle", newSpawnPosition, Quaternion.Euler(new Vector3(0, 0, lastSpawnedCircle.rotation.eulerAngles.z + 350)));
                Instantiate(spawnEffect, newSpawnPosition, Quaternion.Euler(new Vector3(0, 0, lastSpawnedCircle.rotation.eulerAngles.z + 350)));
                lastSpawnedCircle = newSpawnedObject.transform;
            }

            GameManager.spawnCircle = false;
        }
    }


}
