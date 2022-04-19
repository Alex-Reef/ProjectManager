using System;

namespace ProjectManager.Models
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MarkerID { get; set; }
        public string UniqleID { get; set; }
        public string EndTime { get; set; }
    }
}