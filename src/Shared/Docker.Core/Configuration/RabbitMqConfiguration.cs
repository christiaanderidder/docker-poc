﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Core.Configuration
{
    public class RabbitMqConfiguration
    {
        public const string Section = "RabbitMq";

        public string? Host { get; set; }
        public string? SchedulerEndpoint { get; set; }
    }
}
