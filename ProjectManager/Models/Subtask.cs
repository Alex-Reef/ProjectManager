using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class Subtask
    {
        public string Name { get; set; }
        public bool Complete { get; set; }
        public string UniqleID { get; set; }

        public Subtask(string name, bool complete, string ID)
        {
            Name = name;
            Complete = complete;
            UniqleID = ID;
        }
    }
}
