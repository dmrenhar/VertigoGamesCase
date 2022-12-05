using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class whell : MonoBehaviour
{
    bool translatable;

    [SerializeField] Text levelText;
    int levelCount;

    objects obje;

    public GameObject ObjelistPrefab;
    
    public List<GameObject> itemList = new List<GameObject>();
    public List<GameObject> itemPos = new List<GameObject>();
    public List<objects> scriptableObject = new List<objects>();
    [Range(0, 81)] public List<int> RandomCount = new List<int>();

    [SerializeField] int bombIndex;
    public int numberOfObject = 8;
    public float timeRotate;
    public float numberCircleRotate;
    
    float angleOfOneObject;
    const float circle = 360;
    float currentTime;

    int indexObject;

    public AnimationCurve curve;

    public Transform objects;

    public GameObject exitButton;

    private void Start()
    {
        exitButton.SetActive(false);
        translatable = true;
        levelText.text = levelCount.ToString();
        angleOfOneObject = circle / 8;
        SetPositionData();
    }

    private void Update()
    {
        
    }

    IEnumerator RotateWheel()
    {
        float startAngle = transform.eulerAngles.z;
        currentTime = 0;

        #region luck
        int randomCount = Random.Range(1, 82);
        Debug.Log("random sayý "+randomCount);
        if (randomCount <= RandomCount[0])
        {
            indexObject = 1;
        }
        else if (randomCount > RandomCount[0] && randomCount <= RandomCount[1])
        {
            indexObject = 2;
        }
        else if (randomCount > RandomCount[1] && randomCount <= RandomCount[2])
        {
            indexObject = 3;
        }
        else if (randomCount > RandomCount[2] && randomCount <= RandomCount[3])
        {
            indexObject = 3;
        }
        else if (randomCount > RandomCount[3] && randomCount <= RandomCount[4])
        {
            indexObject = 4;
        }
        else if (randomCount > RandomCount[4] && randomCount <= RandomCount[5])
        {
            indexObject = 5;
        }
        else if (randomCount > RandomCount[5] && randomCount <= RandomCount[6])
        {
            indexObject = 6;
        }
        else if (randomCount > RandomCount[6] && randomCount <= RandomCount[7])
        {
             indexObject = 7;
        }
        else if (randomCount > RandomCount[8] && randomCount <= RandomCount[9])
        {
             indexObject = 8;
        }
        #endregion
        Debug.Log("index "+indexObject);


        float angleWant = (numberCircleRotate * circle) + angleOfOneObject * indexObject - startAngle;
        
        while (currentTime < timeRotate)
        {
            translatable = false;
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;

            float angleCurrent = angleWant * curve.Evaluate(currentTime / timeRotate);
            transform.eulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
        }
        yield return new WaitForSeconds(1.5f);
        if (bombIndex == indexObject)
        {
            Debug.Log("Bomb");
            exitButton.SetActive(true);
            translatable = false;
        }
        else
        {
            TheObtainedObject();
            translatable = true;
        }

    }
    void TheObtainedObject()
    {
        GameObject items = Instantiate(ObjelistPrefab, new Vector3(0,0,0),Quaternion.identity);

        items.SetActive(true);
        items.GetComponent<Image>().sprite = scriptableObject[indexObject].objectImage;
        items.transform.SetParent(GameObject.FindGameObjectWithTag("ObjeList").transform, false);
        items.GetComponentInChildren<Text>().text = scriptableObject[indexObject].name;
        
        transform.eulerAngles = new Vector3(0, 0, 0);
        safeZone();
    }

    void safeZone()
    {
        if (levelCount % 5 == 0 && levelCount != 0)
        {
            Debug.Log("risk-free silver spin without bomb.");
            exitButton.SetActive(true);
        }
        else if (levelCount % 30 == 0 && levelCount != 0)
        {
            Debug.Log("risk-free golden spin with special rewards and without bomb.");
            exitButton.SetActive(true);
        }
    }

    [ContextMenu("Rotate")]
    public void RotateNow()
    {
        if (translatable)
        {
           StartCoroutine(RotateWheel());
           levelCount++;
           levelText.text = levelCount.ToString();
        }
    }
    void SetPositionData()
    {
        for (int i = 0; i < objects.childCount; i++)
        {
            Debug.Log("baþladý");
            itemList[i].GetComponent<Image>().sprite = scriptableObject[i].objectImage;
            itemList[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(75, 75);
            GameObject items = Instantiate(itemList[i], new Vector3(objects.GetChild(i).transform.localPosition.x, objects.GetChild(i).transform.localPosition.y, objects.GetChild(i).transform.localPosition.z),Quaternion.identity);
            items.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        }
    }

}
