using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LogDbDataLayer
/// </summary>
public class LogDbDataLayer
{
	public LogDbDataLayer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void increaseIPAddressInvalidAttempts(String ipAddress)
    {
        using (LogDbDataContext _dc = new LogDbDataContext())
        {
            _dc.IncreaseIPAddressInvalidAttempts(ipAddress);
        }
    }

    public void removeIPAddressInvalidAttempts(String ipAddress)
    {
        using (LogDbDataContext _dc = new LogDbDataContext())
        {
            _dc.RemoveIPAddressInvalidAttempt(ipAddress);
        }
    }

    public Boolean checkIPAddressExceededAttempts(String ipAddress)
    {
        using (LogDbDataContext _dc = new LogDbDataContext())
        {
            return _dc.IPAddressExceededInvalidAttempts(ipAddress).GetValueOrDefault();
        }
    }
}