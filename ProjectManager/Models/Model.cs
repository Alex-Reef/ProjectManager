using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectManager.Models;

namespace ProjectManager
{
    public class Model
    {
        private Project project { get; set; }
        private List<Project> projects { get; set; }

        public Model()
        {
            projects = new List<Project>();
            Load();
        }

        public void SetProject(Project _project) => project = projects.Find(x=>x.Equals(project));

        public List<List<Task>> GetTasks() => project.Tasks;
        public List<Marker> GetMarkers() => project.Markers;
        public List<string> GetHeaders() => project.Headers;


        // Get task by key (Task Uniqle ID)
        public Task GetTaskByKey(string key)
        {
            Task task = null;
            for (int i = 0; i < 3; i++)
            {
                task = GetTasks()[i].FirstOrDefault(x => x.UniqleID == key);
                if (task != null)
                    break;
            }
            return task;
        }

        public int CreateProject(string name)
        {
            List<List<Task>> list = new List<List<Task>>() { new List<Task>(), new List<Task>(), new List<Task>()};
            List<string> headerColumns = new List<string>() { 
                "Next Up", "In Process","Complete"
            };

            List<Marker> markers = new List<Marker>();

            project = new Project()
            {
                Name = name,
                Tasks = list,
                Markers = markers,
                Headers = headerColumns
            };

            DirectoryInfo dir = Directory.CreateDirectory("Projects");

            using (StreamWriter file = File.CreateText(Environment.CurrentDirectory + "\\Projects\\"+ project.Name + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, project);
            }

            projects.Add(project);
            return 0;
        }

        public int AddMarker(Marker marker)
        {
            project.Markers.Add(marker);
            Save();
            return 0;
        }

        #region NEW MODEL

        public void GetCurentUser() { }
        public void GetUsers() { }

        public Project GetCurentProject() => project;
        public List<Project> GetProjects() => projects;

        public void Update(Project project) {
            this.project = project;
            Save();
        }

        public void Save() {
            var serializer = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter(Environment.CurrentDirectory + @"\Projects\" + project.Name + ".json"))
            {
                using (var jsonTextWriter = new JsonTextWriter(fs))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(fs, project);
                }
                fs.Close();
            }
        }

        public void Load() 
        {
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Projects"))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory("Projects");
            }

            string[] projectArray = Directory.GetFiles(Environment.CurrentDirectory + @"\Projects\", "*.json");
            foreach (string file in projectArray)
            {
                string data = File.ReadAllText(file);
                project = JsonConvert.DeserializeObject<Project>(data);
                projects.Add(project);
            }
        }

        #endregion
    }
}