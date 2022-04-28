using UnityEngine;

public class CollusionCheck : MonoBehaviour
{
    

    public void OnTriggerEnter(Collider other)
    {
       // WallDefiner wallDefiner = gameObject.AddComponent<WallDefiner>();
       
        if (other.tag == "Furniture")
        {
            
            Debug.Log("kes");
            WallDefiner.instance.x=2;
        }
        else
        {
            WallDefiner.instance.x=1;
        }
    }
}
