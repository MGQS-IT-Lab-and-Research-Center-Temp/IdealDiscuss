using IdealDiscuss.Entities;
using System.Collections;

namespace IdealDiscuss.Service.Interface
{
     public interface ICommentReportService
     {
          CommentReport UserId(int userId);
          CommentReport User(User user);
          CommentReport CommentId(int CommentId);
          CommentReport Comment(Comment comment);
          CommentReport AditionalComment(string aditionalComment);
          ICollection<CommentReportFlag> list(CommentReportFlag flag);
     }
}

