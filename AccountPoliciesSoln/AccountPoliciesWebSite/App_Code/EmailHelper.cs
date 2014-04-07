using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Summary description for EmailHelper
/// </summary>
public class EmailHelper
{
	public EmailHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void send(String address, String subject, String body)
    {
        // I am using the event log for testing. You should implement logic to send actual emails.
        
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(address);
        sb.AppendLine(subject);
        sb.AppendLine(body);

        EventLog.WriteEntry("Application", sb.ToString());
    }
}