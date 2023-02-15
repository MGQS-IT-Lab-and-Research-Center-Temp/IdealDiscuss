﻿using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.CommentReport
{
    public class ViewCommentReportDetailDto
    {
        public string AdditionalComment { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
