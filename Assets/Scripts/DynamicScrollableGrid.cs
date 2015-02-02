using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DynamicScrollableGrid : MonoBehaviour
{
    /// <summary>
    /// Scroll bar for the list
    /// </summary>
    public Scrollbar scrollBar;
    /// <summary>
    /// Item prefab
    /// </summary>
    public GameObject item;
    List<GameObject> items;
    /// <summary>
    /// Grid which contains the items
    /// </summary>
    public GridLayoutGroup grid;
    public ScrollRect scrollRect;
    public Text currentCount;
    // Use this for initialization
    Vector3 autoLocalScale;

    void Start()
    {
        items = new List<GameObject>();
        autoLocalScale = new Vector3(1, 1, 1);
        RefreshList();
    }

    public void RefreshList()
    {
        StartCoroutine(GenerateItems());
    }

    public IEnumerator GenerateItems()
    {
        System.Random rand = new System.Random();
        currentCount.text = "Current Count: " + 0.ToString();
        ///Clearing old list
        items.ForEach((go) => GameObject.Destroy(go));
        items.Clear();

        int randomItemCount = rand.Next(20, 100);
        for (int index = 0; index < randomItemCount; index++)
        {
            GameObject localItem = (GameObject)Instantiate(item, Vector3.zero, Quaternion.identity);
            ///Setting parent for the item
            localItem.transform.SetParent(grid.transform);
            localItem.GetComponent<Item>().Id = index + 1;

            ///Important as instatiated item can have random scale and might not be visible
            localItem.transform.localScale = autoLocalScale;
            localItem.transform.localPosition = Vector3.zero;
            items.Add(localItem);
        }
        yield return new WaitForSeconds(.1f);
        ///1. Two ways to move the scroll to first item
        scrollRect.horizontalNormalizedPosition = 0;
        currentCount.text = "Current Count: " + randomItemCount.ToString();

        //2. Moving the scroll bar to left
        //scrollBar.value = 0;
    }
}
