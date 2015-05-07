using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DynamicScrollableGridFromWeb : MonoBehaviour
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
        currentCount.text = "Current Count: " + 0.ToString();
        ///Clearing old list
        items.ForEach((go) => GameObject.Destroy(go));
        items.Clear();

        RequestManager.Instance.AddRequest(new Request()
        {
            URL = "http://www.ashwanik.in/blog/getdynamicgriddata",
            OnComplete = (www, go) =>
            {
                if (string.IsNullOrEmpty(www.error))
                {
                    Debug.Log(www.text);
                    DynamicGridData[] data = Pathfinding.Serialization.JsonFx.JsonReader.Deserialize<DynamicGridData[]>(www.text);
                    if (data != null)
                    {
                        for (int index = 0; index < data.Length; index++)
                        {
                            GameObject localItem = (GameObject)Instantiate(item, Vector3.zero, Quaternion.identity);
                            ///Setting parent for the item
                            localItem.transform.SetParent(grid.transform);
                            localItem.GetComponent<Item>().Id = data[index].Id;
                            RequestManager.Instance.AddRequest(new Request()
                              {
                                  URL = data[index].IconPath,
                                  CurrentGameObject = localItem,
                                  OnComplete = (imageWWW, imageGo) =>
                                  {
                                      if (imageGo != null)
                                      {
                                          Sprite sprite = new Sprite();
                                          sprite = Sprite.Create(imageWWW.texture, new Rect(0, 0, 36, 36), Vector2.zero);
                                          imageGo.GetComponent<Item>().currentImage.sprite = sprite;
                                          imageGo.GetComponent<Item>().currentImage.gameObject.SetActive(true);
                                      }
                                  }
                              });
                            ///Important as instatiated item can have random scale and might not be visible
                            localItem.transform.localScale = autoLocalScale;
                            localItem.transform.localPosition = Vector3.zero;
                            items.Add(localItem);
                        }
                        StartCoroutine(InducedDelay());
                        currentCount.text = "Current Count: " + data.Length.ToString();
                    }
                }
                else
                {
                    Debug.Log(www.error);
                }
                //Sprite sprite = new Sprite();
                //sprite = Sprite.Create(www.texture, new Rect(0, 0, 72, 72), Vector2.zero);
                //action.promoImage.sprite = sprite;
                //action.promoImage.gameObject.SetActive(true);
            }
        });
        yield return null;
        //2. Moving the scroll bar to left
        //scrollBar.value = 0;
    }

    IEnumerator InducedDelay()
    {
        yield return new WaitForSeconds(.1f);
        scrollRect.horizontalNormalizedPosition = 0;
    }
}

public class DynamicGridData
{
    public int Id { get; set; }
    public string IconPath { get; set; }
}