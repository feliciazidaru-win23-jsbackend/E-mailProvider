﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_mailProvider.Models;

public class EmailRequest
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string HtmlBody { get; set; } = null!;
    public string PlainText { get; set; } = null!;
}
