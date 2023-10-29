using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBar : MonoBehaviour
{

    public Enemy enemy;
    public float bossHp;

    public GameObject HC1;
    public GameObject HC2;
    public GameObject HC3;
    public GameObject HC4;
    public GameObject HC5;
    public GameObject HC6;
    public GameObject HC7;
    public bossFightCamera bossCamera;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        HC1.SetActive(false);
        HC2.SetActive(false);
        HC3.SetActive(false);
        HC4.SetActive(false);
        HC5.SetActive(false);
        HC6.SetActive(false);
        HC7.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartBossFight.bossFightStarted)
        {
            bossHp = enemy.currentHealth;

            if (bossHp > 37 && bossHp < 40)
            {
                HC1.SetActive(true);
                HC2.SetActive(true);
                HC3.SetActive(true);
                HC4.SetActive(true);
                HC5.SetActive(true);
                HC6.SetActive(true);
                HC7.SetActive(true);
            }
            if (bossHp <= 37 && bossHp > 34)
            {
                HC1.SetActive(false);
                HC2.SetActive(true);
                HC3.SetActive(true);
                HC4.SetActive(true);
                HC5.SetActive(true);
                HC6.SetActive(true);
                HC7.SetActive(true);
            }
            if (bossHp <= 34 && bossHp > 31)
            {
                HC1.SetActive(false);
                HC2.SetActive(false);
                HC3.SetActive(true);
                HC4.SetActive(true);
                HC5.SetActive(true);
                HC6.SetActive(true);
                HC7.SetActive(true);
            }
            if (bossHp <= 31 && bossHp > 28)
            {
                HC1.SetActive(false);
                HC2.SetActive(false);
                HC3.SetActive(false);
                HC4.SetActive(true);
                HC5.SetActive(true);
                HC6.SetActive(true);
                HC7.SetActive(true);
            }
            if (bossHp <= 28 && bossHp > 25)
            {
                HC1.SetActive(false);
                HC2.SetActive(false);
                HC3.SetActive(false);
                HC4.SetActive(false);
                HC5.SetActive(true);
                HC6.SetActive(true);
                HC7.SetActive(true);
            }
            if (bossHp <= 25 && bossHp > 22)
            {
                HC1.SetActive(false);
                HC2.SetActive(false);
                HC3.SetActive(false);
                HC4.SetActive(false);
                HC5.SetActive(false);
                HC6.SetActive(true);
                HC7.SetActive(true);
            }
            if (bossHp <= 22 && bossHp > 19)
            {
                HC1.SetActive(false);
                HC2.SetActive(false);
                HC3.SetActive(false);
                HC4.SetActive(false);
                HC5.SetActive(false);
                HC6.SetActive(false);
                HC7.SetActive(true);
            }
            if (bossHp <= 19)
            {
                HC1.SetActive(false);
                HC2.SetActive(false);
                HC3.SetActive(false);
                HC4.SetActive(false);
                HC5.SetActive(false);
                HC6.SetActive(false);
                HC7.SetActive(false);
                bossCamera.BackToPlayerCamera();
            }
        }
        
    }


}
