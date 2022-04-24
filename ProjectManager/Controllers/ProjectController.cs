using Newtonsoft.Json;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectManager.Controllers
{
    public class ProjectController : IController<Project>
    {
        private Model Model { get; set; }

        public ProjectController(Model model)
        {
            this.Model = model;
        }

        public void Create(Project _project) {
            var list = Model.GetProjects();

            Project project = new Project() { 
                Name = _project.Name, 
                Headers = new List<string>() { "Next Up", "In Process", "Complete" },
                Markers = new List<Marker>(),
                Tasks = new List<List<Task>>() { 
                    new List<Task>(), 
                    new List<Task>(), 
                    new List<Task>()
                }
            };

            using (StreamWriter file = File.CreateText(Environment.CurrentDirectory + @"\Data\Projects\" + project.Name + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, project);
            }

            list.Add(project);
        }

        public void Update(Project value) {
            var list = Model.GetProjects();
            var index = list.FindIndex(x=>x.Equals(value));
            list[index] = value;
            Model.Save();
        }

        public void Delete(Project value) {
            var list = Model.GetProjects();
            list.Remove(value);
            Model.Save();
        }
    }
}
