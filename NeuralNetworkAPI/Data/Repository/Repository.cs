using NeuralNetworkAPI.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetworkAPI.Data.Repository
{
    public class Repository<T> where T : IHasId
    {
        public Repository()
        {
            if (!File.Exists(FileName)) {
                try {
                    File.WriteAllText(FileName, JsonConvert.SerializeObject(new List<T>()));
                } catch(Exception exception) {
                    Logger.LogException(exception);
                }
            }
        }

        public T Get(long id)
        {
            return GetAll().FirstOrDefault(x => x.Id.Value == id);
        }

        public List<T> GetAll()
        {
            try {
                return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(FileName));
            } catch (Exception exception) {
                Logger.LogException(exception);
                return null;
            }
        }

        public T Save(T data)
        {
            try {
                List<T> all = GetAll();
                if(data.Id.HasValue) {
                    all = all.Where(x => x.Id != data.Id).ToList();
                    all.Add(data);
                } else {
                    if(all.Count == 0) {
                        data.Id = 0;
                    } else {
                        data.Id = all.OrderByDescending(x => x.Id.Value).First().Id.Value + 1;
                    }
                    all.Add(data);
                }
                File.WriteAllText(FileName, JsonConvert.SerializeObject(all));
                return data;
            } catch (Exception exception) {
                Logger.LogException(exception);
                return default(T);
            }
        }

        public bool Delete(T data)
        {
            if(!data.Id.HasValue || data.Id.Value < 0 || Get(data.Id.Value) == null) {
                return false;
            }
            try {
                File.WriteAllText(FileName, JsonConvert.SerializeObject(GetAll().Where(x => x.Id != data.Id).ToList()));
            } catch (Exception exception) {
                Logger.LogException(exception);
                return false;
            }
            return true;
        }

        private string FileName
        {
            get {
                return string.Format("{0}/{1}.json", Settings.RepositoryFileLocation, typeof(T).FullName);
            }
        }
    }
}
