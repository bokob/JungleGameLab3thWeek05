using UnityEngine;

public class ChainHead : MonoBehaviour
{
    [SerializeField] GameObject bodypart;
    [SerializeField] float distance;
    [SerializeField] int num;


    GameObject before;

    void Start()
    {
        before = gameObject;

        for (int i = 0; i < num; i++)
        {
            var v = Instantiate(bodypart, transform.position, transform.rotation);
            var chain = v.GetComponent<Aazz0200_Chain>();
            if (before)
            {
                chain.follow = before;
                before = v;
            }
            chain.distance = distance;
        }
    }
}
