﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.DTOs.Email
{
    public class EmailRequestDto
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

    }
}
