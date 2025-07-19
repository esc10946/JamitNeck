using UnityEngine;
using System.Collections;

public interface ITimeEvent
{
    void OnPlayerEnter(GameObject player);
}

public class TileComp : MonoBehaviour
{
    public Sprite[] tileSprite;
    public float randomTileTime= 5.0f;
    public float recoveryTileTime = 3.0f;
    public TileManager tileManager;
    public Animation breakAnimation;

    Vector2 worldPosition;
    [SerializeField]
    TileType currentTileType = TileType.Normal;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        breakAnimation = GetComponent<Animation>();
    }

    //��Ҵٰ� ġ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerController = collision.GetComponent<PlayerController>();
            if (playerController != null && playerController.bIsJump)
            {
                Debug.Log("���� �� �� Ÿ�� ȿ�� ����");
                return;
            }

            if (IsWalkable())
            {
                GameObject player = collision.gameObject;

                foreach (var tileEffect in GetComponents<ISpecialTile>())
                {
                    tileEffect.Activate(player);
                }
            }
            else
            {
                Debug.Log("����");
                GameManager.Instance.GameOver();
            }
        }
    }


    public bool IsWalkable()
    {
        //return currentTileType != TileType.Danger && currentTileType != TileType.Destroyed;
        return currentTileType != TileType.Destroyed;
    }
    public TileType GetTileType() { return currentTileType; }

    public void SetTileType(TileType inputTileType)
    {
        currentTileType = inputTileType;
        updateTileImage();
        ClearExistingEffects();
        TileEvent();

        //if (currentTileType == TileType.Danger) StartCoroutine(DestroyTile());
    }

    public void ClearExistingEffects()
    {
        foreach (var effect in GetComponents<ISpecialTile>())
        {
            Destroy((Component)effect);
        }
    }

    private void updateTileImage()
    {
        //if(currentTileType == TileType.Destroyed) return;
        int tileIndex = (int)currentTileType;
        if (!spriteRenderer || tileSprite.Length <= tileIndex || tileSprite[tileIndex] == null) return;

        spriteRenderer.sprite = tileSprite[tileIndex];
    }

    //�ļ��� �ı��� Ÿ���� �ð��������� �ٽ� ������� ���ƿ�
    public IEnumerator DestroyTile()
    {
        yield return new WaitForSeconds(1f);
        //Ÿ�� �μ����� �ִϸ��̼� ����
        SoundManager.Instance.PlaySFX(SFXType.TileDestroy);
        SetTileType(TileType.Destroyed);
        //Debug.Log(breakAnimation.Play("Broken") ? "True" : "False");
        //animator.SetTrigger("Play");

        yield return new WaitForSeconds(recoveryTileTime);
        SetTileType(TileType.Normal);
    }
    private void TileEvent()
    {
        switch (currentTileType)
        {
            case TileType.Danger:
                gameObject.AddComponent<DangerTileEffect>();
                StartCoroutine(DestroyTile());
                break;
            case TileType.Electric:
                gameObject.AddComponent<ElectricTileEffect>();
                break;
            case TileType.Trap:
                gameObject.AddComponent<TrapTileEffect>();
                break;
            case TileType.Spin:
                gameObject.AddComponent<SpinTileEffect>();
                break;
            case TileType.Ice:
                gameObject.AddComponent<IceTileEffect>();
                break;
            case TileType.Fog:
                gameObject.AddComponent<FogTileEffect>();
                break;
            case TileType.Random:
                gameObject.AddComponent<RandomTileEffect>();
                break;
        }
    }
    public IEnumerator RevertTile()
    {
        yield return new WaitForSeconds(0f); // Ÿ�� 1�� �� ����
        SetTileType(TileType.Normal);
    }
}
