using UnityEngine;

public class CollusionCheck : MonoBehaviour
{
    //WallDefiner wallDefiner;

    public void OnTriggerEnter(Collider other)
    {
       // WallDefiner wallDefiner = gameObject.AddComponent<WallDefiner>();
       
        if (other.tag == "Furniture")
        {
            //wallDefiner.SetisTrigger(0);
            Debug.Log("kes");
        }
        else
        {
           // wallDefiner.SetisTrigger(1);
        }
    }
}
