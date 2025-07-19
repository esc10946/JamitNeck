using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{

    [SerializeField] Image TutorialImage;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button closeButton;
    private int index = 0;

    public Sprite[] TutorialImages;

    void Start()
    {
        leftButton.onClick.AddListener(OnClickLeftButton);
        rightButton.onClick.AddListener(OnClickRightButton);
        closeButton.onClick.AddListener(OnClickCloseButton);

        TutorialImage.sprite = TutorialImages[index];
    }
    
    void OnClickLeftButton()
    {
        if (index == 0) { return; }
        TutorialImage.sprite = TutorialImages[index - 1];
        index--;

    }

    void OnClickRightButton()
    {
        if (index == 2) { return; }
        TutorialImage.sprite = TutorialImages[index + 1];
        index++;
    }
    void OnClickCloseButton()
    {
        //Todo: ���� �Ŵ��� �޼ҵ� ȣ��(�ΰ��� UI���� ���� �����ϱ�)
        gameObject.SetActive(false);
    }
}
