using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Logic.BAL
{
    public class SaverAndLoader
    {
        private static Dictionary<Guid, IOLock> locksById = new Dictionary<Guid, IOLock>();
        private static object locksByIdLock = new object();

        private static void RemoveIdToLock(Guid id)
        {
            lock (locksByIdLock)
            {
                if (locksById.ContainsKey(id))
                {
                    locksById[id].Count--;
                    if (locksById[id].Count <= 0)
                    {
                        locksById.Remove(id);
                    }
                }
            }
        }

        private static void AddIdToLock(Guid id)
        {
            lock (locksByIdLock)
            {
                if (locksById.ContainsKey(id))
                {
                    locksById[id].Count++;
                }
                else
                {
                    locksById.Add(id, new IOLock(id));
                }
            }
        }

        private static object GetLockById(Guid id)
        {
            lock (locksByIdLock)
            {
                return locksById[id].Lock;
            }
        }

        public static void Save(ISaveLoadable savable)
        {
            string dataPath;
            System.Xml.Serialization.XmlSerializer serialiser;
            object lockObject;

            AddIdToLock(savable.GetId());
            lockObject = GetLockById(savable.GetId());

            serialiser = new System.Xml.Serialization.XmlSerializer(savable.GetType());

            dataPath = Paths.GetUserForkandBeardDataSubFolderPath(savable.GetSubDirectoryPath());
            lock (lockObject)
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(dataPath, String.Format("{0}.xml", savable.GetId().ToString())),  System.IO.FileMode.Create))
                {
                    serialiser.Serialize(fs, savable);
                }
            }
            RemoveIdToLock(savable.GetId());
        }

        public static void Delete(ISaveLoadable savable)
        {
            object lockObject;

            AddIdToLock(savable.GetId());
            lockObject = GetLockById(savable.GetId());

            lock (lockObject)
            {
                System.IO.File.Delete(System.IO.Path.Combine(Paths.GetUserForkandBeardDataSubFolderPath(savable.GetSubDirectoryPath()), String.Format("{0}.xml", savable.GetId().ToString())));
            }
        }

        public static T Load<T>(string subDirectoryPath, Guid id) where T : ISaveLoadable
        {
            string dataPath;
            object lockObject;
            T loaded;

            AddIdToLock(id);
            lockObject = GetLockById(id);

            System.Xml.Serialization.XmlSerializer serialiser;
            serialiser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            dataPath = Paths.GetUserForkandBeardDataSubFolderPath(subDirectoryPath);
            dataPath = System.IO.Path.Combine(dataPath, String.Format("{0}.xml", id.ToString()));

            lock (lockObject)
            {
                loaded = Util.Serialisation.XML.Deserialise<T>(dataPath);
            }

            RemoveIdToLock(id);
            return loaded;
        }

        public static List<T> LoadAll<T>(string subDirectoryPath) where T : ISaveLoadable
        {
            List<T> saveLoadables = new List<T>();
            SortedDictionary<int, List<T>> sortedSaveLoadables = new SortedDictionary<int, List<T>>();
            string dataPath;
            T item;

            System.Xml.Serialization.XmlSerializer serialiser;
            serialiser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            dataPath = Paths.GetUserForkandBeardDataSubFolderPath(subDirectoryPath);

            foreach (string xmlFileName in System.IO.Directory.GetFiles(dataPath, @"*.xml", System.IO.SearchOption.AllDirectories))
            {
                item = Util.Serialisation.XML.Deserialise<T>(xmlFileName);
                if (!sortedSaveLoadables.ContainsKey(item.GetIndex()))
                {
                    sortedSaveLoadables.Add(item.GetIndex(), new List<T>());
                }
                sortedSaveLoadables[item.GetIndex()].Add(item);
            }

            foreach (int index in sortedSaveLoadables.Keys)
            {
                saveLoadables.AddRange(sortedSaveLoadables[index]);
            }

            return saveLoadables;
        }

        private class IOLock
        {
            public int Count { get; set; }
            public Guid Id { get; set; }
            public object Lock { get; set; }

            public IOLock()
            {
                this.Lock = new object();
                this.Count = 1;
            }

            public IOLock(Guid id)
                :this()
            {
                this.Id = id;
            }
        }
    }
}
