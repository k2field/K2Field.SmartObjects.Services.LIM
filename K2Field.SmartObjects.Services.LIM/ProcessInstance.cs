﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using Attributes = SourceCode.SmartObjects.Services.ServiceSDK.Attributes;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System.Net;
using SourceCode.Workflow.Management;

namespace K2Field.SmartObjects.Services.LIM
{
    /*
     * exception handling
     * */

    [Attributes.ServiceObject("LIMProcessInstance", "LIM Process Instance", "LIM Process Instance")]
    public class ProcessInstance
    {
        
        [Attributes.Property("ProcessSetId", SoType.Number, "Process Set Id", "Process Set Id")]
        public int ProcessSetId { get; set; }

        public ServiceConfiguration ServiceConfiguration { get; set; }

        [Attributes.Property("ProcessId", SoType.Number, "Process Id", "Process Id")]
        public int ProcessId { get; set; }

        [Attributes.Property("ProcessInstanceId", SoType.Number, "Process Instance Id", "Process Instance Id")]
        public int ProcessInstanceId { get; set; }

        [Attributes.Property("ExecutingProcessId", SoType.Number, "Executing Process Id", "Executing Process Id")]
        public int? ExecutingProcessId { get; set; }

        [Attributes.Property("Name", SoType.Text, "Name", "Name")]
        public string Name { get; set; }

        [Attributes.Property("FullName", SoType.Text, "FullName", "FullName")]
        public string FullName { get; set; }

        [Attributes.Property("Folio", SoType.Text, "Folio", "Folio")]
        public string Folio { get; set; }

        [Attributes.Property("Originator", SoType.Text, "Originator", "Originator")]
        public string Originator { get; set; }

        [Attributes.Property("Status", SoType.Text, "Status", "Status")]
        public string Status { get; set; }

        [Attributes.Property("ExpectedDuraction", SoType.Number, "Expected Duration", "Expected Duration")]
        public int ExpectedDuration { get; set; }

        [Attributes.Property("StartDate", SoType.DateTime, "Start Date", "Start Date")]
        public DateTime? StartDate { get; set; }

        [Attributes.Property("FinishDate", SoType.DateTime, "Finish Date", "Finish Date")]
        public DateTime? FinishDate { get; set; }

        [Attributes.Property("Priority", SoType.Number, "Priority", "Priority")]
        public int Priority { get; set; }

        [Attributes.Property("NewVersion", SoType.Number, "New Version", "New Version")]
        public int NewVersion { get; set; }

        [Attributes.Property("ActivityName", SoType.Text, "Activity Name", "Activity Name")]
        public string ActivityName { get; set; }

        [Attributes.Property("SecondActivityName", SoType.Text, "Second Activity Name", "Second Activity Name")]
        public string SecondActivityName { get; set; }

        [Attributes.Property("ResultStatus", SoType.Text, "Result Status", "Result Status")]
        public string ResultStatus { get; set; }

        [Attributes.Property("ResultMessage", SoType.Text, "Result Message", "Result Message")]
        public string ResultMessage { get; set; }

        //[Attributes.Property("IsDefaultVersion", SoType.Text, "Is Default Version", "Is Default Version")]
        //public bool IsDefaultVersion { get; set; }

        //[Attributes.Property("VersionDate", SoType.DateTime, "Version Date", "Version Date")]
        //public DateTime VersionDate { get; set; }

        //[Attributes.Property("VersionDescription", SoType.Text, "Version Description", "Version Description")]
        //public string VersionDescription { get; set; }

        //[Attributes.Property("VersionLabel", SoType.Text, "Version Label", "Version Label")]
        //public string VersionLabel { get; set; }

        //[Attributes.Property("VersionNumber", SoType.Text, "Version Number", "Version Number")]
        //public int VersionNumber { get; set; }



