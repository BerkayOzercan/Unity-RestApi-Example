using System;
using System.Collections.Generic;
using System.IO;
using Assets.GameSystem.Scripts;
using UnityEngine;

namespace Assets.SaveSystem.Scripts
{
    public class FileDataService : IDataService
    {
        ISerializer _serializer;
        string _dataPath;
        string _fileExtension;

        public FileDataService(ISerializer serializer)
        {
            _serializer = serializer;
            _dataPath = Application.persistentDataPath;
            _fileExtension = ".json";
            Debug.Log($"FileDataService initialized with path: {_dataPath}");
        }

        string GetPathToFile(string fileName)
        {
            return Path.Combine(_dataPath, string.Concat(fileName, _fileExtension));
        }

        public void Save(GameData data, bool overwrite = true)
        {
            string fileLocation = GetPathToFile(data.Name);

            if (!overwrite && File.Exists(fileLocation))
                throw new IOException($"The file {data.Name}.{_fileExtension} already exist and cannot be overwritten");

            File.WriteAllText(fileLocation, _serializer.Serialize(data));
        }

        public GameData Load(string name)
        {
            string fileLocation = GetPathToFile(name);

            if (!File.Exists(fileLocation))
                throw new ArgumentException($"No persisted GameData with name {name}");

            return _serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
        }

        public IEnumerable<string> ListSaves()
        {
            foreach (string path in Directory.EnumerateFiles(_dataPath))
                if (Path.GetExtension(path) == _fileExtension)
                    yield return Path.GetFileNameWithoutExtension(path);
        }

        public void Delete(string name)
        {
            string fileLocation = GetPathToFile(name);

            if (File.Exists(fileLocation))
                File.Delete(fileLocation);
        }

        public void DeleteAll()
        {
            foreach (string filePath in Directory.GetFiles(_dataPath))
                File.Delete(filePath);
        }
    }
}