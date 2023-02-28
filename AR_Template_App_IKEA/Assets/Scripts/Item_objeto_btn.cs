using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_objeto_btn : MonoBehaviour
{
    public string ItemName;
    public Sprite ItemImage;
    public GameObject ItemPrefab;
    
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ItemName;
        transform.GetChild(1).GetComponent<Image>().sprite = ItemImage;

        var button = GetComponent<Button>();
        button.onClick.AddListener(CreateItem);

    }

    private void CreateItem()
    {
        Instantiate(ItemPrefab);
    }
}
