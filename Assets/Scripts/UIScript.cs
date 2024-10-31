using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject aimUI;

    private void Update()
    {
        UiToggle();
    }

    void UiToggle()
    {
        if (PlayerMovement.Instance.isPickedCheck())
        {
            aimUI.SetActive(false);
        }
        else
        {
            aimUI.SetActive(true);
        }
    }
}
