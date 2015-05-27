using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePublisher
{
    /// <summary>
    /// Useful const in application
    /// DO NOT MODIFY THIS FILE
    /// </summary>
    public static class Globals
    {
        public const string User1 = "Jon Snow";
        public const string User2 = "Daenerys Targaryen";
        public const string User3 = "Cersei Lannister";
        public const string User4 = "Sansa Stark";

        /// <summary>
        /// Sample text to be used as default content
        /// </summary>
        public const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vel venenatis eros. Etiam pretium augue ut dui fermentum, eu vulputate augue scelerisque. Nulla justo urna, lacinia non dolor ac, dictum molestie odio. Vestibulum dapibus lacus ut ex dignissim, at viverra sapien mattis. Nullam efficitur nunc nec ex mattis tincidunt vitae sit amet dolor. Nam sed est sed lectus lacinia vehicula nec cursus dui. Donec rutrum tincidunt risus, id gravida mauris tincidunt eu. Integer porttitor diam quis urna mattis, sit amet laoreet neque pharetra. Maecenas blandit metus augue. Vestibulum fringilla tempor tortor, eu tincidunt erat feugiat vitae. In tincidunt nulla nisl, nec dignissim nisl tristique id. Nam a ipsum dictum, fringilla libero in, mollis sem. Nulla porta placerat erat, et fermentum orci tincidunt lacinia.";

        private static string[] LoremIpsumWords = LoremIpsum.Split(' ');

        private static readonly Random _random;

        static Globals()
        {
            _random = new Random((int)DateTime.Now.Ticks);
        }

        public static string GetRandomText(int maxWordNo = 10)
        {
            var wordsNo = _random.Next(maxWordNo) + 1;
            var sb = new StringBuilder();

            for (int i = 0; i < wordsNo; i++)
            {
                sb.Append(LoremIpsumWords[_random.Next(LoremIpsumWords.Length)]);
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public static Message GetRandomMessage(string author, int maxWordNo = 10)
        {
            return new Message
            {
                Author = author,
                Content = GetRandomText(maxWordNo),
                PublishedOn = new DateTime(DateTime.Now.Ticks - ((long)_random.Next() * 10000L))
            };
        }

        public static IEnumerable<UserQueue> GetRandomDataForAllUsers(int maxMessages = 30, int maxWordNo = 10)
        {
            var resultList = new List<UserQueue>();

            foreach (var user in new [] {User1, User2, User3, User4})
            {
                var userQueue = new UserQueue
                {
                    Owner = user,
                    Messages = new List<Message>()
                };

                foreach (var id in Enumerable.Range(0, _random.Next(maxMessages)))
                {
                    userQueue.Messages.Add(GetRandomMessage(user, maxWordNo));
                }

                resultList.Add(userQueue);
            }


            return resultList;
        }
    }
}
