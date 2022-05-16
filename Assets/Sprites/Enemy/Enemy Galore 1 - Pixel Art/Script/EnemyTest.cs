using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    //[SerializeField] private Animator EnemyAnim;
    [SerializeField] private Animator[] EnemyAnims;

    // Start is called before the first frame update
    //public void Start()
    //{
    //    EnemyAnim = GetComponent<Animator>();
    //}

    public void Animation_1_Idle()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            EnemyAnims[i].SetBool("Run", false);
            Debug.Log("Idle");
        }
    }
    public void Animation_2_Run()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            EnemyAnims[i].SetBool("Run", true);
            Debug.Log("Running");
        }
    }
    public void Animation_3_Hit()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            EnemyAnims[i].SetBool("Run", false);
            EnemyAnims[i].SetTrigger("Hit");
            Debug.Log("Hit");
        }
    }
    public void Animation_4_Death()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            EnemyAnims[i].SetBool("Run", false);
            EnemyAnims[i].SetTrigger("Death");
            Debug.Log("Death");
        }
    }
    public void Animation_5_Ability()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            EnemyAnims[i].SetBool("Run", false);
            EnemyAnims[i].SetBool("Ability", true);
            Debug.Log("Ability");
        }
    }
    public void Animation_6_Attack()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            EnemyAnims[i].SetBool("Run", false);
            EnemyAnims[i].SetTrigger("Attack");
            Debug.Log("Attack");
        }
    }
    public void Animation_7_Attack2()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            EnemyAnims[i].SetBool("Run", false);
            EnemyAnims[i].SetTrigger("Attack 2");
            Debug.Log("Attack 2");
        }
    }
    public void Animation_8_Attack3()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            EnemyAnims[i].SetBool("Run", false);
            EnemyAnims[i].SetTrigger("Attack 3");
            Debug.Log("Attack 3");
        }
    }


    //Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        EnemyAnim.SetBool("Run", false);
    //        Debug.Log("Idle");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        EnemyAnim.SetBool("Run", true);
    //        Debug.Log("Running");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        EnemyAnim.SetBool("Run", false);
    //        EnemyAnim.SetTrigger("Hit");
    //        Debug.Log("Hit");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        EnemyAnim.SetBool("Run", false);
    //        EnemyAnim.SetTrigger("Death");
    //        Debug.Log("Death");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha5))
    //    {
            
    //        EnemyAnim.SetBool("Run", false);
    //        EnemyAnim.SetBool("Ability", true);
    //        Debug.Log("Ability");
            
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha6))
    //    {
    //        EnemyAnim.SetBool("Run", false);
    //        EnemyAnim.SetTrigger("Attack");
    //        Debug.Log("Attack");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha7))
    //    {
    //        EnemyAnim.SetBool("Run", false);
    //        EnemyAnim.SetTrigger("Attack 2");
    //        Debug.Log("Attack 2");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha8))
    //    {
    //        EnemyAnim.SetBool("Run", false);
    //        EnemyAnim.SetTrigger("Attack 3");
    //        Debug.Log("Attack 3");
    //    }
    //}
    //public void GolemEndAbility()
    //{
    //    EnemyAnim.SetBool("Ability", false);
    //}
}
