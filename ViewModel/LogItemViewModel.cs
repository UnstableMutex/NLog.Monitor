using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MVVMLight.Extras;

namespace NLog.Monitor.ViewModel
{
    class LogItemViewModel:VMB
    {
        private readonly BsonDocument _logitem;
        private readonly string _collectionName;

        public LogItemViewModel(BsonDocument logitem,string collectionName)
        {
            _logitem = logitem;
            _collectionName = collectionName;
        }

        public string Message { get { return _logitem["Message"].AsString; } }
        public string UserName { get { return _logitem["UserName"].AsString; }}
        public Guid ID { get; set; }

    }
}
