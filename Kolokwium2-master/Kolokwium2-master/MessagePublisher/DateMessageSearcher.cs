using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePublisher
{
    /// <summary>
    /// DO NOT MODIFY THIS FILE
    /// </summary>
    public class DateMessageSearcher : MessageSearcher
    {
        private readonly DateTime _from, _to;

        public DateMessageSearcher(DateTime from, DateTime to)
        {
            _from = from;
            _to = to;
        }

        public override Func<Message, bool> Searcher
        {
            get
            {
                return (m) => m.PublishedOn < _to.AddDays(1) && m.PublishedOn > _from.Date;
            }
        }
    }
}
