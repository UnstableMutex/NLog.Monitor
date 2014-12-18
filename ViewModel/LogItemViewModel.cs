using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace NLog.Monitor.ViewModel
{
    class LogItemViewModel
    {
        private readonly BsonDocument _logitem;
        private readonly string _collectionName;

        public LogItemViewModel(BsonDocument logitem,string collectionName)
        {
            _logitem = logitem;
            _collectionName = collectionName;
        }

        public string Message { get; set; }
        public string UserName { get; set; }
        public Guid ID { get; set; }

    }
}
