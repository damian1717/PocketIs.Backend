namespace PocketIS.Models.Complaint
{
    public class AddComplaint
    {
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string? Client { get; set; }
        public string? Product { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Deadline { get; set; }
        public string? ResponsiblePerson { get; set; }
        public string? Actions { get; set; }
        public string? WhatHappened { get; set; }
        public string? WhyItIsProblem { get; set; }
        public DateTime? WhenProblemIdentified { get; set; }
        public string? WhereProblemDetected { get; set; }
        public string? HowProblemDetected { get; set; }
        public string? WhoProblemDetected { get; set; }
        public int? PiecesNok { get; set; }
        public string? ProperProcess { get; set; }
        public string? InconsistencyDetected { get; set; }
    }
}
