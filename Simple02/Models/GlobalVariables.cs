using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    public static class GlobalVariables
    {
        static int _Bar = 0;
        public static int Bar
        {
            get
            {
                return _Bar;
            }
            set
            {
                _Bar = value;
            }
        }
    }
}