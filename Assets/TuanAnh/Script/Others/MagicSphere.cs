using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    [SerializeField] private LayerMask monsterLayer;
    private float minRadius;
    private float maxRadius;
    private float currentRadius;
    private bool isBigger;
    public bool IsBigger
    {
        get => isBigger;
        set => isBigger = value;
    }
    private bool isExplode;
    public bool IsExplode
    {
        get => isExplode;
        set => isExplode = value;
    }
    private float timer;

    public void Init()
    {
        isBigger = false;
        isExplode = false;
        minRadius = 0.5f;
        maxRadius = 3f;
        transform.localScale = Vector3.one * minRadius * 2f; 
        timer = 0f;
    }

    void Update()
    {
        if (!isBigger)
        {
            return;
        }
        timer += Time.deltaTime;
        currentRadius = Mathf.Lerp(minRadius, maxRadius, timer);
        transform.localScale = Vector3.one * currentRadius * 2f;

        if (isExplode)
        {
            Explode();
        }

    }

    private void Explode()
    {
        if (currentRadius < maxRadius)
        {
            transform.localScale = Vector3.one * maxRadius * 2f;
        }
        DealDamage();
        StartCoroutine(DestroyAfterDelay(0.2f));
    }

    private void DealDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRadius, monsterLayer);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Monster"))
            {
                Debug.Log("Hit Monster");
                IMonsterController monsterController = collider.GetComponent<IMonsterController>();
                if (monsterController != null)
                {
                    monsterController.TakeDamage(PlayerController.Instance.CurrentPlayerData.attack);
                }
            }

        }

    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, maxRadius);
    //}



}
