using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePublisher
{
    /// <summary>
    /// Your TODO: please follow insstruction 
    /// </summary>
    public class MessagesViewModel : IMessagesViewModel, INotifyPropertyChanged
    {
        private readonly IDispatcher _dispatcher;

        //Niestety na zapuis do pliku brakło czasu, filtrowanie też nie działa do końca tak jak mialo...
        public MessagesViewModel(IDispatcher dispatcher)
        {
            var dateSearch = new DateMessageSearcher(FromDate, ToDate);
            var textSearch = new TextMessageSearcher(TextFilter);
            List<Message> msgs = new List<Message>();
            foreach (var user in ObservedUsers)
            {
                foreach (var msg in user.Messages)
                {
                    if (DataSearch.Searcher(msg) || TextSearch.Searcher(msg))
                    {
                        msgs.Add(msg);
                    }
                }
            }
            FilteredMessages = msgs.AsEnumerable();
            _dispatcher = dispatcher;
        }

        public string UserName
        {
            get { return ObservedUsers.First().Owner; }
        }

        public UserQueue SelectedUser
        {
            get
            {
                return ObservedUsers.First();
            }
            set
            {
                OnPropertyChanged("SelectedUser");
            }
        }

        public IEnumerable<UserQueue> ObservedUsers
        {
            get { return Globals.GetRandomDataForAllUsers(); }
        }

        private string _NewMessagetext;
        public string NewMessageText
        {
            get
            {
                return _NewMessagetext;
            }
            set
            {
                if(_NewMessagetext != value){
                    _NewMessagetext = value;
                }
                OnPropertyChanged("NewMessageText");
            }
        }

        public System.Windows.Input.ICommand PublishCommand
        {
            get { return new MyCommand(PublishCommandMethod); }
        }

        private void PublishCommandMethod()
        {
            Message msg = new Message();
            msg.Author = UserName;
            msg.Content = NewMessageText;
            msg.PublishedOn = DateTime.Now;
            ObservedUsers.Single(s => s.Owner == UserName).Messages.Add(msg) ;
        }

        public DateTime FromDate
        {
            get
            {
                return DateTime.Now.AddYears(-1);
            }
            set
            {
                OnPropertyChanged("FromDate");
            }
        }

        public DateTime ToDate
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
                OnPropertyChanged("ToDate");
            }
        }

        private string _textFilter;
        public string TextFilter
        {
            get
            {
                return _textFilter;
            }
            set
            {
                if (_textFilter != value)
                {
                    _textFilter = value;
                }
                OnPropertyChanged("TextFilter");
            }
        }

        public System.Windows.Input.ICommand FilterCommand
        {
            get { return new MyCommand(FilterCommandMethod); }
        }

        private void FilterCommandMethod()
        {
            IEnumerable<UserQueue> userMessage =
                from user in ObservedUsers
                where user.Owner == UserName
                select new UserQueue { Messages = user.Messages, Owner = user.Owner };
            if (TextFilter != null && TextFilter != string.Empty && SelectedUser != null && userMessage != null)
            {
                var dateSearch = new DateMessageSearcher(FromDate,ToDate);
                var textSearch = new TextMessageSearcher(TextFilter);
                List<Message> msgs = new List<Message>();
                foreach(var user in ObservedUsers)
                {
                    foreach(var msg in user.Messages)
                    {
                        if (dateSearch.Searcher(msg) || textSearch.Searcher(msg))
                        {
                            msgs.Add(msg);
                        }  
                    }
                }
                FilteredMessages = msgs.AsEnumerable();
            }
        }

        public DateMessageSearcher DataSearch
        {
            get { return new DateMessageSearcher(FromDate,ToDate); }
        }

        public TextMessageSearcher TextSearch
        {
            get { return new TextMessageSearcher(TextFilter); }
        }

        private IEnumerable<Message> _FilteredMessages;
        public IEnumerable<Message> FilteredMessages
        {
            get { return _FilteredMessages; }

            set
            {
                if(_FilteredMessages != value)
                {
                    _FilteredMessages = value;
                }
                OnPropertyChanged("FilteredMessages");
            }
        }

        public System.Windows.Input.ICommand SaveCommand
        {
            get { return new MyCommand(SaveCommandMethod); }
        }

        private void SaveCommandMethod()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
