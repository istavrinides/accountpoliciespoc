﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="LogDb")]
public partial class LogDbDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  #endregion
	
	public LogDbDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["LogDbConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public LogDbDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public LogDbDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public LogDbDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public LogDbDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.IncreaseIPAddressInvalidAttempts")]
	public int IncreaseIPAddressInvalidAttempts([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IPAddress", DbType="NVarChar(15)")] string iPAddress)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iPAddress);
		return ((int)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.RemoveIPAddressInvalidAttempt")]
	public int RemoveIPAddressInvalidAttempt([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IPAddress", DbType="NVarChar(15)")] string iPAddress)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iPAddress);
		return ((int)(result.ReturnValue));
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.IPAddressExceededInvalidAttempts", IsComposable=true)]
	public System.Nullable<bool> IPAddressExceededInvalidAttempts([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IPAddress", DbType="NVarChar(15)")] string iPAddress)
	{
		return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iPAddress).ReturnValue));
	}
}
#pragma warning restore 1591
