using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePublisher
{
    public class TextMessageSearcher : MessageSearcher
    {
        private readonly string _query;

        public TextMessageSearcher(string query)
        {
            _query = query;
        }

        public override Func<Message, bool> Searcher
        {
            get
            {
                if (string.IsNullOrEmpty(_query))
                {
                    return (m) => true;
                }
                else
                {
                    return (m) => m.Content != null && m.Content.Contains(_query);
                }
            }
        }
    }
}
