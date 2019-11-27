using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskDAL
{
    // Enum value representing status of update value
    public enum UpdateStatus
    {
        Ok = 1,
        Failed = -1,
        Stale = -2
    };
}

