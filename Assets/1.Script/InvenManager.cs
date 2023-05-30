using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InvenManager : MonoBehaviour
{
    [SerializeField] public Canvas invenCanvas;
    [SerializeField] Image inven;
    [SerializeField] Image scroll;
    public Image skulIcon;
    public Sprite nullSprite;
    public List<Image> skuls = new List<Image>();
    public List<Image> items = new List<Image>();
    public List<TMP_Text> testx = new List<TMP_Text>();

    void Start()
    {
        //inven.rectTransform.localScale = new Vector3(1, 1, 1);   
        StartCoroutine("ScrollAction");
    }
    void Update()
    {

    }

    IEnumerable ScrollAction()
    {
        for(int i = 0; i <= 10; i++)
        {
            inven.rectTransform.localScale += new Vector3(i, 1 , 1);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
    }
}
