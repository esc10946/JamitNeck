using UnityEngine;

public class StatueWatcher : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }
    public Direction lookDirection;
    public Transform firePoint;
    public GameObject laserPrefab;
    public float delayBeforeFire = 3f;

    private void Start()
    {
        //LookInDirection(lookDirection);
        Invoke(nameof(FireLaser), delayBeforeFire);
    }

    private void FixedUpdate()
    {
        
    }


    private void LookInDirection(Direction dir)
    {
        Vector3 dirVec = Vector3.zero;
        switch (dir)
        {
            case Direction.Up: dirVec = Vector3.up; break;
            case Direction.Down: dirVec = Vector3.down; break;
            case Direction.Left: dirVec = Vector3.left; break;
            case Direction.Right: dirVec = Vector3.right; break;
        }

        transform.up = dirVec; // ���� ȸ��
    }

    private void FireLaser()
    {
        Vector3 startPos = firePoint.position; // ���⼭ null�̸� ���� �߻�
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
    }
}
