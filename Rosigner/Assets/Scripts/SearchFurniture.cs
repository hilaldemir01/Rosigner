using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchFurniture : MonoBehaviour
{
    public string theName;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SearchBar(string searchFurniture)
    {
       theName = searchFurniture;
       Debug.Log(""+theName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
