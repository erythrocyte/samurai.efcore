﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SamuraiApp.Data
{
    public partial class Clan
    {
        public Clan()
        {
            Samurais = new HashSet<Samurai>();
        }

        public int Id { get; set; }
        public string ClanName { get; set; }

        public virtual ICollection<Samurai> Samurais { get; set; }
    }
}
