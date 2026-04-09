using System;
using Managers;
using UnityEngine;
using UnityEngine.Events;

public class UIEventScript : MonoBehaviour
{

    private GameObject mainObject;
    private UIManager manager;

    [SerializeField] private bool hideable = true;

    public UnityEvent openUI;
    
    void Start()
    {
        mainObject = gameObject;
        manager = GameObject.Find("EventSystem").GetComponent<UIManager>();
        manager.closeUI.AddListener(OnCloseUI);
        openUI.AddListener(manager.OnOpenUI);
    }

    
    public void OpenUI()
    {
        openUI.Invoke();
    }

    private void OnCloseUI()
    {
        if (hideable)
        {
            mainObject.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
