﻿namespace Exam.ViTube
{
    public class User
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public int Watched { get; set; }

        public int Voted { get; set; }

        public User(string id, string username)
        {
            Id = id;
            Username = username;
            Watched = 0;
            Voted = 0;
        }

        
    }
}
