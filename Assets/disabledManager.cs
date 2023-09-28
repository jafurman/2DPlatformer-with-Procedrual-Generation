using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledManager : MonoBehaviour
{
    public static GameObject disabledP;
    public static GameObject disabledM;

    private static Animator pAnim;
    private static Animator mAnim;

    private void Start()
    {
        pAnim = disabledP.GetComponent<Animator>();
        mAnim = disabledM.GetComponent<Animator>();

        disabledP.SetActive(false);
        disabledM.SetActive(false);
    }

    public IEnumerator DisableP()
    {
        Weapon.canShoot = false;
        Bullet.canHold = false;
        disabledP.SetActive(true);
        pAnim.enabled = true;

        yield return new WaitForSeconds(5f);

        Weapon.canShoot = true;
        Bullet.canHold = true;
        disabledP.SetActive(false);
        pAnim.enabled = false;
        PlayerController.freezeOn = false;
        PlayerController.released = false;
    }

    public static IEnumerator DisableM()
    {
        PlayerController.allowAttack = false;
        disabledM.SetActive(true);
        mAnim.enabled = true;

        yield return new WaitForSeconds(5f);

        PlayerController.allowAttack = true;
        disabledM.SetActive(false);
        mAnim.enabled = false;

    }
}
