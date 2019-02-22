using System;
using System.Collections.Generic;
using System.Text;

namespace bayonet.Core.Models
{
    public class Item
    {
        /// <summary>
        /// The item's unique id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// True if the item deleted.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The type of the item.
        /// One of "job", "story", "comment", "poll", or "pollopt".
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// The username of the item's author.
        /// </summary>
        public string By { get; set; }

        /// <summary>
        /// Creation date of the item, in Unix Time.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// The comment, story or poll text.
        /// HTML.
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// True if the item is dead.
        /// </summary>
        public bool Dead { get; set; }

        /// <summary>
        /// The comment's parent: either another comment or the relevant story.
        /// </summary>
        public Item Parent { get; set; }

        /// <summary>
        /// The pollopt's associated poll.
        /// </summary>
        public Item Poll { get; set; }

        /// <summary>
        /// The ids of the item's comments, in ranked display order.
        /// </summary>
        public string[] Kids { get; set; }

        /// <summary>
        /// The URL of the story.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The story's score, or the votes for a pollopt.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The title of the story, poll or job.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A list of related pollopts, in display order.
        /// </summary>
        public Item[] Parts { get; set; }

        /// <summary>
        /// In the case of stories or polls, the total comment count.
        /// </summary>
        public int Descendants { get; set; }
    }
}
