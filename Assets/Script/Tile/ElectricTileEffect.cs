using UnityEngine;

public class ElectricTileEffect : MonoBehaviour, ISpecialTile
{
    private bool triggered = false;

    public void Activate(GameObject player)
    {
        if (triggered) return;
        triggered = true;

        Debug.Log("ElectricTileEffect �ߵ�");

        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
            controller.DisableJump(3f); // 5�ʰ� ���� ����

        StartCoroutine(RevertTile());
    }

    private System.Collections.IEnumerator RevertTile()
    {
        yield return new WaitForSeconds(1f); // Ÿ�� 1�� �� ����

        TileComp tile = GetComponent<TileComp>();
        if (tile != null)
            tile.SetTileType(TileType.Normal);
    }

    public void ResetTile()
    {
        triggered = false;
    }
}
