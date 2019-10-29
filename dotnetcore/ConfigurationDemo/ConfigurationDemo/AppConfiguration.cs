using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationDemo
{
    public class AppConfiguration
    {
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public int ParticipantCount { get; set; }
        public ProjectDetails ProjDetails { get; set; }
    }

    public class ProjectDetails
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
    }
}
