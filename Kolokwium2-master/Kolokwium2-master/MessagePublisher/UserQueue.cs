using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MessagePublisher
{
    /// <summary>
    /// DO NOT MODIFY THIS FILE
    /// </summary>
    public class UserQueue
    {
        public string Owner { get; set; }

        public List<Message> Messages { get; set; }
    }
}
