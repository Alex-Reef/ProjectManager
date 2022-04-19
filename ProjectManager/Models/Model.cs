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
        }

        public void SetProject(Project _project) => project = projects.Find(x=>x.Equals(project));

        public Project GetProject() => project;
        public List<List<Task>> GetTasks() => project.Tasks;
        public List<Marker> GetMarkers() => project.Markers;

        public void RemoveTask(Task task)
        {
            var list = project.Tasks;
            foreach(var item in list)
                item.RemoveAll(r => r.UniqleID == task.UniqleID);
            SaveProject();
        }

        public void MoveTask(Task task, int to)
        {
            RemoveTask(task);
            project.Tasks[to].Add(task);
            SaveProject();
        }

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

        public List<Project> GetProjectsList()
        {
            string[] projectArray = Directory.GetFiles(Environment.CurrentDirectory + "\\Projects\\", "*.json");
            foreach (string file in projectArray)
            {
                string data = File.ReadAllText(file);
                project = JsonConvert.DeserializeObject<Project>(data);
                projects.Add(project);
            }
            return projects;
        }

        public int CreateProject(string name)
        {
            List<Task> NextUp, InProcess, Complete;
            NextUp = new List<Task>();
            InProcess = new List<Task>();
            Complete = new List<Task>();
            List<List<Task>> list = new List<List<Task>>();
            list.Add(NextUp);
            list.Add(InProcess);
            list.Add(Complete);

            List<Marker> markers = new List<Marker>();
            project = new Project()
            {
                Name = name,
                Tasks = list,
                Markers = markers
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

        public int UpdateTask( int Category, Task task, int MarkerID)
        {
            SetMarker(task, MarkerID);
            AddTask(task, Category);
            SaveProject();
            return 0;
        }

        public int AddTask(Task task, int to)
        {
            RemoveTask(task);
            project.Tasks[to].Add(task);
            SaveProject();
            return 0;
        }

        public int AddMarker(Marker marker)
        {
            project.Markers.Add(marker);
            SaveProject();
            return 0;
        }

        public void DeleteMarker(Marker marker)
        {
            project.Markers.RemoveAll(x => x.UniqleID == marker.UniqleID);
            SaveProject();
        }

        public void UpdateMarker(Marker marker)
        {
            int id = project.Markers.FindIndex(x => x.UniqleID == marker.UniqleID);
            project.Markers[id] = marker;
            SaveProject();
        }

        public int SetMarker(Task task, int markerID)
        {
            int id = -1, category = -1;
            for (int i = 0; i < 3; i++)
            {
                id = project.Tasks[i].FindIndex(x => x.UniqleID == task.UniqleID);
                if (id != -1)
                {
                    category = i;
                    break;
                }
            }

            var list = project.Tasks[category];
            list[id].MarkerID = GetMarkers()[markerID].UniqleID;
            return 0;
        }

        public int SaveProject()
        {
            var serializer = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter(Environment.CurrentDirectory + "\\Projects\\" + project.Name + ".json"))
            {
                using (var jsonTextWriter = new JsonTextWriter(fs))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(fs, project);
                }
                fs.Close();
            }
            return 0;
        }
    }
}