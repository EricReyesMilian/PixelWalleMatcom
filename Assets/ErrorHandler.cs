using UnityEngine;
using TMPro;

public class ErrorHandler : MonoBehaviour
{
    public static ErrorHandler errorHandler;
    public TMP_InputField errorSMS;
    public Animator anim;
    void Awake()
    {
        if (errorHandler == null)
        {
            errorHandler = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        errorSMS.onValueChanged.AddListener(Show);

    }
    public void Warning(string message)
    {
        errorSMS.textComponent.color = Color.yellow;
        errorSMS.text = message;
        Show(message);

    }
    public void Error(string message)
    {
        errorSMS.textComponent.color = Color.red;

        errorSMS.text = message;
        Show(message);
    }
    public void Info(string message)
    {
        errorSMS.textComponent.color = Color.green;

        errorSMS.text = message;
        Show(message);
    }
    void Show(string text)
    {
        anim.SetTrigger("show");
    }
}
