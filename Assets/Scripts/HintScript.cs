using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GlobalScript;
using Image = UnityEngine.UIElements.Image;

public class HintScript : MonoBehaviour
{

    private GameObject shownText;
    private GameObject shownImage;
    [SerializeField] private string keyText;
    [SerializeField] private operation keyOperation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var thisObject = gameObject.transform;
        for (int i = 0; i < thisObject.childCount; i++)
        {
            var child = thisObject.GetChild(i);
            switch (child.name)
            {
                case "ShownText":
                    child.GetComponent<TextMeshProUGUI>().text = wordDictionary[keyText];
                    break;
                case "ShownImage":
                    child.GetComponent<Image>().sprite = iconDictionary[keyOperation];
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
