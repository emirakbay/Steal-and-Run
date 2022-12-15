using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class StealMoneyEffect : MonoBehaviour
{
    //[Header("UI references")]
    ////[SerializeField] TMP_Text coinUIText;
    //[SerializeField] GameObject moneyPrefab;
    //[SerializeField] Transform target;
    //public GameObject thief;

    public GameObject moneyPrefab;

    [Space]
    [Header("Available money : (money to pool)")]
    [SerializeField] int maxMoney;
    Queue<GameObject> moneyQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation Settings")]
    [SerializeField][Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField][Range(0.9f, 2.0f)] float maxAnimDuration;

    //public GameObject ui;
    private float currentMoveSpeed = 0.1f;
    private Vector3 worldPos;

    void FixedUpdate()
    {
        ////Get the location of the UI element you want the 3d onject to move towards
        //Vector3 screenPoint = ui.transform.position + new Vector3(0, 0, 5);  //the "+ new Vector3(0,0,5)" ensures that the object is so close to the camera you dont see it

        ////find out where this is in world space
        //worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

        //move towards the world space position
        //transform.position = Vector3.MoveTowards(transform.position, worldPos, currentMoveSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Awake()
    {
        //targetPosition = target.position;
        // prepare pool

        //PrepareMoneyPacks();
    }

    private void PrepareMoneyPacks()
    {
        //Quaternion leftRotation = Quaternion.Euler(moneyPrefab.transform.rotation.x, moneyPrefab.transform.rotation.y, -70);
        //Quaternion rightRotation = Quaternion.Euler(moneyPrefab.transform.rotation.x, moneyPrefab.transform.rotation.y, 120);
        GameObject money;
        for (int i = 0; i < maxMoney; i++)
        {
            if (transform.parent.CompareTag("RightHand"))
            {
                money = Instantiate(moneyPrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Quaternion.identity, transform);
                //money.SetActive(false);
                moneyQueue.Enqueue(money);
            }
            if (transform.parent.CompareTag("LeftHand"))
            {
                money = Instantiate(moneyPrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Quaternion.identity, transform);
                //money.SetActive(false);
                moneyQueue.Enqueue(money);
            }
        }
    }

    public void Animate()
    {
        for (int i = 0; i < 2; i++)
        {
            // check if there's coins in the pool
            if (moneyQueue.Count > 0)
            {
                // extract a coin from the pool
                GameObject money = moneyQueue.Dequeue();
                money.SetActive(true);
                // move coin to the collected coin pos 
                //money.transform.position = collectedMoneyPosition;
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                //money.transform.DOMove(worldPos, duration)
                //    .SetEase(Ease.InSine)
                //    .OnComplete(() =>
                //    {
                //        money.SetActive(false);
                //        moneyQueue.Enqueue(money);
                //        // TODO
                //        // increase money count
                //    });
            }
        }
    }

    //public void AddMoney(Vector3 collectedMoneyPosition, int amount)
    //{
    //    Animate(collectedMoneyPosition, amount);
    //}

    private void InstantiateMoneyPack()
    {
        Quaternion leftRotation = Quaternion.Euler(moneyPrefab.transform.rotation.x, moneyPrefab.transform.rotation.y, -70);
        Quaternion rightRotation = Quaternion.Euler(moneyPrefab.transform.rotation.x, moneyPrefab.transform.rotation.y, 120);

        if (transform.parent.CompareTag("RightHand"))
        {
            Instantiate(moneyPrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), rightRotation, transform);
        }

        if (transform.parent.CompareTag("LeftHand"))
        {
            Instantiate(moneyPrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), leftRotation, transform);
        }
    }
}
