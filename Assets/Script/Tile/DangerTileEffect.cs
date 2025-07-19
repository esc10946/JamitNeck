using UnityEngine;

public class DangerTileEffect : MonoBehaviour, ISpecialTile
{
    private bool triggered = false;

    public void Activate(GameObject player)
    {
        if (!triggered)
        {
            triggered = true;
            // �˹� �׽�Ʈ
            Debug.Log("DangerTileEffect �ߵ�");
            //Vector3 knockbackDir = (player.transform.position - transform.position).normalized;
            //Debug.Log(knockbackDir);
            //player.GetComponent<Rigidbody2D>().AddForce(knockbackDir * 500f);
        }
    }

    public void ResetTile()
    {
        triggered = false;
    }
}
