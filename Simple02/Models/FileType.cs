using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    public enum FileType
    {
        Ref = 1, Photo, //this type works for nearly all formats of photos
        Tex =2, Other,
        //how to define other file type ?
        
    }
}