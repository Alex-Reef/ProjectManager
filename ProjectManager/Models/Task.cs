using System.Collections.Generic;

namespace ProjectManager.Models
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> MarkersID { get; set; }
        public string UniqleID { get; set; }
        public string EndTime { get; set; }
    }
}