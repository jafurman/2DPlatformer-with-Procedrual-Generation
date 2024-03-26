using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartBossFight : MonoBehaviour
{

    public GameObject boss;
    public Enemy enemy;
    public CinemachineVirtualCamera cam;
    public float newFOV = 60f;

    public BossHPBar hpBar;
    public static bool bossFightStarted = false;
    public bossFightCamera bossCamera;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        hpBar = boss.GetComponent<BossHPBar>();

        if (boss != null)
        {
            boss.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (boss != null)
            {
                StartCoroutine(spawnHP());

                if (cam != null)
                {
                    bossCamera.StartBossFightCamera();
                }
            }

            StartCoroutine(DesObj());
        }
    }

    public IEnumerator spawnHP()
    {
        StartCoroutine(spawnLights.startLights());
        yield return new WaitForSeconds(2f);
        boss.SetActive(true);
        yield return new WaitForSeconds(1f);
        hpBar.HC1.SetActive(true);
        yield return new WaitForSeconds(.3f);
        hpBar.HC2.SetActive(true);
        yield return new WaitForSeconds(.3f);
        hpBar.HC3.SetActive(true);
        yield return new WaitForSeconds(.3f);
        hpBar.HC4.SetActive(true);
        yield return new WaitForSeconds(.3f);
        hpBar.HC5.SetActive(true);
        yield return new WaitForSeconds(.3f);
        hpBar.HC6.SetActive(true);
        yield return new WaitForSeconds(.3f);
        hpBar.HC7.SetActive(true);
        bossFightStarted = true;

        Debug.Log("STARTING BOSS HEALTH: " + enemy.currentHealth);
        //Boss Fight Starting Method Calls
        //LightingController.turnBright();
    }

    public IEnumerator DesObj()
    {
        yield return new WaitForSeconds(6f);

        Destroy(gameObject);
    }
}
