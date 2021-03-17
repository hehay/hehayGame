using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WarrningWindow : MonoBehaviour
{

    [SerializeField] private Text text;
    private WarringEvent Event;

    void Awake()
    {
        this.gameObject.SetActive(false);
    }
    public void Active(WarringModel model)
    {
        transform.SetAsLastSibling();
        this.gameObject.SetActive(true);
        text.text = model.valuse;
        Event = model.warringEvent;
        if (model.timer > 0)
        {
            Invoke("Close",model.timer);
        }
    }

    void Close()
    {
        if (IsInvoking("Close")) CancelInvoke("Close");

        if (Event != null)
        {
            Event();
        }
        gameObject.SetActive(false);
    }

}
