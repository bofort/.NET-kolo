using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessagePublisher
{
    /// <summary>
    /// DO NOT MODIFY THIS FILE
    /// </summary>
    public interface IMessagesViewModel : INotifyPropertyChanged
    {
        string UserName { get; }

        UserQueue SelectedUser { get; set; }

        IEnumerable<UserQueue> ObservedUsers { get; }

        string NewMessageText { get; set; }

        ICommand PublishCommand { get; }

        DateTime FromDate { get; set; }

        DateTime ToDate { get; set; }

        string TextFilter { get; set; }

        ICommand FilterCommand { get; }

        IEnumerable<Message> FilteredMessages { get; }

        ICommand SaveCommand { get; }

    }
}
