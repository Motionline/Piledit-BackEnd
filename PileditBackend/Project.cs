using PileditBackend.TL;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace PileditBackend
{
    public class Project
    {
        public Size OutputSize { get; private set; }
        public string ProjectPath { get; }
        public string ProjectName { get; }
        public IReadOnlyList<string> ObjectsPath { get; }
        public Timeline Timeline { get; }

        private Project(string path, string name, Size size, Timeline timeline)
        {
            OutputSize = size;
            ProjectPath = Path.GetDirectoryName(path);
            ProjectName = name;
            Timeline = timeline;
        }

        public void ChangeSize(int width, int heigth)
        {
            OutputSize = new Size(width, heigth);
        }

        public static Project Create(string name, Size size)
        {
            return new(null, name, size, new Timeline());
        }

        public void Save()
        {
            SaveNewFile(ProjectPath, FileMode.Open);
        }

        public void SaveNewFile(string file, FileMode mode = FileMode.CreateNew)
        {
            using var stream = new FileStream(file, mode, FileAccess.Write);
            Console.WriteLine(this);
            //BinaryFormatter bf = new BinaryFormatter();
            //bf.Serialize(stream, this);
        }
    }
}
