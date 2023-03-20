using DG.Tweening;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{

    [SerializeField] private float rotationSpeed,yAxisHeight;
    private void OnEnable()
    {
        CollectableManager.OnGoldCollect += MoveToPlayer;
      
    }
    private void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        transform.position = new Vector3(transform.position.x, yAxisHeight, transform.position.z);
    }
    void Update()
    {
       
        transform.Rotate(Vector3.up*Time.deltaTime*rotationSpeed,Space.World);
    }

    private void MoveToPlayer(Vector3 player)
    {
        StartCoroutine(MoveGoldCoroutine(player));
    }   
    private IEnumerator MoveGoldCoroutine(Vector3 gameObject)
    {
       
        this.transform.DOMove(gameObject,1.2f).SetEase(Ease.InOutFlash);
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        
    }
    

   

    private void OnDisable()
    {
       
        CollectableManager.OnGoldCollect -= MoveToPlayer;
    }
}
