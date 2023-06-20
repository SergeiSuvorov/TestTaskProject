using Interface;
using System.IO;
using UnityEngine;

namespace Tools
{
    public sealed class SaveDataRepository
    {
        private readonly IData<SavedData> _data;

        private const string _folderName = "DataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;

        public SaveDataRepository()
        {
            _data = new JsonData<SavedData>();
            _path = Path.Combine(Application.dataPath, _folderName);

        }

        public void Save(SavedData savedData)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            savedData.Name = "Test";
            _data.Save(savedData, Path.Combine(_path, _fileName));

        }

        public SavedData Load()
        {
            var file = Path.Combine(_path, _fileName);
            if (!File.Exists(file)) return null;
            else return _data.Load(file);
        }
    }
}