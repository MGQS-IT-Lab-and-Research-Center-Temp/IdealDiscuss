using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Implementations;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations
{
     public class CommentReportService : ICommentReportService
     {
          private CommentReportRepository _commentReportRepository;
          public CommentReportService(ICommentReportRepository commentReportRepository)
          {
               _commentReportRepository = (CommentReportRepository)commentReportRepository;
          }

          public CommentReport AditionalComment(string aditionalComment)
          {
               throw new NotImplementedException();
          }

          public CommentReport Comment(Comment comment)
          {
               throw new NotImplementedException();
          }

          public CommentReport CommentId(int CommentId)
          {
               throw new NotImplementedException();
          }

          public ICollection<CommentReportFlag> list(CommentReportFlag flag)
          {
               throw new NotImplementedException();
          }

          public CommentReport User(User user)
          {
               throw new NotImplementedException();
          }

          public CommentReport UserId(int userId)
          {
               throw new NotImplementedException();
          }
     }
}
