using UnityEngine;

public class WeaponStaff : MonoBehaviour
{

    private int staffLevelMax = 5;

    public void Use()
    {
        var to = Camera.main.ScreenToWorldPoint(Input.mousePosition); to.z = 0;
        int stafflevel = GetComponentInParent<PlayerTransform>().staffLevel;
        if (stafflevel > staffLevelMax)
            stafflevel = staffLevelMax;
        transform.GetChild(stafflevel - 1).gameObject.GetComponent<Act>().Try_Act(transform.GetComponentInParent<Status>().gameObject, to);
    }
}
