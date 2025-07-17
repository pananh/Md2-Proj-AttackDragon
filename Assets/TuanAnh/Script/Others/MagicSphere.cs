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
    private MeshRenderer parentMeshRenderer;

    [SerializeField] private AudioClip explosionSound;

    public void Init()
    {
        isBigger = false;
        isExplode = false;
        minRadius = 0.5f;
        maxRadius = 3f;
        transform.localScale = Vector3.one * minRadius * 2f;
        timer = 0f;

        parentMeshRenderer = GetComponent<MeshRenderer>();
        parentMeshRenderer.enabled = false;

    }

    void Update()
    {
        if (!isBigger)
        {
            return;
        }
        parentMeshRenderer.enabled = true;
        timer += Time.deltaTime;
        currentRadius = Mathf.Lerp(minRadius, maxRadius, timer);
        transform.localScale = Vector3.one * currentRadius * 2f;

        if (isExplode)
        {
            isExplode = false;
            Explode();
        }

    }

    private void PlayExplosionSound()
    {
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1);
    }

    private void Explode()
    {
        if (currentRadius < maxRadius)
        {
            transform.localScale = Vector3.one * maxRadius * 2f;
        }
        DealDamage();
        PlayExplosionSound();
        StartCoroutine(DestroyAfterDelay(0.2f));
    }

    private void DealDamage()
    {
        // co tao mot it luc day len quai vat, tru khi bat isKinematic
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRadius, monsterLayer);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Monster"))
            {
                Debug.Log("Hit Monster Magic 2");
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
