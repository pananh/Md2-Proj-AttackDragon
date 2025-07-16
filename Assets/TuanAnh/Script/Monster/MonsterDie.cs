using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDie : MonsterState
{
    public override void Enter(IMonsterController monster)
    {
       
        monster.Agent.isStopped = true;
        monster.Agent.enabled = false;
        monster.Animator.SetBool("Die", true);
        PlayerController.Instance.TakeExperience(monster.MonsterData.gainExp);
        HealthBarManager.Instance.RemoveMonsterHealthBar(monster);
        UIMinimap.Instance.RemoveMonsterIcon(monster);
        MonsterManager.Instance.RemoveMonster(monster);
        Rigidbody rigidbody = monster.Transform.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
        monster.StartMonsterCoroutine(DestroyAfterDelay(monster, 5f));
    }

    public override void Exit()
    {
    }

    private IEnumerator DestroyAfterDelay(IMonsterController monster, float delay)
    {
        yield return new WaitForSeconds(delay);
        Object.Destroy(monster.Transform.gameObject);
    }
}
