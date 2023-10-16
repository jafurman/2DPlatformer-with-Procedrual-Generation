using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableManager : MonoBehaviour
{
    public GameObject disabledP;
    public GameObject disabledM;

    private Animator pAnim;
    private Animator mAnim;

    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        pAnim = disabledP.GetComponent<Animator>();
        mAnim = disabledM.GetComponent<Animator>();

        disabledP.SetActive(false);
        disabledM.SetActive(false);
    }

    public IEnumerator DisableP()
    {
        PlayerController.canUseP = false;
        Weapon.canShoot = false;
        Bullet.canHold = false;
        disabledP.SetActive(true);
        pAnim.enabled = true;

        yield return new WaitForSeconds(4.8f);

        PlayerController.canUseP = true;
        Bullet.canHold = true;
        disabledP.SetActive(false);
        pAnim.enabled = false;
        Weapon.canShoot = true;

    }

    public IEnumerator DisableM()
    {
        yield return new WaitForSeconds(.4f);
        PlayerController.canUseM = false;
        disabledM.SetActive(true);
        mAnim.enabled = true;

        yield return new WaitForSeconds(4.4f);

        PlayerController.canUseM = true;
        disabledM.SetActive(false);
        mAnim.enabled = false;
    }

    public void Update()
    {
       if (!player.gameObject.activeSelf)
        {
            // M code
            PlayerController.canUseM = true;
            disabledM.SetActive(false);
            mAnim.enabled = false;

            // P code
            PlayerController.canUseP = true;
            Bullet.canHold = true;
            disabledP.SetActive(false);
            pAnim.enabled = false;
            Weapon.canShoot = true;
        }
    }
}
