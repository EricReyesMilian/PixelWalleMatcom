using UnityEngine;
using TMPro;

public class ErrorHandler : MonoBehaviour
{
    public static ErrorHandler errorHandler;
    public TextMeshProUGUI errorSMS;
    public Animator anim;
    bool onScreen;
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
        //errorSMS.onValueChanged.AddListener(Show);

    }
    public void Warning(string message)
    {
        errorSMS.text += message + '\n';
        //Show(message);

    }
    public void AddError(string message)
    {
        errorSMS.text += message;
        errorSMS.text += "\n";

    }
    public void Error(string message)
    {

        errorSMS.text += message + '\n';
    }
    public void Info(string message)
    {

        errorSMS.text += message + '\n';
        //Show(message);
    }
    public void Show()
    {

        anim.SetTrigger("show");

        onScreen = true;
    }
    public void Close()
    {
        anim.SetTrigger("close");
        onScreen = false;
    }
    public void ActionButton()
    {
        if (!onScreen)
        {
            anim.SetTrigger("show");
            onScreen = true;
        }
        else
        {
            Close();

        }

    }
}
