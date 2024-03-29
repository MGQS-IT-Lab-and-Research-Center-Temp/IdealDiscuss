﻿namespace IdealDiscuss.Entities.Dtos.QuestionReport
{
    public class QuestionReportDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string QuestionId { get; set; }
        public string QuestionReporter { get; set; }
        public string AdditionalComment { get; set; }
        public string QuestionText { get; set; }
        public List<string> FlagNames { get; set; } = new List<string>();
    }
}