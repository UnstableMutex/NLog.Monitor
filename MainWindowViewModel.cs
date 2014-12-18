using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NLog.Monitor.Annotations;
using NLog.Monitor.ViewModel;

namespace NLog.Monitor
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            var db = new MongoClient(Settings.Server).GetServer().GetDatabase("Logs");
            LogItems = new LinkedList<LogItemViewModel>();
            LinkedListNode<LogItemViewModel> lastnode = null;
            foreach (var collname in Settings.Collections)
            {
                var coll = db.GetCollection(collname);
                var docs = coll.Find(Query.EQ("Subscribers", Environment.UserName));
                foreach (var doc in docs)
                {
                    LogItems.AddLast(new LogItemViewModel(doc, collname));

                }
            }
            _selected = LogItems.First;

        }

        public LinkedList<LogItemViewModel> LogItems { get; private set; }
        private LinkedListNode<LogItemViewModel> _selected;
        public LogItemViewModel Selected { get { return _selected.Value; } }

        public void Next()
        {
            _selected = _selected.Next;
            OnPropertyChanged("Selected");
        }

        public void Prev()
        {
            _selected = _selected.Previous;
            OnPropertyChanged("Selected");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
