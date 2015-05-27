using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePublisher
{
    public class MessageSearcher
    {
        public virtual Func<Message, bool> Searcher
        {
            get { return (m) => true; }
        }
    }
}
