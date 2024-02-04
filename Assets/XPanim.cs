using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPanim : MonoBehaviour
{
    public int soulScoreAddition;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<Text>();
        StartCoroutine(killIt());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator killIt()
    {
        if (scoreText != null)
        {
            scoreText.text = soulScoreAddition.ToString();
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
