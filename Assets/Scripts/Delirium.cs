using UnityEngine;
using UnityEngine.UI;

public class Delirium : MonoBehaviour
{
    public Text deliriumText;
    public Image deliriumBar;

    float delirium, maxDelirium = 10;

    private void Start()
    {
        delirium = maxDelirium;
    }
    private void Update()
    {
        deliriumText.text = delirium + "";
        if (delirium > maxDelirium) delirium = maxDelirium;

        DeliriumBarFiller();
    }

    void DeliriumBarFiller()
    {
        deliriumBar.fillAmount = delirium / maxDelirium;
    }

    public void Delirious(float deliriousPoints)
    {
        if (delirium > 0)
            delirium -= deliriousPoints;
    }

    public void Sane(float sanePoints)
    {
        if (delirium < maxDelirium)
            delirium += sanePoints;
    }
}


