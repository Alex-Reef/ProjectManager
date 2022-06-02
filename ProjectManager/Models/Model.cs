using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ProjectManager.Models;

namespace ProjectManager
{
    public class Model
    {
        public Project CurentProject { get; set; }
        public User CurentUser { get; set; }
        public List<User> Users { get; set; }
        public List<Project> Projects { get; set; }
        public AppSettings AppSettings { get; set; }
        public List<Notification> Notifications { get; set; }

        public int MaxMarkers = 15;
        public int MaxTasks = 50;

        public Model()
        {
            Projects = new List<Project>();
            AppSettings = new AppSettings();
            Users = new List<User>();
            Notifications = new List<Notification>();
            Load();
            LoadAppData();
            LoadUserData();
        }

        public void SetProject(Project _project) => CurentProject = Projects.Find(x=>x.Equals(_project));
        public void SetUser(User _user) => CurentUser = Users.Find(x=>x.UniqleID == _user.UniqleID);

        public void Save() {
            var serializer = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter(Environment.CurrentDirectory + @"\Data\Projects\" + CurentProject.Name + ".json"))
            {
                using (var jsonTextWriter = new JsonTextWriter(fs))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(fs, CurentProject);
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
                CurentProject = JsonConvert.DeserializeObject<Project>(data);
                Projects.Add(CurentProject);
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
                    serializer.Serialize(fs, Users);
                }
                fs.Close();
            }
        }

        public void LoadUserData()
        {
            if (File.Exists(Environment.CurrentDirectory + @"\Data\Users.json"))
            {
                string data = File.ReadAllText(Environment.CurrentDirectory + @"\Data\Users.json");
                Users = JsonConvert.DeserializeObject<List<User>>(data);
            }
        }

        public void LoadAppData()
        {
            if (Directory.Exists(Environment.CurrentDirectory + @"\Data\"))
            {
                string data = File.ReadAllText(Environment.CurrentDirectory + @"\Data\AppSettings.json");
                AppSettings = JsonConvert.DeserializeObject<AppSettings>(data);
            }
            else
            {
                AppSettings = new AppSettings();
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
                    serializer.Serialize(fs, AppSettings);
                }
                fs.Close();
            }
        }
    }
}