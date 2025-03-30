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
        grid.UpdateCellSize(int.Parse(num.text));
        ActionAnim();
    }
}
