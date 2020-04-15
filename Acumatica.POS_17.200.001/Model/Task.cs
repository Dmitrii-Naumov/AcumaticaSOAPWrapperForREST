using Acumatica.RESTClient.Model;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Acumatica.POS_17_200_001.Model
{
	[DataContract]
	public class Task : Entity
	{

		[DataMember(Name="Body", EmitDefaultValue=false)]
		public StringValue Body { get; set; }

		[DataMember(Name="Category", EmitDefaultValue=false)]
		public StringValue Category { get; set; }

		[DataMember(Name="CompletedAt", EmitDefaultValue=false)]
		public DateTimeValue CompletedAt { get; set; }

		[DataMember(Name="CompletionPercentage", EmitDefaultValue=false)]
		public IntValue CompletionPercentage { get; set; }

		[DataMember(Name="DueDate", EmitDefaultValue=false)]
		public DateTimeValue DueDate { get; set; }

		[DataMember(Name="Internal", EmitDefaultValue=false)]
		public BooleanValue Internal { get; set; }

		[DataMember(Name="NoteID", EmitDefaultValue=false)]
		public GuidValue NoteID { get; set; }

		[DataMember(Name="Owner", EmitDefaultValue=false)]
		public StringValue Owner { get; set; }

		[DataMember(Name="Parent", EmitDefaultValue=false)]
		public GuidValue Parent { get; set; }

		[DataMember(Name="ParentSummary", EmitDefaultValue=false)]
		public StringValue ParentSummary { get; set; }

		[DataMember(Name="Priority", EmitDefaultValue=false)]
		public StringValue Priority { get; set; }

		[DataMember(Name="RelatedActivities", EmitDefaultValue=false)]
		public List<TaskRelatedActivity> RelatedActivities { get; set; }

		[DataMember(Name="RelatedEntityDescription", EmitDefaultValue=false)]
		public StringValue RelatedEntityDescription { get; set; }

		[DataMember(Name="RelatedTasks", EmitDefaultValue=false)]
		public List<TaskRelatedTask> RelatedTasks { get; set; }

		[DataMember(Name="Reminder", EmitDefaultValue=false)]
		public ReminderDetail Reminder { get; set; }

		[DataMember(Name="StartDate", EmitDefaultValue=false)]
		public DateTimeValue StartDate { get; set; }

		[DataMember(Name="Status", EmitDefaultValue=false)]
		public StringValue Status { get; set; }

		[DataMember(Name="Summary", EmitDefaultValue=false)]
		public StringValue Summary { get; set; }

		[DataMember(Name="TimeActivity", EmitDefaultValue=false)]
		public TaskTimeActivity TimeActivity { get; set; }

		[DataMember(Name="WorkgroupID", EmitDefaultValue=false)]
		public StringValue WorkgroupID { get; set; }

	}
}