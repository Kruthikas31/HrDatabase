using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class EducationExperience
    {
        public List<EducationDetails> educationDetails { get; set; }

        public List<ExperienceDetails> experienceDetails { get; set; }
    }
}