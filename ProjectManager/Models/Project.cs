using System.Collections.Generic;

namespace ProjectManager.Models
{
    public class Project
    {
        public string Name { get; set; }
        public List<List<Task>> Tasks {get; set; }
        public List<Marker> Markers { get; set; }
    }
}