using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectManager.Models;
using System.Windows;

namespace ProjectManager
{
    public class Model
    {
        private Project project { get; set; }
        private User user { get; set; } 
        private List<User> users { get; set; }  
        private List<Project> projects { get; set; }
        private AppSettings appSettings { get; set; }

        public Model()
        {
            projects = new List<Project>();
            appSettings = new AppSettings();
            users = new List<User>();
            Load();
            LoadAppData();
            LoadUserData();
        }

        public void SetProject(Project _project) => project = projects.Find(x=>x.Equals(_project));
        public void SetUser(User _user) => user = users.Find(x=>x.Equals(_user));

        public User GetCurentUser() => user;
        public List<User> GetUsers() => users;
        public AppSettings GetAppSettings() => appSettings;

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

        public void SaveUserData()
        {
            var serializer = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter(Environment.CurrentDirectory + @"\Data\Users.json"))
            {
                using (var jsonTextWriter = new JsonTextWriter(fs))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(fs, users);
                }
                fs.Close();
            }
        }

        public void LoadUserData()
        {
            if (Directory.Exists(Environment.CurrentDirectory + @"\Data\") && Directory.Exists(Environment.CurrentDirectory + @"\Data\Users.json"))
            {
                string data = File.ReadAllText(Environment.CurrentDirectory + @"\Data\Users.json");
                users = JsonConvert.DeserializeObject<List<User>>(data);
            }
            else
            {
                user = new User();
                users.Add(user);
                SaveUserData();
            }
        }

        public void LoadAppData()
        {
            if (Directory.Exists(Environment.CurrentDirectory + @"\Data\"))
            {
                string data = File.ReadAllText(Environment.CurrentDirectory + @"\Data\AppSettings.json");
                appSettings = JsonConvert.DeserializeObject<AppSettings>(data);
            }
            else
            {
                appSettings = new AppSettings();
                SaveAppData();
            }
        }

        public void SaveAppData()
        {
            var serializer = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter(Environment.CurrentDirectory + @"\Data\AppSettings.json"))
            {
                using (var jsonTextWriter = new JsonTextWriter(fs))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(fs, appSettings);
                }
                fs.Close();
            }
        }
    }
}