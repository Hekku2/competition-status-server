using DataAccess.Entity;

namespace Api.Services.Interfaces
{
    /// <summary>
    /// Todo actually design this
    /// </summary>
    public interface ICompetitionService
    {
        void UploadCompetition(CompetitionEntity entity);
        public CompetitionEntity? GetCurrentState();
    }
}
