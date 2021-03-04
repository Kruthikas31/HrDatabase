using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class BasicInfoDependent
    {
        public BasicInformation basicInformation { get; set; }

        
        public List<DependentDetails> dependentDetails { get; set; }
    }
}