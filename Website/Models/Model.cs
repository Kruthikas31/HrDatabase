using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class Model
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string FullName { get; internal set; }
        public string EmailId { get; internal set; }
        public string ErrorMsg { get; internal set; }
    }
}