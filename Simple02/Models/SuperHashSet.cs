using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple02.Models;


namespace Simple02.Models
{
    public class SuperHashSet<T> :HashSet<T>
    {

        
        /*
        public bool AddWhileDiff(T other)
        {
            //case4.Deng((QCase)other);
            //(QCase)other.Deng(case4);
            foreach (T element in this)
            {
                if ((QCase)element.Deng((QCase)other))
                    return false;
            }
            Add(other);
            return true;
        }
        */
    }
}