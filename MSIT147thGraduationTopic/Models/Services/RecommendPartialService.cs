﻿using MessagePack.Formatters;
using Microsoft.IdentityModel.Tokens;
using MSIT147thGraduationTopic.EFModels;
using MSIT147thGraduationTopic.Models.Dtos.Recommend;
using MSIT147thGraduationTopic.Models.Infra.Repositories;
using MSIT147thGraduationTopic.Models.Infra.Utility;
using System.Security.Claims;

namespace MSIT147thGraduationTopic.Models.Services
{
    public class RecommendPartialService
    {

        private readonly GraduationTopicContext _context;
        private readonly RecommendPartialRepository _repo;
        private readonly IHttpContextAccessor _accessor;

        public RecommendPartialService(GraduationTopicContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _repo = new(context);
            _accessor = accessor;

        }

        public async Task<List<SpecDisplyDto>> GetFavorSpecs(int? merchandiseId)
        {
            var generator = new RandomGenerator();

            int[] visitedMerchandiseIds = GetVisitedMerchandiseIds();
            var visitedTagIds = await _repo.GetVisitedTagIds(visitedMerchandiseIds);
            bool logIn = int.TryParse(_accessor.HttpContext!.User.FindFirstValue("MemberId"), out int parseNumber);
            int? memberId = logIn ? parseNumber : null;
            var inCartTagIds = await _repo.GetInCartTagIds(memberId);
            int[] conflictSpecIds = await _repo.GetMerchandiseSpecIds(merchandiseId);

            if (visitedTagIds.IsNullOrEmpty() && inCartTagIds.IsNullOrEmpty())
            {
                var specIds = await _repo.GetRecommendSpecIds(null, conflictSpecIds, 40);
                specIds = generator.RandomCollectionFrom(specIds, 8).ToList();
                return await _repo.GetSpecDisplayDtos(specIds);
            }

            var recommendByVisitSpecIds = !visitedTagIds.IsNullOrEmpty() ?
                await _repo.GetRecommendSpecIds(visitedTagIds, conflictSpecIds) : new();
            var recommendByCartSpecIds = !inCartTagIds.IsNullOrEmpty() ?
                await _repo.GetRecommendSpecIds(inCartTagIds, recommendByVisitSpecIds.Concat(conflictSpecIds)) : new();

            var recSpecIds = recommendByVisitSpecIds.Concat(recommendByCartSpecIds).Distinct();
            recSpecIds = generator.RandomCollectionFrom(recSpecIds, 8);
            return await _repo.GetSpecDisplayDtos(recSpecIds);
        }

        private int[] GetVisitedMerchandiseIds()
        {
            int? last1 = _accessor.HttpContext!.Session.GetInt32("Last_1");
            int? last2 = _accessor.HttpContext.Session.GetInt32("Last_2");
            int? last3 = _accessor.HttpContext.Session.GetInt32("Last_3");
            return new int?[] { last1, last2, last3 }
                .Where(o => o != null).Select(o => o!.Value).ToArray();
        }



    }
}
