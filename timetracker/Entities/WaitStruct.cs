namespace timetracker.Entities
{
	public struct WaitStruct
	{
		public int PID { get; set; }

		public ulong StartTime { get; set; }

		public int Count { get; set; }

		public int ParentID { get; set; }
	}
}