        [Attributes.Method("GetRunningProcessInstance", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.Read, "Get Running Process Instance", "Get Running Process Instance",
        new string[] { "ProcessInstanceId" }, //required property array (no required properties for this sample)
        new string[] { "ProcessInstanceId" }, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "ProcessSetId", "ProcessId", "ExecutingProcessId", "FullName", "Folio", "Originator", "Status", "ExpectedDuraction", "StartDate", "FinishDate", "Priority", "ResultStatus", "ResultMessage" })] // , "IsDefaultVersion", "VersionDate", "VersionDescription", "VersionLabel", "VersionNumber"
        public LIM.ProcessInstance GetProcessInstance()
        {
            WorkflowManagementServer svr = new WorkflowManagementServer("localhost", 5555);
            try
            {
                svr.Open();
                ProcessInstances instances = null;

                SourceCode.Workflow.Management.Criteria.ProcessInstanceCriteriaFilter filter = new SourceCode.Workflow.Management.Criteria.ProcessInstanceCriteriaFilter();
                filter.AddRegularFilter(ProcessInstanceFields.ProcInstID, SourceCode.Workflow.Management.Criteria.Comparison.Equals, this.ProcessInstanceId);

                instances = svr.GetProcessInstancesAll(filter);

                if (instances != null & instances.Count > 0)
                {
                    this.ExecutingProcessId = instances[0].ExecutingProcID;
                    this.ExpectedDuration = instances[0].ExpectedDuration;
                    this.FinishDate = instances[0].FinishDate;
                    this.Folio = instances[0].Folio;
                    this.FullName = instances[0].ProcSetFullName;
                    //this.IsDefaultVersion = instances[0].Process.DefaultVersion;
                    //this.Name = instances[0].Process.FullName;
                    this.Originator = instances[0].Originator;
                    this.Priority = instances[0].Priority;
                    this.ProcessId = instances[0].ProcID;
                    this.ProcessInstanceId = instances[0].ID;
                    this.ProcessSetId = instances[0].ProcSetID;
                    this.StartDate = instances[0].StartDate;
                    this.Status = instances[0].Status;
                    //this.VersionDescription = instances[0].Process.VersionDesc;
                    //this.VersionDate = instances[0].Process.VersionDate;
                    //this.VersionLabel = instances[0].Process.VersionLabel;
                    //this.VersionNumber = instances[0].Process.VersionNumber;
                }
            }
            catch (Exception ex)
            {
                this.ResultStatus = "Exception";
                this.ResultMessage = ex.GetBaseException().Message;
                return this;
            }
            finally
            {
                try
                {
                    svr.Connection.Close();
                    svr.Connection.Dispose();
                }
                catch { }

                svr = null;
            }

            return this;
        }

