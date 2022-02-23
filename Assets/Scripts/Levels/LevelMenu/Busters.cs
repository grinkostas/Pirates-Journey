using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class Busters : MonoBehaviour
{
    public List<NodeBuff> AllBusters;

    public NodeBuff this[string index]
    {
        get
        {
            return AllBusters.Find(x => x.Id == index);
        }
    }

}
