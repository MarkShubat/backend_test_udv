using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_test_udv
{
    public class Rootobject
    {
        public Response response { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public Item[] items { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int owner_id { get; set; }
        public int date { get; set; }
        public int can_delete { get; set; }
        public bool is_favorite { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
        public int can_edit { get; set; }
        public int can_pin { get; set; }
        public bool can_archive { get; set; }
        public bool is_archived { get; set; }
        public Post_Source post_source { get; set; }
        public Comments comments { get; set; }
        public Likes likes { get; set; }
        public Reposts reposts { get; set; }
        public Views views { get; set; }
        public Donut donut { get; set; }
        public float short_text_rate { get; set; }
        public bool zoom_text { get; set; }
        public string hash { get; set; }
    }

    public class Post_Source
    {
        public string type { get; set; }
    }

    public class Comments
    {
        public int can_post { get; set; }
        public int can_close { get; set; }
        public int count { get; set; }
        public bool groups_can_post { get; set; }
    }

    public class Likes
    {
        public int can_like { get; set; }
        public int count { get; set; }
        public int user_likes { get; set; }
        public int can_publish { get; set; }
    }

    public class Reposts
    {
        public int count { get; set; }
        public int wall_count { get; set; }
        public int mail_count { get; set; }
        public int user_reposted { get; set; }
    }

    public class Views
    {
        public int count { get; set; }
    }

    public class Donut
    {
        public bool is_donut { get; set; }
    }

}
