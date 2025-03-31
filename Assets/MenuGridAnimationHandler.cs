using UnityEngine;
using TMPro;
public class MenuGridAnimationHandler : MonoBehaviour
{
    Animator anim;
    public TMP_InputField num;
    public GridDisplay grid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void ActionAnim()
    {
        anim.SetTrigger("action");
    }
    public void SetNewGrid()
    {
        try
        {
            int x = int.Parse(num.text);
            if (x > 0 && x <= 100)
            {
                grid.UpdateCellSize(int.Parse(num.text));

            }
            else
            {
                ErrorHandler.errorHandler.Error("Introduzca un valor entero mayor que 0 y menor que 101");
            }
        }
        catch
        {
            ErrorHandler.errorHandler.Error("Introduzca un valor entero mayor que 0 y menor que 101");

        }

        ActionAnim();
    }
}
