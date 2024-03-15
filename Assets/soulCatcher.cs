using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 // Import this namespace for Light2D

public class soulCatcher : MonoBehaviour
{
    public Animator anim;
    public bool active;

    public GameObject player;
    private UnityEngine.Rendering.Universal.Light2D light2D;
    private SpriteRenderer spriteRenderer; 

    public GameObject spawnTagPrefab;

    public SoulScoreManager ssm;

    // Start is called before the first frame update
    void Start()
    {
        ssm = GameObject.FindGameObjectWithTag("soulScoreManager").GetComponent<SoulScoreManager>();

        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();

        // Initialize active to false
        active = false;

        // Get references to Light2D and SpriteRenderer components
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
        {
            // If the player hits the thing, we then activate the visual indication if the flag is set
            if (active)
            {
                anim.SetTrigger("activated");

                // Set the color of Light2D and SpriteRenderer to #FF69FF (magenta)
                light2D.color = new Color(1f, 0.41f, 1f); // RGB values for #FF69FF
                spriteRenderer.color = new Color(1f, 0.41f, 1f); // RGB values for #FF69FF
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            // Set the new current position of the player to be the "start" which would resemble a checkpoint
            GameManager.playerStart = player.transform.position;

            Destroy(col.gameObject);

            StartCoroutine(spawntheTag());

            ssm.addPoints(10);
            
            // Set the active value to true
            active = true;


        }
    }

    private IEnumerator spawntheTag()
    {
        Vector3 PlayerPosition = player.transform.position;
        PlayerPosition.y += .66f;
        GameObject spawnTag = Instantiate(spawnTagPrefab, PlayerPosition, Quaternion.identity);

        yield return new WaitForSeconds(2);

        Destroy(spawnTag);


    }
}
