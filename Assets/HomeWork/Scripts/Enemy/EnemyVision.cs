using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] LayerMask _playerLayer;

    [Header("Settings")]
    [SerializeField] private float _seeAreaSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _seeAreaSize);
    }

    public bool TrySeeTarget(out Transform target)
    {
        target = null;
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _seeAreaSize, _playerLayer);

        if (hit != null)
        {
            Vector2 direction = (hit.transform.position - transform.position).normalized;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, _seeAreaSize, ~(1 << gameObject.layer));

            if (hit2D.collider != null)
            {
                if (hit2D.collider == hit)
                {
                    target = hit2D.transform;
                    return true;
                }
            }
        }
        return false;
    }
}
