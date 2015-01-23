using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class FocusCamera : MonoBehaviour
{

    public GameObject targetObject;
    private Vector3 localPosition;
    void Start()
    {
        localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        AnimateIt();
    }

    void AnimateIt()
    {
        StartCoroutine(FocusInAndOut());
    }

    IEnumerator FocusInAndOut()
    {
        yield return new WaitForSeconds(2.5F);
        HOTween.To(gameObject.transform, 1.5f,
new TweenParms().Prop("position", targetObject.transform.position, false).Ease(EaseType.Linear));
        yield return new WaitForSeconds(2.5F);
        HOTween.To(gameObject.transform, 1.5f, new TweenParms().Prop("position", localPosition, false).Ease(EaseType.Linear).OnComplete(StartAction));
    }
   

    private void StartAction()
    {
        Debug.Log("StartAction");
        StartCoroutine(Repeat());
    }

    IEnumerator Repeat()
    {
        yield return new WaitForSeconds(2.0f);
        AnimateIt();
    }
}
