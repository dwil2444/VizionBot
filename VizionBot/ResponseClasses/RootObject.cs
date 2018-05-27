using System;
using System.Collections.Generic;

namespace VizionBot
{
    public class RootObject
    {
        public Category[] categories { get; set; }
        public Tag[] tags { get; set; }
        public Description description { get; set; }
        public object[] faces { get; set; }
        public string requestId { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Description
    {
        public string[] tags { get; set; }
        public Caption[] captions { get; set; }
    }

    public class Caption
    {
        public string text { get; set; }
        public float confidence { get; set; }
    }

    public class Metadata
    {
        public int height { get; set; }
        public int width { get; set; }
        public string format { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public float score { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public float confidence { get; set; }
    }
}