using UnityEngine;

public class WeaponBow : MonoBehaviour
{
    
    private int bowLevelMax = 5;

    public void Use()
    {
        var to = Camera.main.ScreenToWorldPoint(Input.mousePosition); to.z = 0;
        int bowlevel = GetComponentInParent<PlayerTransform>().bowLevel;
        if (bowlevel > bowLevelMax)
            bowlevel = bowLevelMax;
        transform.GetChild(bowlevel - 1).gameObject.GetComponent<Act>().Try_Act(transform.GetComponentInParent<Status>().gameObject, to);
    }
}
