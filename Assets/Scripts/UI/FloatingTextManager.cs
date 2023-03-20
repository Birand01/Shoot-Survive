using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FloatingTextPool))]
public class FloatingTextManager : MonoBehaviour
{
    private FloatingTextPool floatingTextPool;
    public GameObject floatingTextPrefab,experienceTextPrefab;
    private int floatTextPoolCount = 10;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float deactivateTime;

    private void OnEnable()
    {
        EnemyHealth.OnEnemyFloatTextAnimation += ShowFloatingText;
        EnemyHealth.OnPlayerExperienceText += ShowExperienceText;
    }
    private void Awake()
    {
        floatingTextPool = GetComponent<FloatingTextPool>();
        offset = new Vector3(offset.x, offset.y, offset.z);
    }
    private void Start()
    {
        floatingTextPool.Inýtýalize(floatingTextPrefab, floatTextPoolCount);
        floatingTextPool.Inýtýalize(experienceTextPrefab, floatTextPoolCount);
    }

    private void ShowFloatingText(GameObject position, float health)
    {
        GameObject floatingTextGO = floatingTextPool.CreateObject();
        floatingTextGO.transform.localPosition = position.transform.position + offset;
        floatingTextGO.gameObject.GetComponent<TextMesh>().text = health.ToString();
        floatingTextGO.gameObject.GetComponent<TextMesh>().color = Color.white;
        StartCoroutine(DeactivateFloatingText(floatingTextGO));

    }
    private void ShowExperienceText(GameObject position, float experience)
    {
        GameObject experienceTextGO = floatingTextPool.CreateObject();
        experienceTextGO.transform.localPosition = position.transform.position + offset;
        experienceTextGO.gameObject.GetComponent<TextMesh>().text = experience.ToString();
        experienceTextGO.gameObject.GetComponent<TextMesh>().color = Color.HSVToRGB(106,57,66);
        StartCoroutine(DeactivateFloatingText(experienceTextGO));
    }
    

    private IEnumerator DeactivateFloatingText(GameObject floatingText)
    {
        yield return new WaitForSeconds(deactivateTime);
        floatingText.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        EnemyHealth.OnPlayerExperienceText -= ShowExperienceText;
        EnemyHealth.OnEnemyFloatTextAnimation -= ShowFloatingText;
    }
}
