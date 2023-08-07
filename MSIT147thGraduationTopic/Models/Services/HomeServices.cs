﻿using MSIT147thGraduationTopic.EFModels;
using MSIT147thGraduationTopic.Models.Dtos;
using MSIT147thGraduationTopic.Models.Infra.Repositories;
using MSIT147thGraduationTopic.Models.Infra.Utility;

namespace MSIT147thGraduationTopic.Models.Services
{
    public class HomeServices
    {
        private readonly GraduationTopicContext _context;
        private readonly HomeRepository _repo;

        public HomeServices(GraduationTopicContext context)
        {
            _context = context;
            _repo = new HomeRepository(context);
        }

        public IEnumerable<RecommandSpecDisplayDto> GetMostPopularSpecs(int amount = 8)
        {
            var specs = _repo.GetMostPopularSpecs(20);
            return new RandomGenerator().RandomCollectionFrom(specs, amount);
        }
        public IEnumerable<RecommandSpecDisplayDto> GetMostFavorableSpecs(int amount = 8)
        {
            var specs = _repo.GetMostFavorableSpecs(20);
            return new RandomGenerator().RandomCollectionFrom(specs, amount);
        }

        public IEnumerable<RecommandSpecDisplayDto> GetNewestSpecs(int amount = 8)
        {
            var specs = _repo.GetNewestSpecs(50);
            return new RandomGenerator().RandomCollectionFrom(specs, amount);
        }
    }
}
