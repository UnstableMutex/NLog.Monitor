using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MVVMLight.Extras;
using NLog.Monitor.Annotations;
using NLog.Monitor.ViewModel;

namespace NLog.Monitor
{
    class MainWindowViewModel :VMB
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
           // AssignCommands<NoWeakRelayCommand>();
            NextCommand = new RelayCommand(Next, CanNext);
            PrevCommand = new RelayCommand(Prev,CanPrev);
        }


        public LinkedList<LogItemViewModel> LogItems { get; private set; }
        private LinkedListNode<LogItemViewModel> _selected;
        public LogItemViewModel Selected { get { return _selected.Value; } }
        public RelayCommand PrevCommand { get; private set; }

        
        
        public RelayCommand NextCommand { get; private set; }


        public void Next()
        {
            _selected = _selected.Next;
            RaisePropertyChanged("Selected");
            NextCommand.RaiseCanExecuteChanged();
             PrevCommand.RaiseCanExecuteChanged();         
        }

        bool CanNext()
        {
            return _selected.Next != null;
        }
        bool CanPrev()
        {
            return _selected.Previous != null;
        }
        public void Prev()
        {
            _selected = _selected.Previous;
            RaisePropertyChanged("Selected");
            NextCommand.RaiseCanExecuteChanged();
            PrevCommand.RaiseCanExecuteChanged();  
        }


    }
}
