using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;


public class MoveUI : MonoBehaviour
{
    private float currentMoveSpeed = 0.1f;

    private float duration = 0.1f;

    void FixedUpdate()
    {
        if (FindObjectOfType<Image>().name == "MoneyScoreImage")
        {
            GameObject uiObject = FindObjectOfType<Image>().gameObject;
            Vector3 screenPoint = uiObject.transform.position + new Vector3(0, 0, 5);  //the "+ new Vector3(0,0,5)" ensures that the object is so close to the camera you dont see it

            //find out where this is in world space
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

            ////move towards the world space positions
            //transform.position = Vector3.MoveTowards(transform.position, worldPos, currentMoveSpeed);

            //transform.DOMove(worldPos, duration)
            //       .SetEase(Ease.InSine)
            //       .OnComplete(() =>
            //       {
            //           StartCoroutine(DestroyMoneyPack());
            //       });

            //.OnComplete(() =>
            //{
            //    Destroy(gameObject);
            //    // TODO
            //    // increase money count
            //});
        }
    }

    IEnumerator DestroyMoneyPack()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
