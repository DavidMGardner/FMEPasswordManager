using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FME.PasswordManager.Interfaces;
using FME.PasswordManager.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Serilog;
using StructureMap.Diagnostics;

namespace FME.PasswordManager.Persistence
{
    public class JsonFilePersistence<T> : IEntityPersistence<T>
    {
        private readonly ISerialization<T> _serialization;
        
        public JsonFilePersistence(ISerialization<T> serialization)
        {
            _serialization = serialization;
        }
        
        public bool PutList(List<T> entities)
        {
            return _serialization.SerializeObject(entities);
        }

        public bool AddRange(List<T> entities)
        {
            var list = GetList();
            list.AddRange(entities);
            return PutList(list);
        }

        public List<T> GetList()
        {
            return _serialization.DeserializeObject();
        }

        public bool EnsureList()
        {
            return _serialization.EnsureContainer();
        }
    }
}