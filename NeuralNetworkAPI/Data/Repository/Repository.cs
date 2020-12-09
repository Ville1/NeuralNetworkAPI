using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkAPI.Data.Repository
{
    public class Repository<T> where T : IHasId
    {
        private List<T> all;

        public Repository()
        {
            all = new List<T>();
        }

        public T Get(long id)
        {
            return all.FirstOrDefault(x => x.Id == id);
        }

        public List<T> GetAll()
        {
            return all.Select(x => x).ToList();
        }

        public T Save(T data)
        {
            if(data.Id >= 0) {
                all = all.Where(x => x.Id != data.Id).ToList();
                all.Add(data);
                return data;
            } else {
                if(all.Count == 0) {
                    data.Id = 0;
                } else {
                    data.Id = all.OrderByDescending(x => x.Id).First().Id + 1;
                }
                all.Add(data);
                return data;
            }
        }
    }
}
