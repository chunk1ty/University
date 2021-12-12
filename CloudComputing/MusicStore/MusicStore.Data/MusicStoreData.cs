namespace MusicStore.Data
{
    using MusicStore.Data.Repository;
    using MusicStore.Models;
    using System;
    using System.Collections.Generic;

    public class MusicStoreData : IMusicStoreData
    {
        private IMusicStoreDbContext context;
        private IDictionary<Type, object> repositories;

        public MusicStoreData(IMusicStoreDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public MusicStoreData()
            : this(new MusicStoreDbContext())
        {
        }

        public IGenericRepository<Song> Songs
        {
            get 
            {
                return this.GetRepository<Song>();
            }
        }

        public IGenericRepository<Album> Albums
        {
            get
            {
                return this.GetRepository<Album>();
            }
        }

        public IGenericRepository<Artist> Artists
        {
            get
            {
                return this.GetRepository<Artist>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
