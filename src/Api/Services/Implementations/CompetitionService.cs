using Api.Services.Interfaces;
using DataAccess.Entity;

namespace Api.Services.Implementations
{
    /// <summary>
    /// TODO Make a proper implementation which acutally saves the competition entity to database
    /// </summary>
    public class CompetitionService : ICompetitionService
    {
        private CompetitionEntity? _competitionEntity;

        public void UploadCompetition(CompetitionEntity entity)
        {
            _competitionEntity = entity;
        }

        public CompetitionEntity? GetCurrentState()
        {
            return _competitionEntity;
        }
    }
}
