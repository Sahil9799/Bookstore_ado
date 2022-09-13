namespace RepositoryLayer.Interfaces.FeedbackInterfaces
{
    using System.Collections.Generic;
    using ModelLayer.Models.FeedbackModels;

    public interface IFeedbackRL
    {
        public bool AddFeedback(int UserId, FeedbackPostModel postModel);

        public List<FeedbackResponseModel> GetAllFeedbacksByBookId(int BookId);

        public bool DeleteFeedbackById(int FeedbackId);
    }
}