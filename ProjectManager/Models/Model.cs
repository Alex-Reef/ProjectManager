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
        private AppSettings appSettings { get; set; }

        public Model()
        {
            projects = new List<Project>();
            Load();
        }

        public void SetProject(Project _project) => project = projects.Find(x=>x.Equals(_project));

        public void GetCurentUser() { }
        public void GetUsers() { }

        public Project GetCurentProject() => project;
        public List<Project> GetProjects() => projects;

        public void Save() {
            var serializer = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter(Environment.CurrentDirectory + @"\Data\Projects\" + project.Name + ".json"))
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
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Data\Projects"))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(@"Data\Projects\");
            }

            string[] projectArray = Directory.GetFiles(Environment.CurrentDirectory + @"\Data\Projects\", "*.json");
            foreach (string file in projectArray)
            {
                string data = File.ReadAllText(file);
                project = JsonConvert.DeserializeObject<Project>(data);
                projects.Add(project);
            }
        }

        public void LoadAppData()
        {
            if (Directory.Exists(Environment.CurrentDirectory + @"\AppSettings.json"))
            {
                string data = File.ReadAllText(Environment.CurrentDirectory + @"\AppSettings.json");
                appSettings = JsonConvert.DeserializeObject<AppSettings>(data);
            }
        }

        public void SaveAppData()
        {
            var serializer = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter(Environment.CurrentDirectory + @"\AppSettings.json"))
            {
                using (var jsonTextWriter = new JsonTextWriter(fs))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(fs, project);
                }
                fs.Close();
            }
        }
    }
}