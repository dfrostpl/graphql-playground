﻿using System;

namespace CMS.Models
{
    public abstract class DbObject
    {
        public Guid Id { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}