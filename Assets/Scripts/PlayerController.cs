using UnityEngine;
using EZCameraShake;

public class PlayerController : MonoBehaviour {

    public string currentColor;

    public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;
    public GameObject deathYellow;
    public GameObject deathCyan;
    public GameObject deathMagenta;
    public GameObject deathPink;
    public GameObject scoreEffect;

    Rigidbody2D rb;
    SpriteRenderer sr;
    float wallSpawnLimit = 8f;
    GameObject clickArea;

    void Start () {
        GameManager.playerDead = false;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        SetRandomColor();
        rb.velocity = Vector2.zero;
        clickArea = GameObject.Find("Ball Click Area").gameObject;
        //rb.isKinematic = true;
    }
	
	void Update () {
        if (!GameManager.inGame) return;
        if (transform.position.y <= -3f) GameManager.playerDead = true;
        KinematicController();
        if (rb.velocity != Vector2.zero)
        {
            clickArea.SetActive(false);
        }
        else
        {
            clickArea.SetActive(true);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            AudioManager.instance.Play("wall");
            return;
        }
        if(collision.gameObject.tag == currentColor)
        {
            GameManager.score++;
            GameManager.spawnCircle = true;
            FindObjectOfType<AudioManager>().Play("move");
            transform.position = collision.transform.root.localPosition;
            Instantiate(scoreEffect, transform.position, Quaternion.identity);
            rb.velocity = Vector2.zero;
            SetRandomColor();
            foreach (Transform child in collision.transform.parent)
            {
                if (child.CompareTag(currentColor))
                {
                    child.GetComponent<PolygonCollider2D>().enabled = false;
                }
            }
        }
        else
        {
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            string effectName = "death" + currentColor;
            GameObject deathEffect;
            switch (effectName)
            {
                case "deathYellow":
                    deathEffect = deathYellow;
                    break;
                case "deathPink":
                    deathEffect = deathPink;
                    break;
                case "deathCyan":
                    deathEffect = deathCyan;
                    break;
                case "deathMagenta":
                    deathEffect = deathMagenta;
                    break;
                default:
                    deathEffect = deathYellow;
                    break;
            }
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("death");
            GameManager.playerDead = true;
            Destroy(gameObject);
        }
    }

    void KinematicController()
    {
        if (rb.velocity == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }


    void SetRandomColor()
    {
        int index = Random.Range(0, 4);

        switch (index)
        {
            case 0:
                currentColor = "Cyan";
                sr.color = colorCyan;
                break;
            case 1:
                currentColor = "Yellow";
                sr.color = colorYellow;
                break;
            case 2:
                currentColor = "Magenta";
                sr.color = colorMagenta;
                break;
            case 3:
                currentColor = "Pink";
                sr.color = colorPink;
                break;
        }
    }
}
