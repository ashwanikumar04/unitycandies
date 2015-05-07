using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public int Id;
    public Image currentImage;
    public void Start()
    {
        foreach (Transform localTransform in transform)
        {
            if (localTransform.name == "Id")
            {
                localTransform.GetComponent<Text>().text = (Id).ToString();
            }
        }
    }

    public void OnClick()
    {
        GameObject.Find("ButtonClicked").GetComponent<Text>().text = "Item #" + Id.ToString() + " Clicked";
        Debug.Log("Item #" + Id.ToString() + " Clicked");
    }
}