        [Attributes.Method("GetRunningProcessInstanceByVersion", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.List, "Get Running Process Instance By Version", "Get Process Instance By Version",
        new string[] { "ProcessId" }, //required property array (no required properties for this sample)
        new string[] { "ProcessId"}, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "ProcessSetId", "ProcessId", "ExecutingProcessId", "FullName", "Name", "Folio", "Originator", "Status", "ExpectedDuraction", "StartDate", "FinishDate", "Priority" })] // , "IsDefaultVersion", "VersionDate", "VersionDescription", "VersionLabel", "VersionNumber"
        public List<LIM.ProcessInstance> GetRunningProcessInstanceByVersion()
        {
            List<LIM.ProcessInstance> results = new List<ProcessInstance>();

            WorkflowManagementServer svr = new WorkflowManagementServer("localhost", 5555);
            try
            {
                svr.Open();
                ProcessInstances instances = null;

                SourceCode.Workflow.Management.Criteria.ProcessInstanceCriteriaFilter filter = new SourceCode.Workflow.Management.Criteria.ProcessInstanceCriteriaFilter();
                filter.AddRegularFilter(ProcessInstanceFields.ProcID, SourceCode.Workflow.Management.Criteria.Comparison.Equals, this.ProcessId);

                instances = svr.GetProcessInstancesAll(filter);

                for (int i = 0; i < instances.Count; i++)
                {
                    LIM.ProcessInstance pi = new ProcessInstance();

                    pi.ExecutingProcessId = instances[i].ExecutingProcID;
                    pi.ExpectedDuration = instances[i].ExpectedDuration;
                    pi.FinishDate = instances[i].FinishDate;
                    pi.Folio = instances[i].Folio;
                    pi.FullName = instances[i].ProcSetFullName;
                    //pi.IsDefaultVersion = instances[i].Process.DefaultVersion;
                    //pi.Name = instances[i].Process.FullName;
                    pi.Originator = instances[i].Originator;
                    pi.Priority = instances[i].Priority;
                    pi.ProcessId = instances[i].ProcID;
                    pi.ProcessInstanceId = instances[i].ID;
                    pi.ProcessSetId = instances[i].ProcSetID;
                    pi.StartDate = instances[i].StartDate;
                    pi.Status = instances[i].Status;
                    //pi.VersionDescription = instances[i].Process.VersionDesc;
                    //pi.VersionDate = instances[i].Process.VersionDate;
                    //pi.VersionLabel = instances[i].Process.VersionLabel;
                    //pi.VersionNumber = instances[i].Process.VersionNumber;

                    results.Add(pi);
                }
            }
            catch (Exception ex)
            {
                //this.ResultStatus = "Exception";
                //this.ResultMessage = ex.GetBaseException().Message;
                //return this;
            }
            finally
            {
                try
                {
                    svr.Connection.Close();
                    svr.Connection.Dispose();
                }
                catch { }

                svr = null;
            }

            return results;
        }

        [Attributes.Method("MigrateProcessInstance", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.Read, "Migrate Process Instance", "Migrate process instance from one version to another version",
        new string[] { "ProcessInstanceId", "NewVersion" }, //required property array (no required properties for this sample)
        new string[] { "ProcessInstanceId", "NewVersion", "ActivityName" }, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "NewVersion", "ActivityName", "ResultStatus", "ResultMessage" })] // , "IsDefaultVersion", "VersionDate", "VersionDescription", "VersionLabel", "VersionNumber"
        public LIM.ProcessInstance MigrateProcessInstance()
        {
            List<LIM.ProcessInstance> results = new List<ProcessInstance>();

            WorkflowManagementServer svr = new WorkflowManagementServer("localhost", 5555);
                        
            try
            {
                svr.Open();
                svr.StopProcessInstances(this.ProcessInstanceId);
                
                svr.SetProcessInstanceVersion(this.ProcessInstanceId, this.NewVersion);
                
                if (!string.IsNullOrWhiteSpace(this.ActivityName))
                {
                    svr.GotoActivity(this.ProcessInstanceId, ActivityName);
                }
                else
                {
                    svr.StartProcessInstances(this.ProcessInstanceId);
                }
            }
            catch (Exception ex)
            {
                this.ResultStatus = "Exception";
                this.ResultMessage = ex.GetBaseException().Message;
                return this;
            }
            finally
            {
                try
                {
                    svr.Connection.Close();
                    svr.Connection.Dispose();
                }
                catch { }

                svr = null;
            }

            this.ResultStatus = "Success";

            return this;
        }


        [Attributes.Method("GotoActivity", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.Read, "Goto Activity", "Move a process instance from one Activity to another",
        new string[] { "ProcessInstanceId", "ActivityName" }, //required property array (no required properties for this sample)
        new string[] { "ProcessInstanceId", "ActivityName" }, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "ActivityName", "ResultStatus", "ResultMessage" })] 
        public LIM.ProcessInstance GotoActivity()
        {
            List<LIM.ProcessInstance> results = new List<ProcessInstance>();

            WorkflowManagementServer svr = new WorkflowManagementServer("localhost", 5555);
            
            try
            {
                svr.Open();
                svr.GotoActivity(this.ProcessInstanceId, this.ActivityName);
            }
            catch (Exception ex)
            {
                //forcedStop = false;
                this.ResultStatus = "Exception";
                this.ResultMessage = ex.GetBaseException().Message;
                return this;
            }
            finally
            {
                try
                {
                    svr.Connection.Close();
                    svr.Connection.Dispose();
                }
                catch { }

                svr = null;
            }

            this.ResultStatus = "Success";
           
            return this;
        }


        [Attributes.Method("GotoActivities", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.Read, "Goto Activities", "Move a process instance from one Activity to two separate activites",
        new string[] { "ProcessInstanceId", "ActivityName", "SecondActivityName" }, //required property array (no required properties for this sample)
        new string[] { "ProcessInstanceId", "ActivityName", "SecondActivityName" }, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "ActivityName", "SecondActivityName", "ResultStatus", "ResultMessage" })]
        public LIM.ProcessInstance GotoActivities()
        {
            List<LIM.ProcessInstance> results = new List<ProcessInstance>();

            WorkflowManagementServer svr = new WorkflowManagementServer("localhost", 5555);

            try
            {
                svr.Open();
                svr.GotoActivity(this.ProcessInstanceId, this.ActivityName, this.SecondActivityName);
            }
            catch (Exception ex)
            {
                //forcedStop = false;
                this.ResultStatus = "Exception";
                this.ResultMessage = ex.GetBaseException().Message;
                return this;
            }
            finally
            {
                try
                {
                    svr.Connection.Close();
                    svr.Connection.Dispose();
                }
                catch { }

                svr = null;
            }

            this.ResultStatus = "Success";

            return this;
        }
    }
}
